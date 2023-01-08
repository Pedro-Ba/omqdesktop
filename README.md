# omqdesktop

This was a short project done in two days mostly to mess with C# for the first time, and to try learning an API. This uses the osu! API to retrieve the top 100 scores of an user (through their ID). Then, it keeps the links to the beatmap preview, the beatmap cover, and keeps the title and artist saved as strings in a list. 

The song is played through NAudio as fire-and-forget (except for the transitions which are await'ed through a Task); the guess autocomplete is off the top 100 you have, so some maps may show up more than once (different mapsets or different difficulties) but choosing any of them is fine (Except for cases where one is TV size and the other is full, in which case, I hope the preview is kind to you!).

Some songs may not play due to being removed from the preview in the website.

You should see that the "client_id" and "client_secret" section in Form1.cs isn't filled in, since that's my personal credentials. Insert your own when compiling. I'll probably add the option to do that in app later.
