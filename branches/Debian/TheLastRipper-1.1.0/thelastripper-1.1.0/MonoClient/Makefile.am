
EXTRA_DIST = 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/MonoClient.exe
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../libLastRip/bin/Debug/libLastRip.dll
BUILD_DIR = ./bin/Debug
endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/TheLastRipper.exe
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../libLastRip/bin/Release/libLastRip.dll
BUILD_DIR = ./bin/Release
endif


BINARIES = \
	$(THELASTRIPPER)  
PROGRAMFILES = \
	$(THELASTRIPPER_PNG) \
	$(LIBLASTRIP_DLL) \
	$(TAGLIB_SHARP_DLL)  
LINUX_DESKTOPAPPLICATIONS = \
	$(THELASTRIPPER_DESKTOP)  

	
all: $(ASSEMBLY) $(BINARIES) $(PROGRAMFILES) $(LINUX_DESKTOPAPPLICATIONS) 

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
	gtk-gui/MonoClient.Preferences.cs 

DATA_FILES = \
	app.desktop \
	TheLastRipper.png 

RESOURCES = \
	gtk-gui/gui.stetic \
	../AboutBox.svg \
	logo.png 

EXTRAS = \
	./thelastripper.desktop.in \
	./thelastripper.in 

REFERENCES =  \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	$(GLADE_SHARP_20_LIBS) \
	System \
	Mono.Posix \
	$(RSVG_SHARP_20_LIBS)

DLL_REFERENCES = 

THELASTRIPPER_DESKTOP = $(BUILD_DIR)/thelastripper.desktop

THELASTRIPPER_PNG = $(BUILD_DIR)/TheLastRipper.png

LIBLASTRIP_DLL = $(BUILD_DIR)/libLastRip.dll

TAGLIB_SHARP_DLL = $(BUILD_DIR)/taglib-sharp.dll

THELASTRIPPER = $(BUILD_DIR)/thelastripper


$(THELASTRIPPER_DESKTOP): thelastripper.desktop
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(THELASTRIPPER_PNG): ./TheLastRipper.png
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(LIBLASTRIP_DLL): ../libLastRip/bin/Release/libLastRip.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(TAGLIB_SHARP_DLL): ../taglib-sharp/taglib-sharp.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(THELASTRIPPER): thelastripper
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include