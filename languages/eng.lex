$pol Angielski
$eng English
$def English

% ------------------------------------------------------
% Język   : Polski
% Program : CardDesigner
% Wersja  : 0.8.x
% Tłumacz : Kamil Biały
% ------------------------------------------------------
	
% tłumaczenia globalne
% ============================================================

% nazwy rozszerzeń
@Extensions
	|00 Comma Separated Values
	|01 Compressed Backup Data
	
% znaki specjalne dostępne dla danego języka
@Locale
	|00 
	|01 

% nazwy formularzy
@FormNames
	|00 Data filter
	|01 Database settings
	|02 Column manager
	|03 Data editor
	|04 New pattern
	|05 About program
	|06 Program update
	|07 Column type settings
	|08 Column assignment
	|09 CDesigner - Document series creator
	
% nazwy wyskakujących okienek
@MessageNames
	|00 Incorrect file format
	|01 Choose file
	|02 Column add
	|03 File loading
	|04 Critical error
	|05 Localization settings
	|06 Pattern creation
	|07 Save as
	|08 Pattern importer
	|09 Database manager
	|10 Pattern editor

% błędy globalne
@GlobalErrors
	|00 Critical error!
	|01 Application is already running!\nYou can't run two instances of CDesigner application!
	|02 You can't run CDesigner application, becouse CDRestore is running. 
	|03 This option is not available in current version.
	
% formularz ustawień pliku bazy danych i wczytywania pliku
% ============================================================
#DatafileSettings
		
	% pole wyboru kodowania
	@Encoding
		|00 Default encoding
		|01 ASCII
		|02 UTF-8
		|03 UTF-16 BigEndian
		|04 UTF-16 LittleEndian
		|05 UTF-32
		|06 UTF-7
	
	% pole wyboru typu separatora
	@Separator
		|00 Semicolon
		|01 Comma
		|02 Dot
		|03 Tab
		|04 Space
		|05 Other char
		
	% nagłówki tabel
	@Headers
		|00 Rows preview
		|01 Columns
		
	% przyciski
	@Buttons
		|00 Change file
		|01 Load
		|02 Cancel
		
	% wiadomości informacji / błędów
	@Messages
		|00 File extension '{0}' is not supported by program.\nSupported extensions: {1}.

	% napisy na formularzu
	@Labels
		|00 Auto detect column types
		|01 Without header

% formularz edycji kolumn i wierszy
% ============================================================
#EditColumns

	% nagłówki tabel
	@Headers
		|00 Name
		|01 Assigned columns
		|02 Rows preview
		|03 Available columns
		|04 Filters for column
		|06 OLD COLUMNS
		|07 NEW COLUMNS
		
	% napisy na oknie
	@Labels
		|00 Column type
		|01 Filter settings
		|02 Filter type
		|03 Modifier
		|04 Result
		|05 Exclude found rows from collection
		|06 Apply filter for all column copies
		
	% kroki w formularzu (przejścia)
	@Steps
		|00 Column creator
		|01 Filter and types
		
	% przyciski
	@Buttons
		|00 Add
		|01 Clear
		|02 Remove
		|03 Save
		|04 Cancel
		|05 Change
		|06 Apply
		|07 Advanced
		
	% typy kolumn
	@ColumnTypes
		|00 Text
		|01 Number
		|02 Percent
		|03 Date
		|04 Currency
		|05 Character
		|06 Default
		
	% rodzaje
	@FilterType
		|00 Lowercase
		|01 Uppercase
		|02 Title case
		|03 Equal
		|04 Not equal
		|05 Format
	
	% wyskakujące okienka z wiadomościami
	@Messages
		|00 Available characters:\nAlphabetic characters, numbers, . - + _ and space.
		|01 Column name cannot be empty.
		|02 This column, named '{0}', was created before!
		|03 Column '{0}' already exists. If you save your changes, then old column with this name will be overwritten. You can use this column as long as you not saving changes.\n\nDo you really want to create this column?
	
% formularz edycji wierszy
% ============================================================
#EditRows
	
	% napisy na oknie
	@Labels
		|00 Rows per page:
		|01 from {0}
		
	@Buttons
		|00 Save
		|01 Cancel
		
