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


})