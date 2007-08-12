#!/bin/sh
#!/bin/sh
cd $TARGET_BUILD_DIR
rm -rf TheLastRipper.app
cp -r ../../Main.nib .
macpack -m:2 -n:TheLastRipper -o:. -a:TheLastRipper.exe -r:/Library/Frameworks/Mono.framework/Versions/Current/lib/mono/cocoa-sharp/cocoa-sharp.dll -r:Main.nib

