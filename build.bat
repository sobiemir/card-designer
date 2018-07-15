:: Kamil Biały <sobiemir>
:: =====================================================================================================================
:: CDesigner
:: CDRestore
:: =====================================================================================================================
:: To compile, just use build.bat in console.
:: You need to have installed CSC compiler.
:: Additionally, you can download archives for pdfsharp from http://archive.aculo.pl/card-designer/lib
:: and place it to dll folder, or download wget for windows, so, this script download this dll automatically for you.
:: You can compile application, by using "build", "build d:console", "build d:trace", "build d:full", where d:console
:: builds app with DEBUG flag and console view, d:trace save all logs to file, and d:full have it all.
:: =====================================================================================================================

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

:: create obj directory for CDesigner resources
if not exist obj (
	echo ### Creating "obj" directory...
	mkdir obj
)

:: generate resources using resgen
echo ### Generating resources for CDesigner...

resgen /useSourcePath ^
	/compile ^
        resx\DatabaseSettingsForm.resx,obj\CDesigner.DatabaseSettingsForm.resources ^
        resx\DataFilterForm.resx,obj\CDesigner.DataFilterForm.resources ^
        resx\DataReader.resx,obj\CDesigner.DataReader.resources ^
        resx\DBConnection.resx,obj\CDesigner.DBConnection.resources ^
        resx\EditColumnsForm.resx,obj\CDesigner.EditColumnsForm.resources ^
        resx\InfoForm.resx,obj\CDesigner.InfoForm.resources ^
		resx\MainForm.resx,obj\CDesigner.MainForm.resources ^
		resx\NewPattern.resx,obj\CDesigner.NewPattern.resources ^
		resx\Settings.resx,obj\CDesigner.Settings.resources ^
		resx\UpdateForm.resx,obj\CDesigner.UpdateForm.resources ^
		properties\Resources.resx,obj\CDesigner.Properties.Resources.resources


:: generate resources using resgen
echo ### Generating resources for CDRestore...

resgen /useSourcePath ^
	/compile ^
        resx\cdrestore\MainForm.resx,obj\CDRestore.MainForm.resources ^
		properties\cdrestore\Resources.resx,obj\CDRestore.Properties.Resources.resources

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

:: change target and definitions for debug
set target=/target:winexe
set define=

if "%1" == "d:console" (
	set target=/target:exe
	set define=/define:DEBUG
) else if "%1" == "d:trace" (
	set define=/define:TRACE
) else if "%1" == "d:full" (
	set target=/target:exe
	set define=/define:DEBUG;TRACE
)

:: compile CDesigner application using csc
echo ### Compiling CDesigner application...

