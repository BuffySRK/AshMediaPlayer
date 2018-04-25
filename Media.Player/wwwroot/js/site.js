// Write your JavaScript code.
// Get a handle to the player
player = document.getElementById('MediaPlayer');
btnPlayPause = document.getElementById('btnPlayPause');
progressBar = document.getElementById('progressBar');

progressBar.addEventListener("click", seek);
player.addEventListener('timeupdate', updateProgressBar, false); // Add a listener for the timeupdate event so we can update the progress bar, set useCapture to false so it doesnt bubble.

// Update the progress bar
function updateProgressBar() {
    // Work out how much of the media has played via the duration and currentTime parameters
    var percentage = Math.floor((100 / player.duration) * player.currentTime);
    // Update the progress bar's value
    progressBar.value = percentage;
    // Update the progress bar's text (for browsers that don't support the progress element)
    progressBar.innerHTML = percentage + '% played';
}

function seek(e) {
    var percent = e.offsetX / this.offsetWidth;
    player.currentTime = percent * player.duration;
    e.target.value = Math.floor(percent / 100);
    e.target.innerHTML = progressBar.value + '% played';
}


function playMedia() {
    player.play();
}

function pauseMedia() {
    player.pause();
}

player.addEventListener('ended', function () { this.pause(); }, false);

// Stop the current media from playing, and return it to the start position
function stopMedia() {
    player.pause();
    if (player.currentTime) {
        player.currentTime = 0;
    }

    switchPlayPauseButton(btnPlayPause, 'play');
}