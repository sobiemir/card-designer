@echo off

:: check if resgen and csc commands exist in system
where resgen
if %ERRORLEVEL% neq 0 (
	echo ### Command resgen not exist
	exit /B 1
)
where csc
if %ERRORLEVEL% neq 0 (
	echo ### Command csc not exist
	exit /B 1
)

:: create obj directory for resources
if not exist obj (
	echo ### Creating "obj" directory...
	mkdir obj
)

:: generate resources using resgen
echo ### Generating resources...
@echo on
resgen /useSourcePath ^
	/compile ^
		resx\Main.resx,obj\CDesigner.Main.resources ^
		resx\NewPattern.resx,obj\CDesigner.NewPattern.resources ^
        resx\DataReader.resx,obj\CDesigner.DataReader.resources ^
        resx\Info.resx,obj\CDesigner.Info.resources ^
		properties\Resources.resx,obj\CDesigner.Properties.Resources.resources
@echo off

:: create dll and build directory
if not exist dll (
	echo ### Creating "dll" directory...
	mkdir dll
)
if not exist build (
	echo ### Creating "build" directory...
	mkdir build
)

:: check if wget command is available
echo ### Checking wget command...
where wget

:: wget not exist
if %ERRORLEVEL% neq 0 (
	echo ### Command wget not found
	
	:: check if dll files exist, if not, display error
	if not exist dll/PdfSharp.dll (
		echo ### File "dll/PdfSharp.dll" not exist
		exit /B 1
	)
) else (
	echo ### Command wget found
	
	:: check if dll files exist, if not, download them
	if not exist dll/PdfSharp.dll (
		echo ### Downloading "PDFSharp.dll" from archive...
		wget http://archive.aculo.pl/card-designer/lib/PdfSharp-v1.32.2608.dll ^
			-O dll/PdfSharp.dll
	)
)

:: compile application using csc
echo ### Compiling application...
@echo on

csc /reference:dll\PdfSharp.dll ^
	/out:build\CDesigner.exe ^
	/resource:obj\CDesigner.Main.resources ^
	/resource:obj\CDesigner.NewPattern.resources ^
    /resource:obj\CDesigner.DataReader.resources ^
    /resource:obj\CDesigner.Info.resources ^
	/resource:obj\CDesigner.Properties.Resources.resources ^
	/win32manifest:properties\app.manifest ^
	/win32icon:resources\cdesigner.ico ^
	/target:winexe ^
	/utf8output ^
		src\PageField.cs ^
		src\Main.cs ^
		designer\Main.Designer.cs ^
		src\Info.cs ^
		designer\Info.Designer.cs ^
		src\NewPattern.cs ^
		designer\NewPattern.Designer.cs ^
        src\PatternEditor.cs ^
		src\Program.cs ^
		src\Structs.cs ^
        src\DataReader.cs ^
        designer\DataReader.Designer.cs ^
		properties\AssemblyInfo.cs ^
		properties\Resources.Designer.cs

@echo off

:: create icons folder
if not exist build\icons (
	echo ### Creating "build\icons" directory...
	mkdir build\icons
)

echo ### Copy images and dlls

:: copying image and dll files
copy /Y resources\noimage.png build
copy /Y dll\PdfSharp.dll build
copy /Y resources\icons\image-field.png build\icons
copy /Y resources\icons\text-field.png build\icons
copy /Y resources\icons\exit-application.png build\icons
copy /Y resources\transparent.png build
copy /Y resources\cdrestore.ico build
copy /Y resources\icons\cdrestore-512.png build\icons
copy /Y resources\icons\cdrestore-256.png build\icons
copy /Y resources\icons\cdrestore-128.png build\icons
copy /Y resources\icons\cdrestore-96.png build\icons
copy /Y resources\icons\cdrestore-64.png build\icons
copy /Y resources\icons\cdrestore-48.png build\icons
copy /Y resources\icons\cdrestore-32.png build\icons
copy /Y resources\icons\cdrestore-16.png build\icons
copy /Y resources\cdesigner.ico build
copy /Y resources\icons\cdesigner-512.png build\icons
copy /Y resources\icons\cdesigner-256.png build\icons
copy /Y resources\icons\cdesigner-128.png build\icons
copy /Y resources\icons\cdesigner-96.png build\icons
copy /Y resources\icons\cdesigner-64.png build\icons
copy /Y resources\icons\cdesigner-48.png build\icons
copy /Y resources\icons\cdesigner-32.png build\icons
copy /Y resources\icons\cdesigner-16.png build\icons
copy /Y resources\icons\add-pattern.png build\icons

echo ### Finished
exit /B 0