csc /reference:dll\PdfSharp.dll ^
	/out:build\cdesigner.exe ^
	/resource:obj\CDesigner.DatabaseSettingsForm.resources ^
	/resource:obj\CDesigner.DataFilterForm.resources ^
	/resource:obj\CDesigner.DataReader.resources ^
	/resource:obj\CDesigner.DBConnection.resources ^
	/resource:obj\CDesigner.EditColumnsForm.resources ^
    /resource:obj\CDesigner.InfoForm.resources ^
    /resource:obj\CDesigner.MainForm.resources ^
	/resource:obj\CDesigner.NewPattern.resources ^
	/resource:obj\CDesigner.Settings.resources ^
	/resource:obj\CDesigner.UpdateForm.resources ^
	/resource:obj\CDesigner.Properties.Resources.resources ^
	/appconfig:properties\app.config ^
	/win32manifest:properties\app.manifest ^
	/win32icon:resources\cdesigner.ico ^
	%target% ^
	%define% ^
	/utf8output ^
		src\class\AssemblyLoader.cs ^
		src\class\CBackupData.cs ^
		src\class\DatabaseReader.cs ^
		src\class\PatternEditor.cs ^
		src\class\Program.cs ^
		src\class\ProgressStream.cs ^
		src\class\Structs.cs ^
		src\controls\AlignedPage.cs ^
		src\controls\AlignedPictureBox.cs ^
		src\controls\CustomContextMenuStrip.cs ^
		src\controls\PageField.cs ^
		src\controls\TreeComboBox.cs ^
		src\forms\DatabaseSettingsForm.cs ^
		src\forms\DataFilterForm.cs ^
		src\forms\DataReader.cs ^
		src\forms\DBConnection.cs ^
		src\forms\EditColumnsForm.cs ^
		src\forms\InfoForm.cs ^
		src\forms\MainForm.cs ^
		src\forms\NewPattern.cs ^
		src\forms\Settings.cs ^
		src\forms\UpdateForm.cs ^
		designer\DatabaseSettingsForm.Designer.cs ^
		designer\DataFilterForm.Designer.cs ^
		designer\DataReader.Designer.cs ^
		designer\DBConnection.Designer.cs ^
		designer\EditColumnsForm.Designer.cs ^
		designer\InfoForm.Designer.cs ^
		designer\MainForm.Designer.cs ^
		designer\NewPattern.Designer.cs ^
		designer\Settings.Designer.cs ^
		designer\UpdateForm.Designer.cs ^
		properties\AssemblyInfo.cs ^
		properties\Resources.Designer.cs

:: compile CDRestore application using csc
echo ### Compiling CDRestore application...

csc /out:build\cdrestore.exe ^
    /resource:obj\CDRestore.MainForm.resources ^
	/resource:obj\CDRestore.Properties.Resources.resources ^
	/win32manifest:properties\cdrestore\app.manifest ^
	/win32icon:resources\cdrestore.ico ^
	%target% ^
	%define% ^
	/utf8output ^
		src\cdrestore\CBackupData.cs ^
		src\cdrestore\MainForm.cs ^
		src\cdrestore\Program.cs ^
		src\cdrestore\ProgressStream.cs ^
		designer\cdrestore\MainForm.Designer.cs ^
		properties\cdrestore\AssemblyInfo.cs ^
		properties\cdrestore\Resources.Designer.cs

:: create icons folder
if not exist build\icons (
	echo ### Creating "build\icons" directory...
	mkdir build\icons
)

:: create update folder
if not exist build\update (
	echo ### Creating "build\update" directory...
	mkdir build\update
)

:: create libraries folder
if not exist build\libraries (
	echo ### Creating "build\libraries" directory...
	mkdir build\libraries
)

:: create images folder
if not exist build\images (
	echo ### Creating "build\images" directory...
	mkdir build\images
)

:: create patterns folder
if not exist build\patterns (
	echo ### Creating "build\patterns" directory...
	mkdir build\patterns
)

:: create backup folder
if not exist build\backup (
	echo ### Creating "build\backup" directory...
	mkdir build\backup
)

echo ### Copy images and dlls

:: copying image and dll files
copy /Y dll\PdfSharp.dll build\libraries
copy /Y resources\noimage.png build\images
copy /Y resources\transparent.png build\images
copy /Y resources\cdrestore.ico build\icons
copy /Y resources\cdesigner.ico build\icons
copy /Y properties\cdrestore\update.lst build
copy /Y resources\icons\image-field.png build\icons
copy /Y resources\icons\text-field.png build\icons
copy /Y resources\icons\exit-application.png build\icons
copy /Y resources\icons\cdrestore-512.png build\icons
copy /Y resources\icons\cdrestore-256.png build\icons
copy /Y resources\icons\cdrestore-128.png build\icons
copy /Y resources\icons\cdrestore-96.png build\icons
copy /Y resources\icons\cdrestore-64.png build\icons
copy /Y resources\icons\cdrestore-48.png build\icons
copy /Y resources\icons\cdrestore-32.png build\icons
copy /Y resources\icons\cdrestore-16.png build\icons
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
