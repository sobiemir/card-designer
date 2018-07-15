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

# check if resgen command exist
if ! [ -x "$(command -v resgen)" ]; then
	echo "### Command resgen not exist"
	exit 1
fi

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

# create obj directory for resources
if ! [ -d "obj" ]; then
	echo "### Creating \"obj\" directory"
	mkdir obj
fi

echo "### Generating resources for CDesigner..."
resgen /useSourcePath \
	/compile \
        resx/DatabaseSettingsForm.resx,obj/CDesigner.DatabaseSettingsForm.resources \
        resx/DataFilterForm.resx,obj/CDesigner.DataFilterForm.resources \
        resx/DataReader.resx,obj/CDesigner.DataReader.resources \
        resx/DBConnection.resx,obj/CDesigner.DBConnection.resources \
        resx/EditColumnsForm.resx,obj/CDesigner.EditColumnsForm.resources \
        resx/InfoForm.resx,obj/CDesigner.InfoForm.resources \
		resx/MainForm.resx,obj/CDesigner.MainForm.resources \
		resx/NewPattern.resx,obj/CDesigner.NewPattern.resources \
		resx/Settings.resx,obj/CDesigner.Settings.resources \
		resx/UpdateForm.resx,obj/CDesigner.UpdateForm.resources \
		properties/Resources.resx,obj/CDesigner.Properties.Resources.resources

echo "### Generating resources for CDRestore..."

resgen /useSourcePath \
	/compile \
        resx/cdrestore/MainForm.resx,obj/CDRestore.MainForm.resources \
		properties/cdrestore/Resources.resx,obj/CDRestore.Properties.Resources.resources

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
	/resource:obj/CDesigner.DatabaseSettingsForm.resources \
	/resource:obj/CDesigner.DataFilterForm.resources \
	/resource:obj/CDesigner.DataReader.resources \
	/resource:obj/CDesigner.DBConnection.resources \
	/resource:obj/CDesigner.EditColumnsForm.resources \
    /resource:obj/CDesigner.InfoForm.resources \
    /resource:obj/CDesigner.MainForm.resources \
	/resource:obj/CDesigner.NewPattern.resources \
	/resource:obj/CDesigner.Settings.resources \
	/resource:obj/CDesigner.UpdateForm.resources \
	/resource:obj/CDesigner.Properties.Resources.resources \
	/appconfig:properties/app.config \
	/win32manifest:properties/app.manifest \
	/win32icon:resources/cdesigner.ico \
	$target \
	$define \
	/utf8output \
		src/class/AssemblyLoader.cs \
		src/class/CBackupData.cs \
		src/class/DatabaseReader.cs \
		src/class/DataFilter.cs \
		src/class/PatternEditor.cs \
		src/class/Program.cs \
		src/class/ProgressStream.cs \
		src/class/Structures.cs \
		src/controls/AlignedPage.cs \
		src/controls/AlignedPictureBox.cs \
		src/controls/GroupComboBox.cs \
		src/controls/PageField.cs \
		src/forms/DatabaseSettingsForm.cs \
		src/forms/DataFilterForm.cs \
		src/forms/DataReader.cs \
		src/forms/DBConnection.cs \
		src/forms/EditColumnsForm.cs \
		src/forms/InfoForm.cs \
		src/forms/MainForm.cs \
		src/forms/NewPattern.cs \
		src/forms/Settings.cs \
		src/forms/UpdateForm.cs \
		designer/DatabaseSettingsForm.Designer.cs \
		designer/DataFilterForm.Designer.cs \
		designer/DataReader.Designer.cs \
		designer/DBConnection.Designer.cs \
		designer/EditColumnsForm.Designer.cs \
		designer/InfoForm.Designer.cs \
		designer/MainForm.Designer.cs \
		designer/NewPattern.Designer.cs \
		designer/Settings.Designer.cs \
		designer/UpdateForm.Designer.cs \
		properties/AssemblyInfo.cs \
		properties/Resources.Designer.cs

# compile CDRestore application using csc
echo "### Compiling CDRestore application..."

$csc /out:build/cdrestore.exe \
	$references \
    /resource:obj/CDRestore.MainForm.resources \
	/resource:obj/CDRestore.Properties.Resources.resources \
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
		properties/cdrestore/AssemblyInfo.cs \
		properties/cdrestore/Resources.Designer.cs

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

echo "### Copying images and dlls"

# copy image and dll files
cp -f dll/PdfSharp.dll build/libraries/PdfSharp.dll
cp -f resources/noimage.png build/images/noimage.png
cp -f resources/transparent.png build/images/transparent.png
cp -f resources/cdrestore.ico build/icons/cdrestore.ico
cp -f resources/cdesigner.ico build/icons/cdesigner.ico
cp -f properties/cdrestore/update.lst build/update.lst
cp -f resources/icons/image-field.png build/icons/image-field.png
cp -f resources/icons/text-field.png build/icons/text-field.png
cp -f resources/icons/exit-application.png build/icons/exit-application.png
cp -f resources/icons/cdrestore-512.png build/icons/cdrestore-512.png
cp -f resources/icons/cdrestore-256.png build/icons/cdrestore-256.png
cp -f resources/icons/cdrestore-128.png build/icons/cdrestore-128.png
cp -f resources/icons/cdrestore-96.png build/icons/cdrestore-96.png
cp -f resources/icons/cdrestore-64.png build/icons/cdrestore-64.png
cp -f resources/icons/cdrestore-48.png build/icons/cdrestore-48.png
cp -f resources/icons/cdrestore-32.png build/icons/cdrestore-32.png
cp -f resources/icons/cdrestore-16.png build/icons/cdrestore-16.png
cp -f resources/icons/cdesigner-512.png build/icons/cdesigner-512.png
cp -f resources/icons/cdesigner-256.png build/icons/cdesigner-256.png
cp -f resources/icons/cdesigner-128.png build/icons/cdesigner-128.png
cp -f resources/icons/cdesigner-96.png build/icons/cdesigner-96.png
cp -f resources/icons/cdesigner-64.png build/icons/cdesigner-64.png
cp -f resources/icons/cdesigner-48.png build/icons/cdesigner-48.png
cp -f resources/icons/cdesigner-32.png build/icons/cdesigner-32.png
cp -f resources/icons/cdesigner-16.png build/icons/cdesigner-16.png
cp -f resources/icons/add-pattern.png build/icons/add-pattern.png

echo "### Finished"
exit 0
