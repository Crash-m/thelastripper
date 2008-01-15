@echo off
:loop
ConsoleClient.exe  -username=hans -password=geheim -musicpath=c:/last.fm/collection -quarantine=c:/last.fm/_quarantine -radiostation=lastfm://user/hans/playlist -skipnew=true -skipexisting=true
goto loop
