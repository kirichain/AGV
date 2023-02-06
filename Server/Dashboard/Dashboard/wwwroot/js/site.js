$(document).ready(function () {
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
    //
    //Init map
    function init_map() {
        //const map = document.createElement("table");
        //var innerHtml = '';
        //map.id = "gridMapTable";
        ////let i = 2;
        //for (let i = 0; i < 20; i++) {
        //    innerHtml += '<tr class="m-0 p-0" id="x-1">';
        //    for (let j = 0; j < 50; j++) {
        //        innerHtml += '<td class="border-end border-bottom border-secondary m-0" id="cell-' + j + '"></td>';
        //    }
        //    innerHtml += '</tr>';
        //}

        //console.log(innerHtml);
        //console.log(map.innerHTML);

        //map.innerHTML = innerHtml;
        //console.log(map.innerHTML);
        //document.getElementById('gridMap').appendChild(map);
        let innerHtml = '';
        for (let i = 0; i < 20; i++) {
            innerHtml += '<tr class="m-0 p-0" id="x-1">';
            for (let j = 0; j < 40; j++) {
                innerHtml += '<td><div class="squareCell border-end border-bottom border-secondary m-0 p-0"></div></td>';
            }
            innerHtml += '</tr>';
        }
        console.log(innerHtml);
        $('#gridMap').append(innerHtml);

        console.log('done');
    }

    init_map();
})