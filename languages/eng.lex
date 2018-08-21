% $pol Angielski
% $eng English
% $def English

% ------------------------------------------------------
% Język   : Polski
% Program : CardDesigner
% Wersja  : 0.8.x
% Tłumacz : Kamil Biały
% ------------------------------------------------------
	
% tłumaczenia globalne
% ============================================================

% znaki specjalne dostępne dla danego języka
@Locale
	|00 
	|01 

% nazwy formularzy
@FormNames
	|00 Filtrowanie danych
	|01 Ustawienia pliku bazy danych
	|02 Zarządzanie kolumnami
	|03 Edycja danych
	|04 New pattern
	|05 About program
	|06 Update window
	|07 Ustawienia typu kolumny
	
% nazwy wyskakujących okienek
@MessageNames
	|00 Nieprawidłowy format pliku
	|01 Wybór pliku z bazą danych
	|02 Dodawanie kolumny
	|03 Wczytywanie pliku
	|04 Błąd krytyczny
	|05 Ustawienia lokalizacji
	|06 Tworzenie wzoru

% błędy globalne
@GlobalErrors
	|00 Błąd krytyczny
	|01 Aplikacja jest już uruchomiona!\nNie można uruchomić dwóch instancji aplikacji CDesigner!
	|02 Nie można uruchomić aplikacji CDesigner ponieważ aplikacja CDRestore jest uruchomiona.
	|03 Wybrana opcja nie jest dostępna w tej wersji programu.
	
% formularz ustawień pliku bazy danych i wczytywania pliku
% ============================================================
#DatafileSettings

	% nazwy rozszerzeń
	@Extensions
		|00 Kolumny oddzielone przecinkiem
		
	% pole wyboru kodowania
	@Encoding
		|00 Domyślne kodowanie
		|01 ASCII
		|02 UTF-8
		|03 UTF-16 BigEndian
		|04 UTF-16 LittleEndian
		|05 UTF-32
		|06 UTF-7
	
	% pole wyboru typu separatora
	@Separator
		|00 Średnik
		|01 Przecinek
		|02 Kropka
		|03 Tabulator
		|04 Spacja
		|05 Inny znak
		
	% nagłówki tabel
	@Headers
		|00 Podgląd wierszy
		|01 Kolumny
		
	% przyciski
	@Buttons
		|00 Zmień plik
		|01 Wczytaj
		|02 Zaniechaj
		
	% wiadomości informacji / błędów
	@Messages
		|00 Brak obsługi bazy danych o podanym rozszerzeniu: {0}.\nObsługiwane rozszerzenia: {1}.

	% napisy na formularzu
	@Labels
		|00 Automatycznie wykrywaj typy kolumn
		|01 Bez nagłówka

% formularz edycji kolumn i wierszy
% ============================================================
#EditColumns

	% nagłówki tabel
	@Headers
		|00 Nazwa
		|01 Przypisane kolumny
		|02 Podgląd wierszy
		|03 Dostępne kolumny
		|04 Filtry dla kolumny
		|06 STARE KOLUMNY
		|07 NOWE KOLUMNY
		
	% napisy na oknie
	@Labels
		|00 Typ kolumny
		|01 Ustawienia filtrowania
		|02 Typ filtra
		|03 Modyfikator
		|04 Wynik
		|05 Wyklucz znalezione wiersze ze zbioru
		|06 Zastosuj filtr dla wszystkich kopii kolumny
		
	% kroki w formularzu (przejścia)
	@Steps
		|00 Kreator kolumn
		|01 Filtry i typy
		
	% przyciski
	@Buttons
		|00 Dodaj
		|01 Wyczyść
		|02 Usuń
		|03 Zapisz
		|04 Anuluj
		|05 Zmień
		|06 Zatwierdź
		|07 Zaawansowane
		
	% typy kolumn
	@ColumnTypes
		|00 Tekst
		|01 Liczba
		|02 Procent
		|03 Data
		|04 Waluta
		|05 Znak
		|06 Domyślny
		
	% rodzaje
	@FilterType
		|00 Małe litery
		|01 Duże litery
		|02 Nazwa własna
		|03 Równy
		|04 Różny
		|05 Format
	
	% wyskakujące okienka z wiadomościami
	@Messages
		|00 Dopuszczalne znaki:\nZnaki alfabetu, cyfry, . - + _ oraz spacja.
		|01 Aby dodać kolumnę musisz podać jej nazwę.
		|02 Dodano już kolumnę o nazwie {0}.
		|03 Kolumna o nazwie {0} już istnieje. Zapisanie zmian spowoduje nadpisanie wartości starej kolumny. Dopóki zmiany nie zostaną zapisane, będziesz jeszcze mógł z niej korzystać w tym oknie.\n\nCzy na pewno chcesz utworzyć kolumnę o tej nazwie?
	
% formularz edycji wierszy
% ============================================================
#EditRows
	
	% napisy na oknie
	@Labels
		|00 Ilość wierszy na stronę:
		|01 z {0}
		
	@Buttons
		|00 Zapisz
		|01 Zaniechaj
		
