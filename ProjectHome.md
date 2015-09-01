# TheLastRipper no longer works #
It's been coming for years now, and last.fm has finally shut-down their old radio API. This means that TheLastRipper will not work anymore, and with no efforts to make it work again. It is probably fair to declare the project **dead**.

For more information see:
http://jonasfj.dk/blog/2012/12/the-end-of-thelastripper/


---


TheLastRipper can save Last.fm streams to mp3's, while downloading album cover, appending ID3v2 tags and organizing you music after Artist/Album/Track. TheLastRipper will also help you generate playlists from the data available from you Last.fm account. TheLastRipper requires a Last.fm login, you can regsiter for free at http://last.fm/

**Please note** that recordings of radio streams, may NOT be legal, we recommend that you investegate your local laws, before using TheLastRipper.

**Features**
  * Recording Last.fm streams to mp3's
  * Organize your music in directories: Artist/Album/Track/
  * Downloads coverart
  * Appends ID3v2 tags
  * Generates playlist
    * M3U
    * SMIL
    * PlS
  * Support proxy settings

## OS X developer needed ##
Currently the project is lacking an active OS X developer and as such there's currently no support for leopard. TheLastRipper is written in C# and running on Mono on OS X. Most of the codebase is already written or ported from another platform. Currently we need someone who'd want to maintain the OS X release once in a while. Knowledge of OS X and C# would be a good thing, but only determination to learn and have fun is required. If you think this could be fun don't hesitate to mail me (jopsen@gmail.com) or leave a comment at [issue 83](https://code.google.com/p/thelastripper/issues/detail?id=83). I'll gladly assist development, though I have limited understanding of OS X.

## New Release 1.4.0 ##
Lately some last.fm changes have course some instabilities, however, updating some variables in our requests have led to more recorder. Thanks to Andreas for pulling of this release, I promise that the current beta version of 1.3.0 for Linux will evolve into a 1.4.0 release really soon this time. New features in release 1.4.0, includes:
  * Song health calculation.
  * Updated interfacing of Last.fm for greater stability ([Issue 149](https://code.google.com/p/thelastripper/issues/detail?id=149)).
  * MP3Tunes integration, automatically backup tracks at mp3tunes.
  * Fixed problems with international chars [Issue 120](https://code.google.com/p/thelastripper/issues/detail?id=120)
  * Many bugfixes including [issue 133](https://code.google.com/p/thelastripper/issues/detail?id=133) and [issue 95](https://code.google.com/p/thelastripper/issues/detail?id=95).
  * Other minor features...

## Release 1.3.0 ##
Currently only available for windows as the Linux developer (me) is currently occupied with other projects. I promise a Linux version too sometime, but thanks to Andreas for pulling off this release.
  * New GUI station selection improvements.
  * Using the new last.fm protocol version 1.2 (Skip now works!).
  * Additional management features like skip songs already recorded.
  * A few other minor details too.