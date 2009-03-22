
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG
ASSEMBLY = bin/Debug/MonoClient.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../libLastRip/bin/Debug/libLastRip.dll \
	../LockerPut/bin/Debug/LockerPut.dll
BUILD_DIR = bin/Debug

LOCKERPUT_DLL_SOURCE=../LockerPut/bin/Debug/LockerPut.dll
TAGLIB_SHARP_DLL_SOURCE=../taglib-sharp/taglib-sharp.dll
LOCKERPUT_DLL_MDB_SOURCE=../LockerPut/bin/Debug/LockerPut.dll.mdb
LOCKERPUT_DLL_MDB=$(BUILD_DIR)/LockerPut.dll.mdb
LIBLASTRIP_DLL_SOURCE=../libLastRip/bin/Debug/libLastRip.dll
THELASTRIPPER_PNG_SOURCE=TheLastRipper.png
LIBLASTRIP_DLL_MDB_SOURCE=../libLastRip/bin/Debug/libLastRip.dll.mdb
LIBLASTRIP_DLL_MDB=$(BUILD_DIR)/libLastRip.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/TheLastRipper.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../libLastRip/bin/Release/libLastRip.dll \
	../LockerPut/bin/Release/LockerPut.dll
BUILD_DIR = bin/Release

LOCKERPUT_DLL_SOURCE=../LockerPut/bin/Release/LockerPut.dll
TAGLIB_SHARP_DLL_SOURCE=../taglib-sharp/taglib-sharp.dll
LOCKERPUT_DLL_MDB=
LIBLASTRIP_DLL_SOURCE=../libLastRip/bin/Release/libLastRip.dll
THELASTRIPPER_PNG_SOURCE=TheLastRipper.png
LIBLASTRIP_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=.resources.dll

PROGRAMFILES = \
	$(LOCKERPUT_DLL) \
	$(TAGLIB_SHARP_DLL) \
	$(LOCKERPUT_DLL_MDB) \
	$(LIBLASTRIP_DLL) \
	$(THELASTRIPPER_PNG) \
	$(LIBLASTRIP_DLL_MDB)  

LINUX_DESKTOPAPPLICATIONS = \
	$(THELASTRIPPER_DESKTOP)  

BINARIES = \
	$(THELASTRIPPER)  


	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_DESKTOPAPPLICATIONS) $(BINARIES) 

FILES = \
	gtk-gui/generated.cs \
	gtk-gui/MainWindow.cs \
	About.cs \
	AssemblyInfo.cs \
	Main.cs \
	MainWindow.cs \
	Preferences.cs \
	Settings.cs \
	gtk-gui/MonoClient.About.cs \
	gtk-gui/MonoClient.Preferences.cs \
	CreateLockerAccount.cs \
	gtk-gui/MonoClient.CreateLockerAccount.cs 

DATA_FILES = \
	app.desktop \
	TheLastRipper.png 

RESOURCES = \
	gtk-gui/gui.stetic \
	logo.png \
	../AboutBox.png 

EXTRAS = \
	thelastripper.in \
	thelastripper.desktop.in 

REFERENCES =  \
	System \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	Mono.Posix

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_DESKTOPAPPLICATIONS) $(BINARIES) 

include $(top_srcdir)/Makefile.include

LOCKERPUT_DLL = $(BUILD_DIR)/LockerPut.dll
TAGLIB_SHARP_DLL = $(BUILD_DIR)/taglib-sharp.dll
THELASTRIPPER = $(BUILD_DIR)/thelastripper
LIBLASTRIP_DLL = $(BUILD_DIR)/libLastRip.dll
THELASTRIPPER_DESKTOP = $(BUILD_DIR)/thelastripper.desktop
THELASTRIPPER_PNG = $(BUILD_DIR)/TheLastRipper.png

$(eval $(call emit-deploy-target,LOCKERPUT_DLL))
$(eval $(call emit-deploy-target,TAGLIB_SHARP_DLL))
$(eval $(call emit-deploy-target,LOCKERPUT_DLL_MDB))
$(eval $(call emit-deploy-wrapper,THELASTRIPPER,thelastripper,x))
$(eval $(call emit-deploy-target,LIBLASTRIP_DLL))
$(eval $(call emit-deploy-wrapper,THELASTRIPPER_DESKTOP,thelastripper.desktop))
$(eval $(call emit-deploy-target,THELASTRIPPER_PNG))
$(eval $(call emit-deploy-target,LIBLASTRIP_DLL_MDB))


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
