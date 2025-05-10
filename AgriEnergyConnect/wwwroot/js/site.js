// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let EMPTY_STRING = "";

// Parameters:
//      containerElem: The element that will hold the animation playback.
//      frameStart: The first image to diplay.
//      frameEnd: The second image to display.
//      durationStart: The duration of the first image.
//      durationEnd: The duration of the second image.
function playAnimation(containerElem, frameStart, frameEnd, durationStart, durationEnd) {

    let counter1 = 0;
    let counter2 = 0;
    let changeImage = true;

    containerElem.style.backgroundImage = "url('" + frameStart + "')";

    let interval1 = setInterval(function () {

        if (counter1 < durationStart) {
            containerElem.style.opacity = 1 - (counter1 / durationStart);
            counter1++;
        }

        if (counter1 == durationStart && counter2 < durationEnd) {
            if (changeImage) {
                containerElem.style.backgroundImage = "url('" + frameEnd + "')";
                changeImage = false;
            }

            containerElem.style.opacity = (counter2 / durationEnd);
            counter2++;
        }

        if (counter1 == durationStart && counter2 == durationEnd) {
            clearInterval(interval1);
        }

    }, 1);
}

function playAnimation2(containerElem1, containerElem2, frameStart, frameEnd, durationStart, durationEnd) {
    let counter1 = 0;
    let counter2 = 0;

    containerElem1.style.opacity = 1;
    containerElem2.style.opacity = 1;
    containerElem1.style.backgroundImage = "url('" + frameStart + "')";
    containerElem2.style.backgroundImage = "url('" + frameEnd + "')";

    let interval1 = setInterval(function () {

        if (counter1 < durationStart) {
            containerElem1.style.opacity = 1 - (counter1 / durationStart);
            counter1++;
        }

        if (counter2 < durationEnd) {
            containerElem2.style.opacity = (counter2 / durationEnd);
            counter2++;
        }

        if (counter1 == durationStart && counter2 == durationEnd) {
            clearInterval(interval1);
        }

    }, 1);
}

function playSlideShow(imageDisplay1, imageDisplay2, frames, duration, bRepeat) {
    let imageIndex = 0;

    let interval = setInterval(function () {

        if (imageIndex < (frames.length - 1)) {
            playAnimation2(imageDisplay1, imageDisplay2, frames[imageIndex], frames[imageIndex + 1], 1000, 1000);
            imageIndex++;
        }
        else {
            imageIndex = 0;
            playAnimation2(imageDisplay1, imageDisplay2, frames[frames.length - 1], frames[imageIndex], 1000, 1000);
        }

        if (!bRepeat) {
            clearInterval(interval);
        }

    }, duration);
}

function GET(url, header, e) {
    let request = new XMLHttpRequest();
    request.open("GET", url, true);

    if (header != null) {
        for (let i = 0; i < header.length; i++) {
            request.setRequestHeader(header[i][0], header[i][1]);
        }
    }

    request.send();

    if (e != null) {
        request.onloadend = function () {
            e(request);
        }
    }
}

function POST(url, data, header, e) {
    let request = new XMLHttpRequest();
    request.open("POST", url, true);

    if (header != null) {
        for (let i = 0; i < header.length; i++) {
            request.setRequestHeader(header[i][0], header[i][1]);
        }
    }

    request.send(data);

    if (e != null) {
        request.onloadend = function () {
            e(request.status);
        }
    }
}

// This method checks if the given password is strong enough.
function IsStrongPassword(password) {
    let result = false;
    let letters = "abcdefghijklmnopqrstuvwxyz";
    let numbers = "0123456789";

    let letterCount = 0;
    let digitCount = 0;
    let symbolCount = 0;

    if (password.length >= 8) {
        for (i = 0; i < password.length; i++) {
            if (letters.includes(password.toLowerCase()[i])) {
                letterCount++;
            }

            if (numbers.includes(password.toLowerCase()[i])) {
                digitCount++;
            }

            if (!letters.includes(password.toLowerCase()[i]) && !numbers.includes(password.toLowerCase()[i])) {
                symbolCount++;
            }
        }

        if (letterCount >= 4 && digitCount >= 2 && symbolCount >= 2)
            result = true;
    }

    return result;
}