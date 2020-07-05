# SpotifyToFoobar

Quick and dirty app to create a playlist from spotify to work with Foobar.

Enter in the playlist id - e.g if your playlist is https://open.spotify.com/playlist/1yYfrkXdN0F95uCHkbkCWo?si=1_COPTY3TWaEQ534QtrPQA, enter in 1yYfrkXdN0F95uCHkbkCWo.

The app will output a SpotifyToFoobar.m3u file in the debug folder.

Requires the foo_input_spotify plugin for foobar: https://github.com/FauxFaux/foo_input_spotify/wiki


Building from scratch:
Edit <code>_clientId = @"YOUR SPOTIFY DEV ID HERE";</code> with your developer spotify id
