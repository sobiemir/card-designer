#!/bin/bash

# Kamil Biały <sobiemir>
# ======================================================================================================================
# CDesigner
# CDRestore
# ======================================================================================================================
# To compile, just use build.bat in console.
# You need to have installed CSC compiler.
# Additionally, you can download archives for pdfsharp from http://archive.aculo.pl/card-designer/lib
# and place it to dll folder, or download wget for windows, so, this script download this dll automatically for you.
# You can compile application, by using "build", "build d:console", "build d:trace", "build d:full", where d:console
# builds app with DEBUG flag and console view, d:trace save all logs to file, and d:full have it all.
# ======================================================================================================================

# check if csc compiler exist
csc="csc"

if ! [ -x "$(command -v csc)" ]; then
	echo "### Command csc not exist, checking mono-csc..."

	# csc can be also mono-csc
	if ! [ -x "$(command -v mono-csc)" ]; then
		echo "### Command mono-csc not exist"
		exit 1
	else
		echo "### Command mono-csc found, switching..."
		csc="mono-csc"

		# mono-csc needs to add some references (at least at debian)
		references="/reference:System.Windows.Forms
			/reference:System.Drawing
			/reference:System.Data"
	fi
fi

# create dll and build directory
if ! [ -d "dll" ]; then
	echo "### Creating \"dll\" directory..."
	mkdir dll
fi
if ! [ -d "build" ]; then
	echo "### Creating \"build\" directory..."
	mkdir build
fi


# check if wget command is available
echo "### Checking wget command..."

# wget not exist
if ! [ -x "$(command -v wget)" ]; then
	echo "### Command wget not found"

	# check if dll files exist, if not, display error
	if ! [ -f "dll/PdfSharp.dll" ]; then
		echo "### File \"dll/PdfSharp.dll\" not exist"
		exit 1
	fi
else
	echo "### Command wget found"

	# check if dll files exist, if not, download them
	if ! [ -f "dll/PdfSharp.dll" ]; then
		echo "### Downloading \"PDFSharp.dll\" from archive..."
		wget http://archive.aculo.pl/card-designer/lib/PdfSharp-v1.32.2608.dll \
			-O dll/PdfSharp.dll
	fi
fi

# change target and definitions for debug
target="/target:winexe"
define=""

if [ "$1" = "d:console" ]; then
	target="/target:exe"
	define="/define:DEBUG"
elif [ "$1" = "d:trace" ]; then
	define="/define:TRACE"
elif [ "$1" = "d:full" ]; then
	target="/target:exe"
	define="/define:DEBUG;TRACE"
fi

# compile CDesigner application using csc
echo "### Compiling CDesigner application..."

$csc /reference:dll/PdfSharp.dll \
	$references \
	/out:build/cdesigner.exe \
	/appconfig:properties/app.config \
	/win32manifest:properties/app.manifest \
	/win32icon:resources/cdesigner.ico \
	$target \
	$define \
	/utf8output \
		src/utils/AssemblyLoader.cs \
		src/utils/DataBackup.cs \
		src/utils/DataFilter.cs \
		src/utils/DataStorage.cs \
		src/utils/IOFileData.cs \
		src/utils/Language.cs \
		src/utils/PatternEditor.cs \
		src/utils/Program.cs \
		src/utils/ProgressStream.cs \
		src/utils/Settings.cs \
		src/utils/Structures.cs \
		src/utils/UpdateApp.cs \
		src/controls/AlignedPage.cs \
		src/controls/AlignedPictureBox.cs \
		src/controls/PageField.cs \
		src/forms/DatafileSettingsForm.cs \
		src/forms/DataReaderForm.cs \
		src/forms/EditColumnsForm.cs \
		src/forms/EditRowsForm.cs \
		src/forms/InfoForm.cs \
		src/forms/MainForm.cs \
		src/forms/NewPatternForm.cs \
		src/forms/UpdateForm.cs \
		designer/DatafileSettingsForm.Designer.cs \
		designer/DataReaderForm.Designer.cs \
		designer/EditColumnsForm.Designer.cs \
		designer/EditRowsForm.Designer.cs \
		designer/InfoForm.Designer.cs \
		designer/MainForm.Designer.cs \
		designer/NewPatternForm.Designer.cs \
		designer/UpdateForm.Designer.cs \
		properties/AssemblyInfo.cs

# compile CDRestore application using csc
echo "### Compiling CDRestore application..."

