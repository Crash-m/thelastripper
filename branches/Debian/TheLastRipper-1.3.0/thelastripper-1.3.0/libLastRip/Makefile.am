
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

TAGLIB_SHARP_DLL_SOURCE=../taglib-sharp/taglib-sharp.dll

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/libLastRip.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

TAGLIB_SHARP_DLL_SOURCE=../taglib-sharp/taglib-sharp.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=.resources.dll

PROGRAMFILES = \
	$(TAGLIB_SHARP_DLL)  


	
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
	ScanningEventArgs.cs 

DATA_FILES = \
	../taglib-sharp/taglib-sharp.dll 

RESOURCES = 

EXTRAS = 

REFERENCES =  \
	System \
	System.Xml

DLL_REFERENCES =  \
	../taglib-sharp/taglib-sharp.dll

CLEANFILES = $(PROGRAMFILES) 

include $(top_srcdir)/Makefile.include

TAGLIB_SHARP_DLL = $(BUILD_DIR)/taglib-sharp.dll

$(eval $(call emit-deploy-target,TAGLIB_SHARP_DLL))


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
