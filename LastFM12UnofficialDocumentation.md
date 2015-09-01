_Copyright 2007 by Tobias Brennecke (tburny), Jonas F. Jensen (Jop...), Matt Brown (blueboxed) and Moritz Strübe (morty).
Release under CreativeCommons Attribution Non-commercial Share Alike ([by-nc-sa](http://creativecommons.org/licenses/by-nc-sa/3.0/))._

# Introduction #
This document is to be used in compliment of existing documentation.  For more information, refer to the following pages: [Audioscrobbler Protocol v1.1](http://www.audioscrobbler.net/wiki/Protocol1.1.merged),  [Audioscrobbler Protocol v1.2](http://www.audioscrobbler.net/development/protocol/).
For web services 2.0 documentation, please see [Web services 2.0 documentation on last.fm](http://last.fm/api).

## Conventions Used in this Document ##
_In order to avoid redundancy, some conventions and abbreviations are used in this document._

### List of Conventions ###
  * **Optional Variables:** When a variable is nested in brackets, it is optional. For example, the post variable used with Handshake.php is optional, thus noted as `"[player]"`.
  * **Depreciated Values:** When a value is depreciated from versions previous to 1.2, it is noted in italics, and explicitly noted as depreciated to avoid confusion.  For example, the returned value of Handshake.php, stream\_url, is depreciated and is thus noted as "_stream\_url_ (**depreciated**)".  An explanation of the depreciation is included in the description of the value.

### List of Abbreviations ###
  * **kv**: Verified **known values** (acceptably posted or known returned values).


# Handshake #
_An intial handshake/login, consists of a request and a response._

**Please Note:** This handshake is for the old web services 1.0, but is also used nowadays for radio functions.

## Request ##
**Example:**
```
http://ws.audioscrobbler.com/radio/handshake.php?version=1.3.1.1&platform=win32&username=[USERNAME]&passwordmd5=[PASSWORDMD5_HASH]&language=de&player=[player]
```

**Post Variables:**
  * **version:** Version of the client.  kv: range from 1.0.0.0 - 1.3.1.1.  Known revision and build versions are usually 0 or 1 (e.g. "1.0.0.0" or "1.0.1.1") .
  * **platform:** The platform the client runs on.  kv: "win32" (windows), "linux" (linux), "mac", "unknown".
  * **username:** The name of the user.
  * **passwordmd5:** The MD5 Hash of the user's password.
  * **language:** The [ISO 639.1](http://www.loc.gov/standards/iso639-2/) language code used by the player.  Suspected to affect the language of the metadata.  kv: "cn", "de", "es", "fr", "it", "jp", "pl", "pt", "ru", "sv", "tr".
  * **player:** The player which will be used to play the Last.FM playlist or stream. This in only used on win kv: "winamp", "wmplayer"

## Response ##
**Example:**
```
    session=ae1eb54a11615e605d61d6e83dde71bc
    stream_url=http://87.117.229.85:80/last.mp3?Session=ae1eb54a11615e605d61d6e83dde71bc
    subscriber=0
    framehack=0
    base_url=ws.audioscrobbler.com
    base_path=/radio
    info_message=
    fingerprint_upload_url=http://ws.audioscrobbler.com/fingerprint/upload.php
```

**Results:**
  * **session:** The session ID for the current session.  This is what associates the Last.FM username with the IP address of the device on the AudioScrobbler system.
  * **_stream\_url_ (depreciated):** The URL of the Last.FM ShoutCast/MP3 stream. Beginning version 1.2, **this is no longer current.**  Refer to section "Request Playlist."
  * **subscriber:** Indicates if the user submitted to handshake.php is a subscriber.  kv: "0" (not a subscriber), "1" (a subscriber).
  * **framehack:** Unknown.
  * **base\_url:** The address to which all future HTTP posts should be sent.  See base\_path.
  * **base\_path:** The root path, as related to the base\_url, to which all future HTTP posts should be sent (e.g. the functions for playing and controlling the stream).  See base\_url.
  * **info\_message:** Unknown.  Most likely a vehicle to send messages to the client.
  * **fingerprint\_upload\_url:** Post URL for Last.fm fingerprinting system.

# Second Handshake/Scrobbler Handshake #
_This handshake is new in the 1.2 version of the last.fm protocol, **it's not needed if you only want to request an XSPF.**_

## Request ##
**Example:**
```
    http://post.audioscrobbler.com/?hs=true&p=1.2&c=ass&v=1.3.1.1&u=[USERNAME]&t=[TIMESTAMP]&a=[Token] 
```

**Post Variables:**
  * **hs:** Indicates whether a handshake was completed successfully.  kv: "true" (hardcoded in the client)
  * **p:** Protocol version.  kv: "1.2".
  * **c:** Client ID, of which a list of registered clients is maintained by AudioScrobbler, see appendix B. Clients which have not been allocated an identifier should use the identifier `tst`, with a version number of 1.0. Do not distribute code which uses the experimental identifier. Do not use the identifiers for other clients. Client authors should use the `tst` identifier for clients which are not publicly distributed.
  * **v:** Version of the client noted in c. kv: "1.3.1.1" (version as related to Client ID 'ass').
  * **Username:** Username.
  * **Timestamp:** [Unix timestamp](http://en.wikipedia.org/wiki/Unix_time) of the current time, e.g 1185443235 if the current date & time is the 26.7.2007 11:47:15.
  * **Token:** md5( md5(password) + Timestamp), An md5 sum of an md5 sum of the password, plus the timestamp as salt.


## Response ##
**Example:**
```
    OK
    be290d5d491e1bedb21e0c35f74ce96b
    http://post.audioscrobbler.com:80/np_1.2
    http://87.117.229.205:80/protocol_1.2
```

**Results:**
  1. The first line indicates whether the previous post was successful.  kv: "OK", "FAILED".  Note there may be other values returned.
  1. The second line indicates session id (a.k.a session key).
  1. The third line contains the URL to post Now Playing information.
  1. The fourth line contains the URL to post unknown information.  **The usage of this is unknown.**


# Adjusting Radio Station #
_Adjustment of radio station is still done the same way as it was in the 1.1 protocol._

## Request ##
**Example:**
```
    http://[base_url][base_path]/adjust.php?session=[sessionID]&url=[LASTFMURI]&lang=[LANGUAGE]
```

**Post Variables:**
  * **base\_url:** Given as part of the response to Handshake.php; explicitly "base\_url".
  * **base\_path:** Given as part of the response to Handshake.php; explicitly "base\_path".
  * **sessionID:** Given as part of the response to Handshake.php; explicitly "session".
  * **url:** The Last.FM URI for which you want to tune in. Refer to Appendix A for more info.
  * **lang:** Value is equal to an ISO 639.1 language code.  kv: "en" (English), "de" (German).


## Response ##
The following response indicates that the information posted to Adjust.php was accepted:
```
    OK
```

The following response indicates that the information posted to Adjust.php was rejected:
```
    FAILED
```

# Requesting an XSPF #
_This feature is new in version 1.2._

For more information on the XSPF (XML Sharable Playlist File) format created by Xiph OSC, please refer to the official [XSPF documentation](http://www.xspf.org/) (http://www.xspf.org/).

## Request ##
**Example:**
```
    http://[base_url][base_path]/xspf.php?sk=[SESSIONID]&discovery=0&desktop=1.5.1
```


**Post Variables:**
  * **base\_url:** Given as part of the response to Handshake.php; explicitly "base\_url".
  * **base\_path:** Given as part of the response to Handshake.php; explicitly "base\_path".
  * **sk:** The session ID of the established session. Refer to section "Handshake".
  * **discovery:** Indicates whether you'd like the XSPF to be generated using Discovery mode.  kv: "0" (Discovery mode off), "1" (Discovery mode on).
  * **desktop:** Version of the client.


## Response ##
The response is an XSPF.  It conforms to all of the require XSPF tags specified in the XSPF standard; but also contains the following additional tags:
`lastfm:trackauth`, `lastfm:albumId`, `lastfm:artistId`.  Additionally, the playlist tag is written incorrectly.

The presence of these additional tags, and the incorrectly written playlist tag, make the XSPFs generated not compliant with the XSPF standard.

Please read below the following example for additional information on the specific tags as they relate to Last.FM.


**Example of an XSPF returned by xspf.php:**
```
<playlist version="1" xmlns:lastfm="http://www.audioscrobbler.net/dtd/xspf-lastfm">
<title>+%C3%84hnliche+K%C3%BCnstler+wie+Meat+Loaf</title>
<creator>Last.fm</creator>
<link rel="http://www.last.fm/skipsLeft">6</link>
<trackList>
    <track>
        <location>http://kingpin4.last.fm/user/d87034ad6ab5d7ed388a6dcd2d2df506.mp3</location>
        <title>Killer Queen (Live)</title>
        <id>3647300</id>
        <album>Live Killers</album>
        <creator>Queen</creator>
        <duration>118000</duration>
        <image>http://images.amazon.com/images/P/B000000OAP.01._SCMZZZZZZZ_.jpg</image>
        <lastfm:trackauth>15fcf</lastfm:trackauth>
        <lastfm:albumId>2553849</lastfm:albumId>
        <lastfm:artistId>1270</lastfm:artistId>       
        <link rel="http://www.last.fm/artistpage">http://www.last.fm/music/Queen</link>
        <link rel="http://www.last.fm/albumpage">http://www.last.fm/music/Queen/Live+Killers</link>
        <link rel="http://www.last.fm/trackpage">http://www.last.fm/music/Queen/_/Killer+Queen+%28Live%29</link>
        <link rel="http://www.last.fm/buyTrackURL">http://www.last.fm/affiliate_sendto.php?link=uapc&amp;prod=731771&amp;pos=9b944892b67cd2d9f7d9da1c934c5428</link>
        <link rel="http://www.last.fm/buyAlbumURL"></link>
        <link rel="http://www.last.fm/freeTrackURL"></link>
    </track>
    <track>
        <location>http://kingpin4.last.fm/user/4c0ae6e135aa0d4c4926fa29a633911e.mp3</location>
        <title>Going, Going... Home</title>
        <id>4195329</id>
        <album></album>
        <creator>Mike &amp; The Mechanics</creator>
        <duration>269000</duration>
        <image></image>
        <lastfm:trackauth>86776</lastfm:trackauth>
        <lastfm:albumId></lastfm:albumId>
        <lastfm:artistId>1000520</lastfm:artistId>       
        <link rel="http://www.last.fm/artistpage">http://www.last.fm/music/Mike%2B%2526%2BThe%2BMechanics</link>
        <link rel="http://www.last.fm/albumpage"></link>
        <link rel="http://www.last.fm/trackpage">http://www.last.fm/music/Mike%2B%2526%2BThe%2BMechanics/_/Going%2C+Going...+Home</link>
        <link rel="http://www.last.fm/buyTrackURL"></link>
        <link rel="http://www.last.fm/buyAlbumURL"></link>
        <link rel="http://www.last.fm/freeTrackURL"></link>
    </track>
	...
	...
	...
</trackList>
</playlist>
```

**Detailed Explanation:**

This is a playlist. its version is "1"....
`<title>` says to the app which title the playlist has
`<creator>` says the creator is "last.fm"(in this case).
Now the first really interesting thing:
The content of the `<link>` tag says after how many skips(or plays of songs) the playlist has to be reloaded.

Now let's take a look on one of the track nodes.
It contains (tag identified here by xpath):
  * playlist/title = title of the radio station
    * playlist/creator = creator of the playlist, usually last.fm
    * playlist/link?rel="http://www.last.fm/skipsLeft" = Number of tracks on this playlist
      * playlist/trackList/track/location = The location of the mp3 file/track->where can the client download it
      * playlist/trackList/track/title = The track title
      * playlist/trackList/track/id = The id of the track in the last.fm music catalog, could be used for adding this track to the personal playlist(I'll say a few words to this topic later in the text)
      * playlist/trackList/track/album = The album name
      * playlist/trackList/track/creator = The Artist
      * playlist/trackList/track/duration = The duration, in milliseconds(really nonsense, except for id3 tagging maybe :P)
      * playlist/trackList/track/image = Url to the album picture
      * playlist/trackList/track/lastfm:trackauth = 5-hexdigit Last.fm recommendation key (appended to the 'L' source ID when submitting track data via Audioscrobbler Protocol v1.2)
      * playlist/trackList/track/lastfm:albumId = Id of the album. seems to be worthless
      * playlist/trackList/track/lastfm:artistId = same thing as with the album
    * links to last.fm's artist-, album- and track pages
    * a url from where you can buy the track
    * another one for the album
    * freeTrackurl: If the song is downloadable for free, this is the download link

# Test User/Pass #
This way you can check, whether a user/pass is valid.

## Request ##
**Example:**
```
http://ws.audioscrobbler.com//ass/pwcheck.php?time=[TIMESTAMP]&username=[USERNAME]&auth=[AUTH1]&auth2=[AUTH2]&defaultplayer=[PLAYER]
```


**Post Variables:**
  * **Timestamp:** Unix timestamp of the current time, e.g 1185443235 if the current date & time is the 26.7.2007 11:47:15.
  * **Username:** Username.
  * **Auth1:** md5( md5(password) + Timestamp), An md5 sum of an md5 sum of the password, plus the timestamp as salt.
  * **Auth2:** Second possible Password. The client uses md5( md5(toLower(password)) + Timestamp)
  * **Player:** See Appendix

## Response ##
The following response indicates that the information posted to Adjust.php was accepted:
```
OK-8
```

Response may contain the following strings:
  * **OK2:** Auth2 is valid
  * **OK:** Auth1 is valid
  * **INVALIDUSER** User unknown
  * **BADPASSWORD** Password not valid
  * **BOOTSTRAP** No idea!


# Appendix A: List of Last.FM URIs #
  * lastfm://user/$username/personal
  * lastfm://user/$username/playlist
  * lastfm://artist/$artistname or lastfm://artist/$artistname/similarartists
  * lastfm://globaltags/$tag
  * lastfm://group/$groupname
  * lastfm://user/$username/neighbours
  * lastfm://user/$username/recommended/100
  * lastfm://play/tracks/$trackid,$trackid,$trackid


# Appendix B: List of Accepted Client Names #
  * AmigaAMP = ami
  * Applescriptable MacOS X Application (iTunes) = osx
  * Audacious = aud
  * BMPx = mpx
  * FooBar = foo
  * Herrie = her
  * MusicMatch Jukebox = mmj
  * QCD = qcd
  * SliMP3 = slm
  * Winamp2 = wa2
  * Winamp3 = wa3
  * Windows Media Player = wmp
  * XMMS = xms
  * XMMS2 = xm2