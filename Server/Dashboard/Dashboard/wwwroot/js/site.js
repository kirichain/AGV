$(document).ready(function () {
    var mapName, mapLength, mapWidth, mapData;
    var client;
    var agvSpeed, agvId, agvMode;
    var coords = [];
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
            $('#cameraStreamView').attr('src', 'https://1e09-203-113-151-208.ap.ngrok.io/stream');
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
    //Init map
    function init_map() {
        let innerHtml = '';
        for (let i = 0; i < 20; i++) {
            innerHtml += '<tr class="m-0 p-0">';
            for (let j = 0; j < 20; j++) {
                innerHtml += '<td><div class="squareCell border-end border-bottom border-secondary m-0 p-0" id="' + j + ':' + i + '"></div></td>';
            }
            innerHtml += '</tr>';
        }
        $('#gridMap').append(innerHtml);
        console.log('Init map done');
    }

    //Get map data from API server
    function get_map_data() {
        let jsonRepsonse = '{"name":"Warehouse 1", "length":4, "width":4, "coordinates":[{"x":"0", "y":"0", "value":"blank"},{"x":"1", "y":"0", "value":"package"},{"x":"2", "y":"0", "value":"blank"},{"x":"3", "y":"0", "value":"blank"},{"x":"0", "y":"1", "value":"blank"},{"x":"1", "y":"1", "value":"blank"},{"x":"2", "y":"1", "value":"blank"},{"x":"3", "y":"1", "value":"blank"},{"x":"0", "y":"2", "value":"blank"},{"x":"1", "y":"2", "value":"blank"},{"x":"2", "y":"2", "value":"blank"},{"x":"3", "y":"2", "value":"blank"},{"x":"0", "y":"3", "value":"blank"},{"x":"1", "y":"3", "value":"blank"},{"x":"2", "y":"3", "value":"package"},{"x":"3", "y":"3", "value":"package"}]}';

        mapData = jsonRepsonse;
        console.log('Get map data done');
    }

    function visualize_map_data(data) {
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

                    if (coord.value == 'package') {
                        //console.log('id = ' + x + ':' + y);
                        let cell = document.getElementById(x + ':' + y);
                        cell.classList.add("bg-info");
                    }
                }
                coordIndex++;
            }
        }

        console.log('Visualize data done');
        //console.log(_mapData);
        //console.log(coords);
    }

    function init_mqtt() {
        let mqttBrokerUrl = 'ws://pirover.xyz:9001';
        client = mqtt.connect(mqttBrokerUrl);

        client.on("connect", function () {
            client.subscribe("agv/control/001");
            client.subscribe("agv/position/001");
            client.subscribe("agv/status/001");
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
            msg = {
                direction: message,
                speed: agvSpeed
            }
            client.publish(topic, JSON.stringify(msg));
        }
        console.log('Sent ' + JSON.stringify(msg));
    }

    function init_control_buttons() {
        //For clicking button
        $('#goForwardButton').click(function () {
            mqtt_publish('agv/control/001', 'forward', 'move');
            console.log('Go Forward');
        });

        $('#goBackwardButton').click(function () {
            mqtt_publish('agv/control/001', 'backward', 'move');
            console.log('Go Backward');
        });

        $('#turnLeftButton').click(function () {
            mqtt_publish('agv/control/001', 'left', 'move');
            console.log('Turn Left');
        });

        $('#turnRightButton').click(function () {
            mqtt_publish('agv/control/001', 'right', 'move');
            console.log('Turn Right');
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

    //Init Map view
    init_map();
    get_map_data();
    visualize_map_data(mapData);
    //Init MQTT and control buttons
    init_mqtt();
    init_control_buttons();
})