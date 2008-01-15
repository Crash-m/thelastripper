#!/bin/sh
# Last.fm repeated rip
# You shoud use more than one radiostation so ripping wont stop if one station is ripped off!
# lastfm://globaltags/deutsch
# lastfm://group/Deutscher%20HipHop%20mit%20Niveau
i=1
while [ 1 = 1 ]
do
  echo run number $i
  i=`expr $i + 1`
  ./ConsoleClient.exe -musicpath=C:/last.fm/collection -username=hans -password=secret -radiostation=lastfm://user/cyclon1978/playlist
  ./ConsoleClient.exe -musicpath=C:/last.fm/collection -username=hans -password=secret -radiostation=lastfm://globaltags/deutsch
  ./ConsoleClient.exe -musicpath=C:/last.fm/collection -username=hans -password=secret -radiostation=lastfm://globaltags/Hoerspiel
done

