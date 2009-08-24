
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG
ASSEMBLY = bin/Debug/libLastRip.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

LIBLASTRIP_DLL_MDB_SOURCE=bin/Debug/libLastRip.dll.mdb
LIBLASTRIP_DLL_MDB=$(BUILD_DIR)/libLastRip.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/libLastRip.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

LIBLASTRIP_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(LIBLASTRIP_DLL_MDB)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) 

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
	LastManager/ErrorEventArgs.cs \
	XSPF.cs \
	ScanningEventArgs.cs \
	SongCompletedEventArgs.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = 

REFERENCES =  \
	System \
	System.Xml \
	$(TAGLIB_SHARP_LIBS)

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) 

include $(top_srcdir)/Makefile.include




$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
