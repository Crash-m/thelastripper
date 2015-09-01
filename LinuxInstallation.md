_There's many ways of installing TheLastRipper depending on you distro. Check if your distros repositories contain TheLastRipper, of not follow on of the instruction sets below._

### Installation under Ubuntu/Debian ###
_Follow these instructions if your distro is based of Debian or Ubuntu._

  1. Download the latest deb package from our downloads listing.
  1. Right click it and choose "install" or "open with gdebi"

You can also install it with following commands:
```
$ wget http://thelastripper.googlecode.com/files/thelastripper_1.1.0-1_all.deb
$ sudo dpkg -i thelastripper_1.1.0-1_all.deb
```

### Installation under Mandriva ###
_Follow these instructions if you are running Mandriva._

  1. Download the latest Mandriva RPM from our download listing
  1. Install the package, detailed instructions may be found here: [Mandriva docs: Installing and removing software](http://wiki.mandriva.com/en/Docs/Basic_tasks/Installing_and_removing_software).

### Installation under ArchLinux ###
_Follow these instructions if you are running ArchLinux._

You can use a tool like "aurbuild" to install. Simply type: "aurbuild -s thelastripper" and you will got the program installed on your system with all dependencies solved.

### Installation from source ###
_Follow these instructions if you cannot install one of the provided packages under your distribution of choice, make sure to check if a package is in your distros repository_

  1. Install requirements through the package manager of you distro:
    * xdg-utils (optional)
    * mono-runtime
    * libglib2.0-cli
    * libgtk2.0-cli
    * libmono-corlib2.0-cli
    * libmono-system2.0-cli
    * librsvg2-common
    * gnome-icon-theme
    * If more's needed the configure script will complain, if you are missing some try it since some things may be package diffently in you distro of choice.
  1. Download the latest tarball from out downloads listing.
  1. Extract the content of the tarball
  1. Run the `./configure` script
  1. Build the application with `make`
  1. Install the application with `make install`
  1. Launch the application with following command: `thelastripper` or using the menu in your desktop of choice.
