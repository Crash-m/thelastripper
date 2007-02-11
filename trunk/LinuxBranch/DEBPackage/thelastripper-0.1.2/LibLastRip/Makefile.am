
# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/LibLastRip.dll
COMPILE_TARGET = library
PC_FILES_IN = TheLastRipper.LibLastRip.pc.in
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Debug
endif
if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/LibLastRip.dll
COMPILE_TARGET = library
PC_FILES_IN = TheLastRipper.LibLastRip.pc.in
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Release
endif

	
all: $(ASSEMBLY)

FILES = \
	./AssemblyInfo.cs \
	./MetaInfo.cs \
	./LastManager/LastManager.Commands.cs \
	./LastManager/LastManager.cs \
	./LastManager/LastManager.MetaInfo.cs \
	./LastManager/LastManager.Stream.cs \
	./PlayList/MetaMusic.cs \
	./PlayList/PlayList.cs \
	./PlayList/IMetaMusic.cs \
	./PlatformSettings.cs \
	./PlayList/PlayListGenerator.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = 

REFERENCES =  \
	-r:System \
	-r:System.Xml

DLL_REFERENCES = 

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) 
	mkdir -p $(dir $(ASSEMBLY))		

	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include