% formularz dodawania nowego wzoru
% ============================================================
#NewPattern

	% napisy na oknie
	@Labels
		|00 Pattern name:
		|01 Copy from pattern:
		|02 Paper format:
		|03 Size ( mm ):
	
	% domyślne wartości dwóch pól wyboru
	@ComboBox
		|00 ---
		|01 Custom
		
	% przyciski
	@Buttons
		|00 Create
		|01 Cancel
		
	% wyskakujące wiadomości
	@Messages
		|00 You must pass a pattern name.
		|01 Pattern with this name already exists!
		|02 You must pass paper size for pattern.
		|03 Program can't create pattern in main directory.\n Check if you are allowed to save data here.
		|04 Allow characters:\nAlphabetic characters, numbers, - + _ ( ) [ ] , #  and space.

% okno z informacją o programie
% ============================================================
#Info
	
	% napisy na oknie
	@Labels
		|00 Compilation date:
		|01 Author:
		|02 Program copy details
		|03 Registered for:
		|04 Serial number:
		|05 Expiration date:
		|06 Never
		
	% przyciski
	@Buttons
		|00 Close

% okno z aktualizacją programu
% ============================================================
#Update

	% etykiety
	@Labels
		|00 New version is available!
		|01 Update is not available, you have the newest version.
		|02 Program version:
		|03 Update version:
		|04 Changelog
		
	% przyciski akcji
	@Buttons
		|00 Update
		|01 Close
		|02 Compress
		
	% wyskakujące okna z wiadomościami
	@Messages
		|00 Program cannot connect to server.\nPlease, check your internet connection.
		|01 Error occured during connecting with server.\nConcat with your admin.
		|02 Compression mode activated.
		|03 Compression mode deactivated.
		|04 Error occured during downloading program update.
		|05 You can't close this window during program update.
		|06 Error occured during creating the archive.
		|07 File compression completed successfully.
		|08 Error occured during extraction the archive.\nContact with your admin.
		|09 Update is extracted and ready for install.\nDo you want to restart program and install it?
	
