$(document).ready(function () {
    var mapName, mapLength, mapWidth, mapData, baseLayer, beaconLayer, packageLayer;
    var recentX, recentY;
    var client;
    var agvSpeed, agvId, agvMode;
    var coords = [];
    var recentView;
    var cellId, cellIndex, cellValue, cellType;
    var isByUserCellSelectorChange = true;
    //Init, checking dashboard
    console.log('Loaded');
    //Checking AGV ID selector
    agvId = $('#agvSelector').val();
    console.log('AGV ID = ' + agvId);
    $('#agvSelector').on('change', function () {
        agvId = $(this).find(':selected').val();
        console.log('AGV ID = ' + agvId);
    });
    //Checking AGV mode selector
    agvMode = $('#modeSelector').val();
    console.log('AGV mode = ' + agvMode);
    $('#modeSelector').on('change', function () {
        agvMode = $(this).find(':selected').val();
        console.log('AGV mode = ' + agvMode);
    });
    //Checking camera view
    $('#cameraStreamView').addClass('hide');
    if ($('#cameraSwitch').is(':checked')) {
        console.log('Camera ON');
        $('#cameraIndicator').html('ON');
    } else {
        console.log('Camera OFF');
        $('#cameraIndicator').html('OFF');
    }
    //Checking camera switch
    $('#cameraSwitch').on('change', function () {
        if ($('#cameraSwitch').is(':checked')) {
            console.log('Camera ON');
            $('#cameraIndicator').html('ON');
            $('#cameraStreamView').attr('src', ' https://3e26-203-113-151-208.ap.ngrok.io/stream');
            $('#cameraStreamView').removeClass('hide');
        } else {
            console.log('Camera OFF');
            $('#cameraIndicator').html('OFF');
            $('#cameraStreamView').attr('src', '');
            $('#cameraStreamView').addClass('hide');
        }
    });
    //Checking speed indicator
    $('#speedIndicator').html('Motor speed: ' + $('#speedSlider').val());
    agvSpeed = $('#speedSlider').val();

    //Watching on speed change
    document.getElementById('speedSlider').oninput = function () {
        agvSpeed = $('#speedSlider').val();
        console.log("Speed Slider = " + agvSpeed);
        $('#speedIndicator').html('Motor speed: ' + $('#speedSlider').val());
        mqtt_publish('agv/control/001', 'speed ' + agvSpeed, 'move');
    };
    //
    //Checking steering assist
    if ($('#steeringAssistSwitch').is(':checked')) {
        console.log('Steering Assist ON');
        $('#steeringAssistIndicator').html('Steering Assist: ON');
    } else {
        console.log('Steering Assist OFF');
        $('#steeringAssistIndicator').html('Steering Assist: OFF');
    }

    $('#steeringAssistSwitch').on('change', function () {
        if ($('#steeringAssistSwitch').is(':checked')) {
            console.log('Steering Assist ON');
            $('#steeringAssistIndicator').html('Steering Assist: ON');
        } else {
            console.log('Steering Assist OFF');
            $('#steeringAssistIndicator').html('Steering Assist: OFF');
        }
    });
    //Sleep function
    function sleep(milliseconds) {
        const date = Date.now();
        let currentDate = null;
        do {
            currentDate = Date.now();
        } while (currentDate - date < milliseconds);
    }
    //Init map
    function init_map(length, width, resolution) {
        previousCellType = '';
        let innerHtml = '';
        $('#editMapLength').attr('placeholder', length);
        $('#editMapWidth').attr('placeholder', width);

        for (let i = 0; i < length; i++) {
            innerHtml += '<tr class="m-0 p-0">';
            for (let j = 0; j < width; j++) {
                innerHtml += '<td><div class="squareCell border-end border-bottom border-secondary m-0 p-0" id="' + j + ':' + i + '"></div></td>';
            }
            innerHtml += '</tr>';
        }
        $('#gridMap').removeClass('hide');
        $('#gridMap').append(innerHtml);
        console.log('Init map done');

        $(".squareCell").click(function () {
            $('#cellCoordIndicator').text("Cell coordinate: " + this.id);
            let cellCoord = this.id.split(":");

            cellId = this.id;
            recentX = cellCoord[0];
            recentY = cellCoord[1];
            console.log(coords);
            cellIndex = (((parseInt(recentY) + 1) * mapWidth) - (mapWidth - recentX));
            cellType = coords[cellIndex].type;

            console.log('Cell type = ' + cellType);

            $('#cellTypeSelector option').removeAttr("selected");
            $('#cellTypeSelector option[value="' + cellType + '"]').attr('selected', 'selected');
            $("#cellTypeSelector").val(cellType).change();

            console.log('Cell index: ' + cellIndex);
            console.log('Cell with choosen coordinate : ' + this.id);
            console.log('Cell data');
            console.log(coords[cellIndex]);
        })

        $('#cellTypeSelector').on('change', function () {
            console.log('Change event');
            cellType = $(this).find(':selected').val();
            coords[cellIndex].type = cellType;
            render_cell(cellType, recentX, recentY);
            console.log('Cell type = ' + cellType + ' changed');
        });
    }
    //Clear grid map
    function clearGridMap() {
        $('#gridMap').empty();
        console.log('Cleared grid map');
    }
    //Get map data from API server
    function get_map_data() {
        let jsonRepsonse = '{"name":"Warehouse 1", "length":12, "width":10, "coordinates":[{"x":"0", "y":"0", "type":"0"},{"x":"1", "y":"0", "type":"#"},{"x":"2", "y":"0", "type":"1"},{"x":"3", "y":"0", "type":"1"},{"x":"0", "y":"1", "type":"0"},{"x":"1", "y":"1", "type":"*"},{"x":"2", "y":"1", "type":"0"},{"x":"3", "y":"1", "type":"0"},{"x":"0", "y":"2", "type":"1"},{"x":"1", "y":"2", "type":"1"},{"x":"2", "y":"2", "type":"0"},{"x":"3", "y":"2", "type":"0"},{"x":"0", "y":"3", "type":"0"},{"x":"1", "y":"3", "type":"*"},{"x":"2", "y":"3", "type":"#"},{"x":"3", "y":"3", "type":"#"}]}';

        mapData = jsonRepsonse;
        console.log('Get map data done');
    }

    function init_map_layer() {
        baseLayer = mapData;
        beaconLayer = mapData;
        packageLayer = mapData;
    }

    function render_map(data) {
        let _mapData = JSON.parse(data);

        mapName = _mapData.name;
        mapWidth = _mapData.width;
        mapLength = _mapData.length;
        coords = _mapData.coordinates;

        let coordIndex = 0;
        for (let y = 0; y < mapLength; y++) {
            for (let x = 0; x < mapWidth; x++) {
                //console.log('coordIndex ' + coordIndex);
                if ((coords[coordIndex] != null) || (coords[coordIndex] != undefined)) {
                    let coord = coords[coordIndex];
                    //console.log(coord);
                    render_cell(coord.type, x, y);
                }
                coordIndex++;
            }
        }

        console.log('Visualize data done');
        //console.log(_mapData);
        //console.log(coords);
    }

    function render_cell(cellType, x, y) {
        if (cellType == '#') {
            //console.log('id = ' + x + ':' + y);
            let cell = document.getElementById(x + ':' + y);
            cell.classList.remove("bg-success");
            cell.classList.remove("bg-transparent");
            cell.classList.remove("bg-warning");
            cell.classList.add("bg-info");
        }
        else if (cellType == '*') {
            //console.log('id = ' + x + ':' + y);
            let cell = document.getElementById(x + ':' + y);
            cell.classList.remove("bg-transparent");
            cell.classList.remove("bg-info");
            cell.classList.remove("bg-warning");
            cell.classList.add("bg-success");
        }
        else if (cellType == '1') {
            //console.log('id = ' + x + ':' + y);
            let cell = document.getElementById(x + ':' + y);
            cell.classList.remove("bg-transparent");
            cell.classList.remove("bg-info");
            cell.classList.remove("bg-success");
            cell.classList.add("bg-warning");
        }
        else if (cellType == '0') {
            //console.log('id = ' + x + ':' + y);
            let cell = document.getElementById(x + ':' + y);
            cell.classList.remove("bg-warning");
            cell.classList.remove("bg-info");
            cell.classList.remove("bg-success");
            cell.classList.add("bg-transparent");
        }

        console.log('Rendered cell');
    }

    function init_mqtt() {
        let mqttBrokerUrl = 'ws://pirover.xyz:9001';
        client = mqtt.connect(mqttBrokerUrl);

        client.on("connect", function () {
            client.subscribe("agv/control/" + agvId);
            client.subscribe("agv/status/" + agvId);
            client.subscribe("agv/package/delivery");
            client.subscribe("agv/package/delivery");
            client.subscribe("agv/package/delivery");

            console.log('Init MQTT done');
        });

        client.on("message", function (topic, payload) {
            console.log([topic, payload].join(": "));
        });
    }

    function mqtt_publish(topic, message, type) {
        let msg;
        if (type == 'move') {
            //msg = {
            //    controller: 'motor-controller',
            //    command: message,
            //}
            client.publish(topic, message);
        }
        console.log('Sent ' + topic + message);
    }

    function init_nav_button() {
        $('#bleScannerButton').click(function () {
            if (recentView != 'bleScannerView') {
                $('#bleScannerView').removeClass('d-none');
                $('#gridMap').addClass('d-none');
                $('#laserScannerView').addClass('d-none');
                $('#deliveryControlView').addClass('d-none');
                recentView = 'bleScannerView';
            }
            console.log('bleScannerButton clicked');
        });

        $('#mapViewButton').click(function () {
            if (recentView != 'gridMap') {
                $('#gridMap').removeClass('d-none');
                $('#bleScannerView').addClass('d-none');
                $('#laserScannerView').addClass('d-none');
                $('#deliveryControlView').addClass('d-none');
                recentView = 'gridMap';
            }
            console.log('mapViewButton clicked');
        });

        $('#deliveryControlButton').click(function () {
            if (recentView != 'deliveryControlView') {
                $('#deliveryControlView').removeClass('d-none');
                $('#bleScannerView').addClass('d-none');
                $('#laserScannerView').addClass('d-none');
                $('#gridMap').addClass('d-none');
                recentView = 'deliveryControlView';
            }
            console.log('deliveryControlButton clicked');
        });
    }

    function init_control_buttons() {
        //For clicking button
        $('#goForwardButton').click(function () {
            if ((agvMode == 'direct') && (agvId != 'Select AGV')) {
                mqtt_publish('agv/control/' + agvId, 'forward', 'move');
                console.log('Go Forward');
            } else {
                alert('AGV ID or Direct mode is not chosen');
            }        
        });

        $('#goBackwardButton').click(function () {
            if ((agvMode == 'direct') && (agvId != 'Select AGV')) {
                mqtt_publish('agv/control/' + agvId, 'backward', 'move');
                console.log('Go Backward');
            } else {
                alert('AGV ID or Direct mode is not chosen');
            }             
        });

        $('#turnLeftButton').click(function () {
            if ((agvMode == 'direct') && (agvId != 'Select AGV')) {
                mqtt_publish('agv/control/' + agvId, 'turn-left', 'move');
                console.log('Turn Left');
            } else {
                alert('AGV ID or Direct mode is not chosen');
            }       
        });

        $('#turnRightButton').click(function () {
            if ((agvMode == 'direct') && (agvId != 'Select AGV')) {
                mqtt_publish('agv/control/' + agvId, 'turn-right', 'move');
                console.log('Turn Right');
            } else {
                alert('AGV ID or Direct mode is not chosen');
            }   
        });

        //For pressing physical key
        $(document).keydown(function (e) {
            if ((e.key == 'w') || (e.key == 'W')) {
                let button = document.getElementById('goForwardButton');
                button.classList.remove('bg-info');
                button.classList.add('bg-success');
                $('#goForwardButton').click();
            }
            if ((e.key == 's') || (e.key == 'S')) {
                let button = document.getElementById('goBackwardButton');
                button.classList.remove('bg-info');
                button.classList.add('bg-success');
                $('#goBackwardButton').click();
            }
            if ((e.key == 'a') || (e.key == 'A')) {
                let button = document.getElementById('turnLeftButton');
                button.classList.remove('bg-info');
                button.classList.add('bg-success');
                $('#turnLeftButton').click();
            }
            if ((e.key == 'd') || (e.key == 'D')) {
                let button = document.getElementById('turnRightButton');
                button.classList.remove('bg-info');
                button.classList.add('bg-success');
                $('#turnRightButton').click();
            }
            //console.log(e.key);
        });

        //Free key when stop pressing
        $(document).keyup(function (e) {
            if ((e.key == 'w') || (e.key == 'W')) {
                let button = document.getElementById('goForwardButton');
                button.classList.remove('bg-success');
                button.classList.add('bg-info');
                $('#goForwardButton').click();
            }
            if ((e.key == 's') || (e.key == 'S')) {
                let button = document.getElementById('goBackwardButton');
                button.classList.remove('bg-success');
                button.classList.add('bg-info');
                $('#goBackwardButton').click();
            }
            if ((e.key == 'a') || (e.key == 'A')) {
                let button = document.getElementById('turnLeftButton');
                button.classList.remove('bg-success');
                button.classList.add('bg-info');
                $('#turnLeftButton').click();
            }
            if ((e.key == 'd') || (e.key == 'D')) {
                let button = document.getElementById('turnRightButton');
                button.classList.remove('bg-success');
                button.classList.add('bg-info');
                $('#turnRightButton').click();
            }
            console.log(e.key);
        });
    }

    function init_right_tab_buttons() {
        $('#saveMapButton').click(function () {
            mapLength = $('#editMapLength').val();
            mapWidth = $('#editMapWidth').val();
            clearGridMap();
            init_map(mapLength, mapWidth, 20);
            init_map_layer();
            visualize_map_data(mapData);
            console.log('Saved map data');
        });
    }
    //Init Map view
    get_map_data();
    init_map(10, 20, 20);
    init_map_layer();
    render_map(mapData);
    //Init MQTT and control buttons
    init_mqtt();
    init_control_buttons();
    init_nav_button();
    init_right_tab_buttons();

})