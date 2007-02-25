
# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/TheLastRipper.exe
COMPILE_TARGET = exe
ASSEMBLY_WRAPPER = thelastripper
ASSEMBLY_WRAPPER_IN = thelastripper.in
PROJECT_REFERENCES =  \
	../LibLastRip/bin/Debug/LibLastRip.dll
BUILD_DIR = ./bin/Debug
endif
if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/TheLastRipper.exe
COMPILE_TARGET = exe
ASSEMBLY_WRAPPER = thelastripper
ASSEMBLY_WRAPPER_IN = thelastripper.in
PROJECT_REFERENCES =  \
	../LibLastRip/bin/Release/LibLastRip.dll
BUILD_DIR = ./bin/Release
endif

	
all: $(ASSEMBLY)

FILES = \
	./gtk-gui/generated.cs \
	./MainWindow.cs \
	./Main.cs \
	./AssemblyInfo.cs \
	./About.cs \
	./Preferences.cs \
	./Settings.cs 

DATA_FILES = 

RESOURCES = \
	../logo.svg \
	../aboutlogo.svg \
	./AboutBoxLinux.png 

EXTRAS = 

REFERENCES =  \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	$(GLADE_SHARP_20_LIBS) \
	$(RSVG_SHARP_20_LIBS) \
	-r:System

DLL_REFERENCES = 

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) 
	mkdir -p $(dir $(ASSEMBLY))		

	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include
