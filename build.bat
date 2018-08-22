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

:: check csc command exist in system
where csc
if %ERRORLEVEL% neq 0 (
	echo ### Command csc not exist
	exit /B 1
)

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
	/appconfig:properties\app.config ^
	/win32manifest:properties\app.manifest ^
	/win32icon:resources\cdesigner.ico ^
	%target% ^
	%define% ^
	/utf8output ^
		src\utils\AssemblyLoader.cs ^
		src\utils\DataBackup.cs ^
		src\utils\DataFilter.cs ^
		src\utils\DataStorage.cs ^
		src\utils\IOFileData.cs ^
		src\utils\Language.cs ^
		src\utils\PatternEditor.cs ^
		src\utils\Program.cs ^
		src\utils\ProgressStream.cs ^
		src\utils\Settings.cs ^
		src\utils\Structures.cs ^
		src\utils\UpdateApp.cs ^
		src\controls\AlignedPage.cs ^
		src\controls\AlignedPictureBox.cs ^
		src\controls\PageField.cs ^
		src\forms\DatafileSettingsForm.cs ^
		src\forms\DataReaderForm.cs ^
		src\forms\EditColumnsForm.cs ^
		src\forms\EditRowsForm.cs ^
		src\forms\InfoForm.cs ^
		src\forms\MainForm.cs ^
		src\forms\NewPatternForm.cs ^
		src\forms\UpdateForm.cs ^
		designer\DatafileSettingsForm.Designer.cs ^
		designer\DataReaderForm.Designer.cs ^
		designer\EditColumnsForm.Designer.cs ^
		designer\EditRowsForm.Designer.cs ^
		designer\InfoForm.Designer.cs ^
		designer\MainForm.Designer.cs ^
		designer\NewPatternForm.Designer.cs ^
		designer\UpdateForm.Designer.cs ^
		properties\AssemblyInfo.cs

:: compile CDRestore application using csc
echo ### Compiling CDRestore application...

csc /out:build\cdrestore.exe ^
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
		properties\cdrestore\AssemblyInfo.cs

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

:: create languages folder
if not exist build\languages (
	echo ### Creating "build\languages" directory...
	mkdir build\languages
)

echo ### Copy images and dlls

:: copying image and dll files
copy /Y dll\PdfSharp.dll build\libraries
copy /Y resources\noimage.png build\images
copy /Y resources\transparent.png build\images
copy /Y resources\cdrestore.ico build\icons
copy /Y resources\cdesigner.ico build\icons
copy /Y resources\information.jpg build\images
copy /Y properties\cdrestore\update.lst build
copy /Y resources\icons\image-field.png build\icons
copy /Y resources\icons\text-field.png build\icons
copy /Y resources\icons\exit-application.png build\icons
copy /Y resources\logo\cdrestore-512.png build\images
copy /Y resources\logo\cdrestore-256.png build\images
copy /Y resources\logo\cdrestore-128.png build\images
copy /Y resources\logo\cdrestore-96.png build\images
copy /Y resources\logo\cdrestore-64.png build\images
copy /Y resources\logo\cdrestore-48.png build\images
copy /Y resources\logo\cdrestore-32.png build\images
copy /Y resources\logo\cdrestore-16.png build\images
copy /Y resources\logo\cdesigner-512.png build\images
copy /Y resources\logo\cdesigner-256.png build\images
copy /Y resources\logo\cdesigner-128.png build\images
copy /Y resources\logo\cdesigner-96.png build\images
copy /Y resources\logo\cdesigner-64.png build\images
copy /Y resources\logo\cdesigner-48.png build\images
copy /Y resources\logo\cdesigner-32.png build\images
copy /Y resources\logo\cdesigner-16.png build\images
copy /Y resources\icons\add-pattern.png build\icons
copy /Y resources\icons\first-page.png build\icons
copy /Y resources\icons\item-add.png build\icons
copy /Y resources\icons\item-delete.png build\icons
copy /Y resources\icons\last-page.png build\icons
copy /Y resources\icons\next-page.png build\icons
copy /Y resources\icons\prev-page.png build\icons
copy /Y resources\icons\refresh.png build\icons
copy /Y resources\icons\split-rows.png build\icons
copy /Y languages\eng.lex build\languages
copy /Y languages\pol.lex build\languages
copy /Y languages\qps.lex build\languages

echo ### Finished
exit /B 0
