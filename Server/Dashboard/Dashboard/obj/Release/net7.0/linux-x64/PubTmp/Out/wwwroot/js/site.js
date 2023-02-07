$(document).ready(function () {
    var mapName, mapLength, mapWidth, mapData;
    var coords = [];
    //Init, checking dashboard
    console.log('Loaded');
    //Checking camera
    $('#cameraStreamView').addClass('hide');
    if ($('#cameraSwitch').is(':checked')) {
        console.log('Camera ON');
        $('#cameraIndicator').html('ON');
    } else {
        console.log('Camera OFF');
        $('#cameraIndicator').html('OFF');
    }
    //
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

    document.getElementById('speedSlider').oninput = function () {
        console.log("Speed Slider = " + $('#speedSlider').val());
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
        let 
    }

    function init_control_key() {

    }

    init_map();
    get_map_data();
    visualize_map_data(mapData);

    init_mqtt();
    init_control_key();

})