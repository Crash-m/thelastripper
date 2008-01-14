
EXTRA_DIST = 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/libLastRip.dll
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Debug
endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/libLastRip.dll
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Release
endif


PROGRAMFILES = \
	$(TAGLIB_SHARP_DLL)  
LINUX_PKGCONFIG = \
	$(LIBLASTRIP_PC)  

	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	AssemblyInfo.cs \
	MetaInfo.cs \
	LastManager/LastManager.Commands.cs \
	LastManager/LastManager.cs \
	LastManager/LastManager.MetaInfo.cs \
	LastManager/LastManager.Stream.cs \
	PlayList/IMetaMusic.cs \
	PlayList/MetaMusic.cs \
	PlayList/PlayList.cs \
	PlayList/PlayListGenerator.cs \
	StreamAbstraction.cs \
	ProgressEventArgs.cs \
	LastManager/ErrorEventArgs.cs 

DATA_FILES = \
	../taglib-sharp/taglib-sharp.dll 

RESOURCES = 

EXTRAS = \
	./liblastrip.pc.in 

REFERENCES =  \
	System \
	System.Xml

DLL_REFERENCES =  \
	../taglib-sharp/taglib-sharp.dll

TAGLIB_SHARP_DLL = $(BUILD_DIR)/taglib-sharp.dll

LIBLASTRIP_PC = $(BUILD_DIR)/liblastrip.pc


$(TAGLIB_SHARP_DLL): ../taglib-sharp/taglib-sharp.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(LIBLASTRIP_PC): liblastrip.pc
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include