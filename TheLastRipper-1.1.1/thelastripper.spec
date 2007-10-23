%define name thelastripper
%define version 1.1.1
%define release %mkrel 1

Summary:	An audio stream ripper for Last.fm
Name:		%{name}
Version:	%{version}
Release:	%{release}
License:	GPL
Group:		Sound
URL:		http://thelastripper.com/
Source0:	http://thelastripper.googlecode.com/files/%{name}-%{version}.tar.bz2
Source1:   	%{name}.png.bz2
BuildRoot:	%{_tmppath}/%{name}-buildroot
Requires:       mono >= 1.2.5
Requires:       gtk-sharp2 >= 2.10.2
Requires:       librsvg >= 2.18.2
Requires:       xdg-utils
BuildRequires:  libmono-devel >= 1.2.5
BuildRequires:  gnome-sharp2 >= 2.16.0
BuildArch:      noarch

%description
TheLastRipper can save Last.fm streams to mp3's, while downloading 
album cover, appending ID3v1 tags and organizing you music after 
Artist/Album/Track. TheLastRipper will also help you generate playlists 
from the data available from you Last.fm account. 

TheLastRipper requires a Last.fm login, you can regsiter for free 
at http://last.fm/

Please note that recordings of radio streams, may NOT be legal, 
we recommend that you investegate you local laws, before using 
TheLastRipper.

%prep
%setup -q

%build
%configure --prefix="${RPM_BUILD_ROOT}/%{_prefix}"
%make
rm -rf ${RPM_BUILD_ROOT}

%install
%makeinstall

# Remove broken XDG file
rm -f ${RPM_BUILD_ROOT}%{_datadir}/applications/%{name}.desktop
rm -f ${RPM_BUILD_ROOT}%{_libdir}/%{name}/TheLastRipper.png

# XDG Menu
mkdir -p ${RPM_BUILD_ROOT}%{_datadir}/applications
cat > ${RPM_BUILD_ROOT}%{_datadir}/applications/mandriva-%{name}.desktop << EOF
[Desktop Entry]
Name=TheLastRipper
Comment=TheLastRipper records Last.FM radiostreams to MP3
Exec=%{_bindir}/%{name}
Icon=%{name}
Terminal=false
Type=Application
StartupNotify=false
Encoding=UTF-8
Categories=AudioVideo;Audio;
EOF

# Menu
mkdir -p ${RPM_BUILD_ROOT}%{_menudir}
cat << EOF > ${RPM_BUILD_ROOT}%{_menudir}/%{name}
?package(%{name}):needs="X11" \
title="TheLastRipper" \
longtitle="TheLastRipper records Last.FM radiostreams to MP3" \
icon="%{name}.png" \
section="Multimedia/Sound" \
xdg="true" \
command="%{_bindir}/%{name}"
EOF

# Icons
mkdir -p ${RPM_BUILD_ROOT}{%_liconsdir,%_iconsdir,%_miconsdir}
bzcat %{SOURCE1} > ${RPM_BUILD_ROOT}%{_liconsdir}/%{name}.png
convert -scale 32 ${RPM_BUILD_ROOT}%{_liconsdir}/%{name}.png %{buildroot}%{_iconsdir}/%{name}.png
convert -scale 16 ${RPM_BUILD_ROOT}%{_liconsdir}/%{name}.png %{buildroot}%{_miconsdir}/%{name}.png

#Remove broken wrapper
rm -f ${RPM_BUILD_ROOT}%{_bindir}/%{name}

#Create new wrapper
cat << EOF > ${RPM_BUILD_ROOT}%{_bindir}/%{name}
#!/bin/sh

exec mono "%{_libdir}/%{name}/TheLastRipper.exe" "\$@"

exit
EOF

%post
%{update_menus}
%{update_desktop_database}

%postun
%{clean_menus}
%{clean_desktop_database}

%clean
rm -rf ${RPM_BUILD_ROOT}

%files
%defattr(-,root,root)
%attr(755,root,root) %{_bindir}/%{name}
%{_datadir}/applications/mandriva-%{name}.desktop
%{_menudir}/%{name}
%{_iconsdir}/%{name}.png
%{_liconsdir}/%{name}.png
%{_miconsdir}/%{name}.png
%dir %{_libdir}/%{name}
%{_libdir}/%{name}/TheLastRipper.exe
%{_libdir}/%{name}/libLastRip.dll
%{_libdir}/%{name}/taglib-sharp.dll

%changelog
* Tue Oct 23 2007 Maxim Heijndijk <maccus at orange dot nl> 1.1.1-1mdv2008.0
- Bump to 1.1.1
- XDG category
- Remove devel-package

* Sat Jun 30 2007 Maxim Heijndijk <maccus at orange dot nl> 1.0.2-1mdv2007.0
- Initial wrap for Mandriva
- First build for RPM based distro
