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
		resx/Main.resx,obj/CDesigner.Main.resources ^
		resx/NewPattern.resx,obj/CDesigner.NewPattern.resources ^
		properties/Resources.resx,obj/CDesigner.Properties.Resources.resources
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

csc /reference:dll/PdfSharp.dll ^
	/out:build/CDesigner.exe ^
	/resource:obj/CDesigner.Main.resources ^
	/resource:obj/CDesigner.NewPattern.resources ^
	/resource:obj/CDesigner.Properties.Resources.resources ^
	/target:winexe ^
	/utf8output ^
		src/CustomLabel.cs ^
		src/Main.cs ^
		designer/Main.Designer.cs ^
		src/NewPattern.cs ^
		designer/NewPattern.Designer.cs ^
		src/Program.cs ^
		properties/AssemblyInfo.cs ^
		properties/Resources.Designer.cs ^
		properties/Settings.Designer.cs

@echo off
echo ### Copy images and dlls

:: copying image and dll files
copy /Y resources\noimage.png build
copy /Y dll\PdfSharp.dll build

echo ### Finished
exit /B 0
