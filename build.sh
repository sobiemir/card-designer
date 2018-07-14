#!/bin/bash

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

echo "### Generating resources..."
resgen /useSourcePath \
	/compile \
		resx/Main.resx,obj/CDesigner.Main.resources \
		resx/NewPattern.resx,obj/CDesigner.NewPattern.resources \
		resx/DataReader.resx,obj/CDesigner.DataReader.resources \
		resx/Info.resx,obj/CDesigner.Info.resources \
		properties/Resources.resx,obj/CDesigner.Properties.Resources.resources

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

# compile application using csc or mono-csc
echo "### Compiling application"
$csc /reference:dll/PdfSharp.dll \
	$references \
	/out:build/CDesigner.exe \
	/resource:obj/CDesigner.Main.resources \
	/resource:obj/CDesigner.NewPattern.resources \
	/resource:obj/CDesigner.DataReader.resources \
	/resource:obj/CDesigner.Info.resources \
	/resource:obj/CDesigner.Properties.Resources.resources \
	/win32manifest:properties/app.manifest \
	/win32icon:resources/cdesigner.ico \
	/target:winexe \
	/utf8output \
		src/PageField.cs \
		src/Main.cs \
		designer/Main.Designer.cs \
		src/Info.cs \
		designer/Info.Designer.cs \
		src/NewPattern.cs \
		designer/NewPattern.Designer.cs \
		src/PatternEditor.cs \
		src/Program.cs \
		src/Structs.cs \
		src/DataReader.cs \
		designer/DataReader.Designer.cs \
		properties/AssemblyInfo.cs \
		properties/Resources.Designer.cs

# create icons folder
if ! [ -d "build/icons" ]; then
	echo "### Creating \"build/icons\" directory..."
	mkdir build/icons
fi

echo "### Copying images and dlls"

# copy image and dll files
cp -f resources/noimage.png build/noimage.png
cp -f dll/PdfSharp.dll build/PdfSharp.dll
cp -f resources/icons/image-field.png build/icons/image-field.png
cp -f resources/icons/text-field.png build/icons/text-field.png
cp -f resources/icons/exit-application.png build/icons/exit-application.png
cp -f resources/transparent.png build/transparent.png
cp -f resources/cdrestore.ico build/cdrestore.ico
cp -f resources/icons/cdrestore-512.png build/icons/cdrestore-512.png
cp -f resources/icons/cdrestore-256.png build/icons/cdrestore-256.png
cp -f resources/icons/cdrestore-128.png build/icons/cdrestore-128.png
cp -f resources/icons/cdrestore-96.png build/icons/cdrestore-96.png
cp -f resources/icons/cdrestore-64.png build/icons/cdrestore-64.png
cp -f resources/icons/cdrestore-48.png build/icons/cdrestore-48.png
cp -f resources/icons/cdrestore-32.png build/icons/cdrestore-32.png
cp -f resources/icons/cdrestore-16.png build/icons/cdrestore-16.png
cp -f resources/cdesigner.ico build/cdesigner.ico
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