% formularz dodawania nowego wzoru
% ============================================================
#NewPattern

	% napisy na oknie
	@Labels
		|00 Pattern name:
		|01 Copy from pattern:
		|02 Paper format:
		|03 Size (mm):
	
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
		|04 Allow characters:\nAlphabetic characters, numbers, - + _ # and space.

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
		|00 Dostępna jest nowa wersja programu!
		|01 Posiadasz najnowszą wersję programu.
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
		|00 Nie można połączyć się z serwerem.\nSprawdź swoje połączenie z internetem.
		|01 Wystąpił błąd podczas próby połączenia z serwerem.\nSkontaktuj się z administratorem.
		|02 Tryb kompresji został aktywowany.
		|03 Tryb kompresji został dezaktywowany.
		|04 Wystąpił bład podczas pobierania aktualizacji.
		|05 Nie możesz zamknąć tego okna podczas aktualizacji programu.
		|06 Wystąpił błąd podczas kompresji plików.
		|07 Kompresja plików zakończona sukcesem.
		|08 Wystąpił błąd podczas rozpakowywania plików.\nSkontaktuj się z administratorem.
		|09 Aktualizacja została przygotowana do instalacji.\nCzy chcesz uruchomić ponownie program aby ją zainstalować?
	
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
	
	% menu dla narzędzi
	@Tools
		|00 &Tools
		|01 Load file with data...
		|02 Create database in memory
		|03 Edit columns...
		|04 Edit rows...
		|05 Save database to file
		
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

% okno zawierające listę wzorów
% ============================================================
#PatternList

	% przyciski na formularzu
	@Buttons
		|00 New pattern
		|01 Delete
	
	% lista kontrolek z napisem na oknie
	@Labels
		|00 Show details for selected pattern
		|01 Page
		
	% informacje o wzorze
	@Pattern
		|00 Nazwa
		|01 Format
		|02 Rozmiar
		|03 Miejsce na dane
		|04 Ilość stron
		
	% menu wyświetlane po kliknięciu prawym w listę wzorów
	@PatternContext
		|00 Nowy wzór...
		|01 Edytuj zaznaczony
		|02 Podgląd
		|03 Wczytaj dane...
		|04 Importuj...
		|05 Eksportuj
		|06 Usuń wzór
		
% formularz edycji wzoru
% ============================================================
#PatternEditor

	% menu wyświetlane po kliknięciu prawym w stronę
	@PageContext
		|00 Dodaj pole
		|01 Usuń wszystkie pola
		|02 Kolor tła strony...
		|03 Obraz tła strony...
		|04 Wyczyść tło
		|05 Rysuj kolor strony
		|06 Rysuj obraz strony
		|07 Dodaj stronę
		|08 Usuń stronę
	
	% menu wyświetlane po kliknięciu prawym w pole
	@LabelContext
		|00 Kolor tła pola...
		|01 Obraz tła pola...
		|02 Wyczyść tło
		|03 Obraz dynamiczny
		|04 Rysuj kolor pola
		|05 Obraz statyczny
		|06 Kolor czcionki...
		|07 Zmień czcionkę...
		|08 Tekst dynamiczny
		|09 Tekst statyczny
		|10 Kolor ramki...
		|11 Wyświetlaj ramkę
		|12 Usuń
		
% formularz ustawień typu kolumny
% ============================================================
#TypeSettings
	
	% napisy na oknie
	@Labels
		|00 {{SzczegolowyTypKolumny}}
		|01 {{UstawieniaZLokalizacji}}
		|02 {{FormatDaty}}
		|03 {{SeparatorCzesci}}
		|04 {{SeparatorGrup}}
		|05 {{WzorzecLiczbyUjemnej}}
		|06 {{WzorzecLiczbyDodatniej}}
		|07 {{DodatkowySymbolLiczby}}
		|08 {{SymbolNaN}}
		|09 {{MinusNieskonczonosc}}
		|10 {{PlusNieskonczonosc}}
		|11 {{ZnakLiczbyUjemnej}}
		|12 {{ZnakLiczbyDodatniej}}
		|13 {{PodgladFormatuDaty}}
		
	% przyciski
	@Buttons
		|00 {{Zapisz}}
		|01 {{Zaniechaj}}
		|02 {{Wczytaj}}
	
	% szczegółowe typy kolumn
	@ColumnSubTypes
		|00 {{Calkowity16}}
		|01 {{Calkowity32}}
		|02 {{Calkowity64}}
		|03 {{PojedynczaPrecyzja}}
		|04 {{PodwojnaPrecyzja}}
		|05 {{Numeryczny}}
		|06 {{Procenty}}
		|07 {{Promile}}
		|08 {{---}}
		
	% wiadomości w wyskakujących okienkach
	@Messages
		|00 {{LokalizacjaOKodzie{0}NieIstnieje}}
		|01 {{PodanyFormatDatyJestNiepoprawny}}
		
	
% formularz filtrowania danych
#DataFilter


	% nagłówki tabeli
	@Headers
		|00 Kolumna
		|01 Typ filtra
		|02 Modyfikator
		|03 Wynik
		|04 W
		
	% nazwy kolumn dla listy rozwijanej
	@GroupComboBox
		|00 Kolumny
		|01 Stare kolumny
		|02 Nowe kolumny
		
	% nazwy filtrów
	@ComboBox
		|00 Format
		|01 Duże litery
		|02 Małe litery
		|03 Nazwa własna
		|04 Różny [<>]
		|05 Równy [==]

	% przyciski na formularzu
	@Buttons
		|00 Dodaj
		|01 Usuń
		|02 Przywróć
		|03 Zastosuj



% edycja danych z bazy
#EditData

	@Labels
		|00 z {0}
		|01 Ilość wierszy na stronę:
		|02 Typ kolumny
		
	@ComboBoxTypes
		|00 Tekst ({0})
		|01 Całkowity ({0})
		|02 Dziesiętny ({0})
		|03 Znak ({0})