% formularz przypisywania kolumn do pól
% ============================================================
#DataReader
	
	% nagłówki kolumn
	@Headers
		|00 Available fields
		|01 Assigned columns
		|02 Available columns
		
	% napisy na formularzu
	@Labels
		|00 Page:
		|01 no column...
		
	% przyciski
	@Buttons
		|00 Save
		|01 Cancel
		
	% wiadomości
	@Messages
		|00 Available characters:\nAlphabetic characters, numbers, - + _ # : [ ] < > and space.
		|01 Format text should not have characters other than:\nAlphabetic characters, numbers, - + _ # : [ ] < > and space.
		|02 Format does not have column named '{0}' (#{1}).\nDo you really want to skip it?
		|03 Format text can't be empty.
	
% Lista pozycji w menu
% ============================================================	
#Menu

	% menu wzorów
	@Pattern
		|00 &Pattern
		|01 New...
		|02 Recently opened
		|03 Clear pattern list
		|04 Close
		|05 Import...
		|06 Export all available...
	
	% menu dla narzędzi
	@Tools
		|00 &Tools
		|01 Load file with data...
		|02 Create memory database
		|03 Edit columns...
		|04 Edit rows...
		|05 Save database to file
		|06 Close data source
		
	% menu języków
	@Language
		|00 &Language
		
	% menu informacji o programie
	@Program
		|00 &Program
		|01 Information
		|02 Updates
		
	% przełącznik pomiędzy formularzami
	@Switcher
		|00 Home
		|01 Editor
		|02 Printout
		
	@Messages
		|00 Created empty database in computer memory.
		|01 This pattern is not exists and will be removed from list!

% okno zawierające listę wzorów
% ============================================================
#PatternList

	% przyciski na formularzu
	@Buttons
		|00 New pattern
		|01 Delete
	
	% lista kontrolek z napisem na oknie
	@Labels
		|00 Show details of selected pattern
		|01 Page:
		
	% informacje o wzorze
	@Pattern
		|00 Name: {0}.
		|01 Format: {0}.
		|02 Size: {0} x {1} mm.
		|03 Data fields: {0}.
		|04 Page count: {0}.
		|05 Custom
		|06 Yes
		|07 No
		
	% menu wyświetlane po kliknięciu prawym w listę wzorów
	@PatternContext
		|00 New pattern...
		|01 Edit selected
		|02 Preview
		|03 Load data...
		|04 Import...
		|05 Export selected
		|06 Remove pattern

	@Messages
		|00 There is always a risk, that importing file will having folders with patterns, that already exists. In this case, all patterns with the same names will be overwritten.\nDo you really want to continue import process?
		|01 Import was completed successfully.
		|02 Do you really want to remove this pattern?
		|03 This pattern is damaged, becouse his configuration file does not exists!
		
% formularz edycji wzoru
% ============================================================
#PatternEditor

	% menu wyświetlane po kliknięciu prawym w stronę
	@PageContext
		|00 Add field
		|01 Remove all fields
		|02 Page color...
		|03 Page image...
		|04 Clear background
		|05 Draw page color
		|06 Draw page image
		|07 Add page
		|08 Remove page
	
	% menu wyświetlane po kliknięciu prawym w pole
	@LabelContext
		|00 Field color...
		|01 Field image...
		|02 Clear background
		|03 Dynamic image
		|04 Draw field color
		|05 Static image
		|06 Font color...
		|07 Change font...
		|08 Dynamic text
		|09 Static text
		|10 Border color...
		|11 Display border
		|12 Remove
		
	% przyciski
	@Buttons
		|00 Load data
		|01 Preview
		|02 Save
		|03 Page color
		|04 Page image
		|05 Border color
		|06 Field color
		|07 Field image
		|08 Font name
		|09 Font color
		
	% przełącznik pomiędzy panelami
	@Switcher
		|00 Field
		|01 Details
		|02 Page

	% napisy na formularzu
	@Labels
		|00 Page:
		|01 Position X ( mm ):
		|02 Position Y ( mm ):
		|03 Width ( mm ):
		|04 Height ( mm ):
		|05 Field appearance:
		|06 Font settings:
		|07 Text position:
		|08 Margin:
		|09 Text transform:
		|10 Border size:
		|11 Field to PDF generation settings:
		|12 Image settings:
		|13 Anchor position:
		|14 Additional text margin:
		|15 Page width:
		|16 Page height:
		|17 Page appearance:
		|18 Page to PDF generation settings:
		|19 Image settings:
		|20 new field

	% pozycja tekstu
	@TextPosition
		|00 Top - Left
		|01 Top - Middle
		|02 Top - Right
		|03 Middle - Left
		|04 Center
		|05 Middle - Right
		|06 Bottom - Left
		|07 Bottom - Middle
		|08 Bottom - Right
		
	% transformacja tekstu
	@TextTransform
		|00 Do not change
		|01 Uppercase
		|02 Lowercase
		|03 Title case

	% pola zaznaczenia
	@Checkboxes
		|00 Auto save without creating preview
		|01 Generate with page color
		|02 Generate with assigned image
		|03 Apply margin for image
		|04 Draw border outside the image
		|05 Display border around field
		|06 Generate with field color
		|07 Draw text from dynamic data source
		|08 Display static text on field
		|09 Generate with assigned image
		|10 Dynamic image
		|11 Draw border outside the image
		|12 Apply margin for image
		|13 Apply additional margin for text
		
	% pozycja punktu zaczepienia
	@AnchorPosition
		|00 Top - Left ( top left corner )
		|01 Top - Right ( top right corner )
		|02 Bottom - Left ( bottom left corner )
		|03 Bottom - Right ( bottom right corner )

	% wyskakujące wiadomości
	@Messages
		|00 Available characters:\nAlphabetic characters, numbers, - + _ and space.
		|01 Pattern has only one page - you can't remove it!
		
% formularz podglądu wydruku
% ============================================================
#PrintoutPreview

	% przyciski
	@Buttons
		|00 Find errors
		|01 Generate PDF

	% napisy na formularzu
	@Labels
		|00 Page:
		|01 Preview row of static pattern
		|02 Number of copies:
		|03 Collate pattern pages on printout
