// Write your JavaScript code.
// Get a handle to the player
player = document.getElementById('MediaPlayer');
btnPlayPause = document.getElementById('btnPlayPause');
progressBar = document.getElementById('progressBar');

// Add a listener for the timeupdate event so we can update the progress bar
player.addEventListener('timeupdate', updateProgressBar, false);

// Add a listener for the play and pause events so the buttons state can be updated
player.addEventListener('play', function () {
    // Change the button to be a pause button
    switchPlayPauseButton(btnPlayPause, 'pause');
}, false);

player.addEventListener('pause', function () {
    // Change the button to be a play button
    switchPlayPauseButton(btnPlayPause, 'play');
}, false);

player.addEventListener('ended', function () { this.pause(); }, false);

progressBar.addEventListener("click", seek);

function seek(e) {
    var percent = e.offsetX / this.offsetWidth;
    player.currentTime = percent * player.duration;
    e.target.value = Math.floor(percent / 100);
    e.target.innerHTML = progressBar.value + '% played';
}

function playPauseVideo() {
    if (player.paused || player.ended) {
        // Change the button to a pause button
        switchPlayPauseButton(btnPlayPause, 'pause');
        player.play();
    }
    else {
        // Change the button to a play button
        switchPlayPauseButton(btnPlayPause, 'play');
        player.pause();
    }
}

// Stop the current media from playing, and return it to the start position
function stopMedia() {
    player.pause();
    if (player.currentTime) {
        player.currentTime = 0;
    }

    switchPlayPauseButton(btnPlayPause, 'play');
}

// Update the progress bar
function updateProgressBar() {
    // Work out how much of the media has played via the duration and currentTime parameters
    var percentage = Math.floor((100 / player.duration) * player.currentTime);
    // Update the progress bar's value
    progressBar.value = percentage;
    // Update the progress bar's text (for browsers that don't support the progress element)
    progressBar.innerHTML = percentage + '% played';
}

// Updates a button's title and CSS class
function switchPlayPauseButton(btn, value) {
    btn.title = value;
    if (btn.title === 'pause') {
        btn.className = "pause glyphicon glyphicon-pause";
    }
    else if (btn.title === 'play') {
        btn.className = "play glyphicon glyphicon-play";
    }

}

function resetMedia() {
    stopMedia();
    progressBar.value = 0;
    // Move the media back to the start
    player.currentTime = 0;
    // Set the play/pause button to 'play'
}