$csc /out:build/cdrestore.exe \
	$references \
	/win32manifest:properties/cdrestore/app.manifest \
	/win32icon:resources/cdrestore.ico \
	$target \
	$define \
	/utf8output \
		src/cdrestore/CBackupData.cs \
		src/cdrestore/MainForm.cs \
		src/cdrestore/Program.cs \
		src/cdrestore/ProgressStream.cs \
		designer/cdrestore/MainForm.Designer.cs \
		properties/cdrestore/AssemblyInfo.cs

# create icons folder
if ! [ -d "build/icons" ]; then
	echo "### Creating \"build/icons\" directory..."
	mkdir build/icons
fi

# create update folder
if ! [ -d "build/update" ]; then
	echo "### Creating \"build/update\" directory..."
	mkdir build/update
fi

# create libraries folder
if ! [ -d "build/libraries" ]; then
	echo "### Creating \"build/libraries\" directory..."
	mkdir build/libraries
fi

# create images folder
if ! [ -d "build/images" ]; then
	echo "### Creating \"build/images\" directory..."
	mkdir build/images
fi

# create patterns folder
if ! [ -d "build/patterns" ]; then
	echo "### Creating \"build/patterns\" directory..."
	mkdir build/patterns
fi

# create backup folder
if ! [ -d "build/backup" ]; then
	echo "### Creating \"build/backup\" directory..."
	mkdir build/backup
fi

# create languages folder
if ! [ -d "build/languages" ]; then
	echo "### Creating \"build/languages\" directory..."
	mkdir build/languages
fi

echo "### Copying images and dlls"

# copy image and dll files
cp -f dll/PdfSharp.dll build/libraries/PdfSharp.dll
cp -f resources/noimage.png build/images/noimage.png
cp -f resources/transparent.png build/images/transparent.png
cp -f resources/cdrestore.ico build/icons/cdrestore.ico
cp -f resources/cdesigner.ico build/icons/cdesigner.ico
cp -f properties/cdrestore/update.lst build/update.lst
cp -f resources/information.jpg build/images/information.jpg
cp -f resources/icons/image-field.png build/icons/image-field.png
cp -f resources/icons/text-field.png build/icons/text-field.png
cp -f resources/icons/exit-application.png build/icons/exit-application.png
cp -f resources/logo/cdrestore-512.png build/images/cdrestore-512.png
cp -f resources/logo/cdrestore-256.png build/images/cdrestore-256.png
cp -f resources/logo/cdrestore-128.png build/images/cdrestore-128.png
cp -f resources/logo/cdrestore-96.png build/images/cdrestore-96.png
cp -f resources/logo/cdrestore-64.png build/images/cdrestore-64.png
cp -f resources/logo/cdrestore-48.png build/images/cdrestore-48.png
cp -f resources/logo/cdrestore-32.png build/images/cdrestore-32.png
cp -f resources/logo/cdrestore-16.png build/images/cdrestore-16.png
cp -f resources/logo/cdesigner-512.png build/images/cdesigner-512.png
cp -f resources/logo/cdesigner-256.png build/images/cdesigner-256.png
cp -f resources/logo/cdesigner-128.png build/images/cdesigner-128.png
cp -f resources/logo/cdesigner-96.png build/images/cdesigner-96.png
cp -f resources/logo/cdesigner-64.png build/images/cdesigner-64.png
cp -f resources/logo/cdesigner-48.png build/images/cdesigner-48.png
cp -f resources/logo/cdesigner-32.png build/images/cdesigner-32.png
cp -f resources/logo/cdesigner-16.png build/images/cdesigner-16.png
cp -f resources/icons/add-pattern.png build/icons/add-pattern.png
cp -f resources/icons/first-page.png build/icons/first-page.png
cp -f resources/icons/item-add.png build/icons/item-add.png
cp -f resources/icons/item-delete.png build/icons/item-delete.png
cp -f resources/icons/last-page.png build/icons/last-page.png
cp -f resources/icons/next-page.png build/icons/next-page.png
cp -f resources/icons/prev-page.png build/icons/prev-page.png
cp -f resources/icons/refresh.png build/icons/refresh.png
cp -f resources/icons/split-rows.png build/icons/split-rows.png
cp -f languages/eng.lex build/languages/eng.lex
cp -f languages/pol.lex build/languages/pol.lex
cp -f languages/qps.lex build/languages/qps.lex

echo "### Finished"
exit 0
