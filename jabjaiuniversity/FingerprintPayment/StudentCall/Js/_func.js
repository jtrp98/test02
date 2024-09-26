
var context = null;
var source = null;
var audioBuffer = null;
//var s1, s2;
//window.onload = function () {
//    //context = new AudioContext();
//    init();
//}


function init() {
    if (!window.AudioContext) {
        if (!window.webkitAudioContext) {
            alert("Your browser does not support any AudioContext and cannot play back this audio.");
            return;
        }
        window.AudioContext = window.webkitAudioContext;
    }
    else
        context = new AudioContext();

    //s1 = context.createBufferSource();
    //s2 = context.createBufferSource();
    //s2.onended = s2Onnended;   
}


var bufferToBase64 = function (buffer) {
    var bytes = new Uint8Array(buffer);
    var len = buffer.byteLength;
    var binary = "";
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
};
var base64ToBuffer = function (buffer) {
    //decodeURIComponent(escape(window.atob(str)));
    //buffer =  buffer.replace(/\s/g, '');    
    var binary = window.atob(buffer);// window.atob(buffer);
    var buffer = new ArrayBuffer(binary.length);
    var bytes = new Uint8Array(buffer);
    for (var i = 0; i < buffer.byteLength; i++) {
        bytes[i] = binary.charCodeAt(i) & 0xFF;
    }
    return buffer;
};
//function stopSound() {
//    if (source) {
//        source.stop(0);
//    }
//}
//function playSound() {
//    // source is global so we can call .stop() later.
//    source = context.createBufferSource();
//    source.buffer = audioBuffer;
//    source.loop = false;
//    source.connect(context.destination);
//    source.start(0); // Play immediately.
//}

//function play(audio, callback) {
//    audio.play();
//    if (callback) {
//        //When the audio object completes it's playback, call the callback
//        //provided      
//        audio.addEventListener('ended', callback);
//    }
//}

function initSound(arrayBuffer) {
    var base64String = bufferToBase64(arrayBuffer);
    var audioFromString = base64ToBuffer(base64String);
    //document.getElementById("encodedResult").value = base64String;
    context.decodeAudioData(audioFromString, function (buffer) {
        // audioBuffer is global to reuse the decoded audio later.
        audioBuffer = buffer;
        //var buttons = document.querySelectorAll('button');
        //buttons[0].disabled = false;
        //buttons[1].disabled = false;
    }, function (e) {
        console.log('Error decoding file', e);
    });
}
