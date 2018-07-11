#!/bin/bash

# check if resgen command exist
if ! [ -x "$(command -v resgen)" ]; then
	echo "### Command resgen not exist"
	exit 1
fi

# check if csc compiler exist
csc='csc'
if ! [ -x "$(command -v csc)" ]; then
	echo "### Command csc not exist, checking mono-csc..."

	# csc can be also mono-csc
	if ! [ -x "$(command -v mono-csc)" ]; then
		echo "### Command mono-csc not exist"
		exit 1
	else
		echo "### Command mono-csc found, switching..."
		csc='mono-csc'
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
        resx/SetColumns.resx,obj/CDesigner.SetColumns.resources \
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
	if ! [ -f "dll/Bytescout.PDFRenderer.dll" ]; then
		echo "### File \"dll/Bytescout.PDFRenderer.dll\" not exist"
		exit 1
	fi

	if ! [ -f "dll/PdfSharp.dll" ]; then
		echo "### File \"dll/PdfSharp.dll\" not exist"
		exit 1
	fi
else
	echo "### Command wget found"

	# check if dll files exist, if not, download them
	if ! [ -f "dll/Bytescout.PDFRenderer.dll" ]; then
		echo "### Downloading \"Bytescout.PDFRenderer.dll\" from archive..."
		wget http://archive.aculo.pl/card-designer/lib/Bytescout.PDFRenderer-v5.20.1870.dll \
			-O dll/Bytescout.PDFRenderer.dll
	fi
	if ! [ -f "dll/PdfSharp.dll" ]; then
		echo "### Downloading \"PDFSharp.dll\" from archive..."
		wget http://archive.aculo.pl/card-designer/lib/PdfSharp-v1.32.2608.dll \
			-O dll/PdfSharp.dll
	fi
fi

# compile application using csc or mono-csc
echo "### Compiling application"
$csc /reference:dll/PdfSharp.dll \
	/reference:System.Windows.Forms \
	/reference:System.Drawing \
	/reference:System.Data \
	/out:build/CDesigner.exe \
	/resource:obj/CDesigner.Main.resources \
	/resource:obj/CDesigner.NewPattern.resources \
    /resource:obj/CDesigner.SetColumns.resources \
	/resource:obj/CDesigner.Properties.Resources.resources \
	/target:winexe \
	/utf8output \
		src/CustomLabel.cs \
        src/ExtendLabel.cs \
		src/Main.cs \
		designer/Main.Designer.cs \
		src/NewPattern.cs \
		designer/NewPattern.Designer.cs \
        src/PatternEditor.cs \
		src/Program.cs \
        src/SetColumns.cs \
        designer/SetColumns.Designer.cs \
		properties/AssemblyInfo.cs \
		properties/Resources.Designer.cs \

echo "### Copying images and dlls"

# copy image and dll files
cp -f resources/noimage.png build/noimage.png
cp -f dll/Bytescout.PDFRenderer.dll build/Bytescout.PDFRenderer.dll
cp -f dll/PdfSharp.dll build/PdfSharp.dll

echo "### Finished"
exit 0
>>>>>>> c2deddd... Update compiling script for Linux
