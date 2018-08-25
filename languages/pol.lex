$pol Polski
$eng Polish
$rus Польский
$deu Polnisch
$ces Polský
$def Polski

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
	|00 Kolumny oddzielone przecinkiem
	|01 Skompresowana kopia zapasowa
	
% znaki specjalne dostępne dla danego języka
@Locale
	|00 ĘÓŁŚĄŻŹĆŃ
	|01 ęółśążźćń

% nazwy formularzy
@FormNames
	|00 Filtrowanie danych
	|01 Ustawienia pliku bazy danych
	|02 Zarządzanie kolumnami
	|03 Edycja danych
	|04 Nowy wzór
	|05 O programie
	|06 Aktualizacja oprogramowania
	|07 Ustawienia typu kolumny
	|08 Przypisywanie kolumn
	|09 CDesigner - Kreator serii dokumentów
	
% nazwy wyskakujących okienek
@MessageNames
	|00 Nieprawidłowy format pliku
	|01 Wybór pliku
	|02 Dodawanie kolumny
	|03 Wczytywanie pliku
	|04 Błąd krytyczny
	|05 Ustawienia lokalizacji
	|06 Tworzenie wzoru
	|07 Zapisz jako
	|08 Import wzoru
	|09 Baza danych
	|10 Edytor wzoru

% błędy globalne
@GlobalErrors
	|00 Błąd krytyczny
	|01 Aplikacja jest już uruchomiona!\nNie można uruchomić dwóch instancji aplikacji CDesigner!
	|02 Nie można uruchomić aplikacji CDesigner ponieważ aplikacja CDRestore jest uruchomiona.
	|03 Wybrana opcja nie jest dostępna w tej wersji programu.
	
% formularz ustawień pliku bazy danych i wczytywania pliku
% ============================================================
#DatafileSettings
		
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
		|00 Nazwa wzoru:
		|01 Kopiuj z wzoru:
		|02 Format papieru:
		|03 Rozmiar ( mm ):
		
	% domyślne wartości dwóch pól wyboru
	@ComboBox
		|00 ---
		|01 Własny
		
	% przyciski
	@Buttons
		|00 Utwórz
		|01 Zaniechaj
		
	% wyskakujące wiadomości
	@Messages
		|00 Musisz uzupełnić nazwę wzoru.
		|01 Wzór o podanej nazwie już istnieje!
		|02 Musisz podać wymiary papieru dla wzoru.
		|03 Nie można utworzyć wzoru w folderze głównym programu.\nSprawdź czy posiadasz odpowiednie uprawnienia do zapisywania danych w tym katalogu.
		|04 Dopuszczalne znaki:\nZnaki alfabetu, cyfry, - + _ ( ) [ ] , # oraz spacja.

% okno z informacją o programie
% ============================================================
#Info
	
	% napisy na oknie
	@Labels
		|00 Data kompilacji:
		|01 Autor:
		
	% przyciski
	@Buttons
		|00 Zamknij

% okno z aktualizacją programu
% ============================================================
#Update

	% etykiety
	@Labels
		|00 Dostępna jest nowa wersja programu!
		|01 Posiadasz najnowszą wersję programu.
		|02 Twoja wersja:
		|03 Wersja aktualizacji:
		|04 Lista zmian w poszczególnych wersjach
		
	% przyciski akcji
	@Buttons
		|00 Aktualizuj
		|01 Zamknij
		|02 Kompresuj
		
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
	
% formularz przypisywania kolumn do pól
% ============================================================
#DataReader
	
	% nagłówki kolumn
	@Headers
		|00 Dostępne pola na stronie
		|01 Przyporządkowane kolumny
		|02 Dostępne kolumny
		
	% napisy na formularzu
	@Labels
		|00 Strona:
		|01 brak kolumny...
		
	% przyciski
	@Buttons
		|00 Zapisz
		|01 Zaniechaj
		
	% wiadomości
	@Messages
		|00 Dopuszczalne znaki:\nZnaki alfabetu, cyfry, - + _ # : [ ] < > oraz spacja.
		|01 Tekst formatujący nie może zawierać znaków innych niż:\nZnaki alfabetu, cyfry, - + _ # : [ ] < > oraz spacja.
		|02 Wybrany format nie zawiera zaznaczonej kolumny '{0}' (#{1}).\nCzy na pewno chcesz ją pominąć?
		|03 Format wyświetlanych danych nie może być pusty.
	
% Lista pozycji w menu
% ============================================================	
#Menu

	% menu wzorów
	@Pattern
		|00 &Wzór
		|01 Nowy...
		|02 Ostatnio otwierane
		|03 Wyczyść listę wzorów
		|04 Zakończ program
		|05 Importuj...
		|06 Eksportuj wszystkie...
	
	% menu dla narzędzi
	@Tools
		|00 &Narzędzia
		|01 Wczytaj plik z danymi...
		|02 Utwórz bazę w pamięci
		|03 Edytuj kolumny...
		|04 Edytuj wiersze...
		|05 Zapisz bazę do pliku
		|06 Zamknij źródło danych
		
	% menu języków
	@Language
		|00 &Język
		
	% menu informacji o programie
	@Program
		|00 &Program
		|01 Informacje
		|02 Aktualizacje
		
	% przełącznik pomiędzy formularzami
	@Switcher
		|00 Główna
		|01 Edytor
		|02 Wydruk
		
	@Messages
		|00 Utworzono pustą bazę danych w pamięci operacyjnej komputera.
		|01 Wybrany wzór nie istnieje i zostanie usunięty z listy!

% okno zawierające listę wzorów
% ============================================================
#PatternList

	% przyciski na formularzu
	@Buttons
		|00 Nowy wzór
		|01 Usuń
	
	% lista kontrolek z napisem na oknie
	@Labels
		|00 Pokaż szczegóły zaznaczonego wzoru
		|01 Strona:
		
	% informacje o wzorze
	@Pattern
		|00 Nazwa: {0}.
		|01 Format: {0}.
		|02 Rozmiar: {0} x {1} mm.
		|03 Miejsce na dane: {0}.
		|04 Ilość stron: {0}.
		|05 Własny
		|06 Tak
		|07 Nie
		
	% menu wyświetlane po kliknięciu prawym w listę wzorów
	@PatternContext
		|00 Nowy wzór...
		|01 Edytuj zaznaczony
		|02 Podgląd
		|03 Wczytaj dane...
		|04 Importuj...
		|05 Eksportuj
		|06 Usuń wzór
		
	@Messages
		|00 Zawsze istnieje ryzyko, że importowany plik może zawierać foldery z wzorami, które już istnieją. W takim wypadku wszystkie wzory o tych samych nazwach zostaną zastąpione.\nCzy na pewno chcesz kontynuować import?
		|01 Dane z pliku zostały zaimportowane
		|02 Czy na pewno chcesz usunąć wybrany wzór?
		|03 Wybrany wzór jest uszkodzony ponieważ nie posiada pliku konfiguracyjnego.
		
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
		
	% przyciski
	@Buttons
		|00 Wczytaj dane
		|01 Podgląd
		|02 Zapisz
		|03 Kolor strony
		|04 Obraz strony
		|05 Kolor ramki
		|06 Kolor pola
		|07 Obraz pola
		|08 Nazwa czcionki
		|09 Kolor czcionki
		
	% przełącznik pomiędzy panelami
	@Switcher
		|00 Pole
		|01 Szczegóły
		|02 Strona
		
	% napisy na formularzu
	@Labels
		|00 Strona:
		|01 Pozycja X ( mm ):
		|02 Pozycja Y ( mm ):
		|03 Szerokość ( mm ):
		|04 Wysokość ( mm ):
		|05 Wygląd pola:
		|06 Ustawienia czcionki:
		|07 Położenie tekstu:
		|08 Margines:
		|09 Wyświetlanie tekstu:
		|10 Grubość ramki:
		|11 Ustawienia generowania PDF:
		|12 Ustawienia obrazu:
		|13 Punkt zaczepienia pola:
		|14 Dodatkowy margines tekstu:
		|15 Szerokość strony:
		|16 Wysokość strony:
		|17 Wygląd strony:
		|18 Ustawienia generowania PDF:
		|19 Ustawienia obrazu:
		|20 nowe pole
		
	% pozycja tekstu
	@TextPosition
		|00 Góra - Lewo
		|01 Góra - Środek
		|02 Góra - Prawo
		|03 Środek - Lewo
		|04 Środek
		|05 Środek - Prawo
		|06 Dół - Lewo
		|07 Dół - Środek
		|08 Dół - Prawo
		
	% transformacja tekstu
	@TextTransform
		|00 Nie zmieniaj
		|01 Duże litery
		|02 Małe litery
		|03 Nazwa własna
	
	% pola zaznaczenia
	@Checkboxes
		|00 Automatyczny zapis bez tworzenia podglądu
		|01 Generuj razem z kolorem strony
		|02 Generuj razem z ustawionym obrazem
		|03 Zastosuj margines do obrazu
		|04 Rysuj ramkę na zewnątrz obrazu
		|05 Wyświetlaj ramkę wokół pola
		|06 Generuj razem z kolorem pola
		|07 Tekst dynamiczny pobierany z bazy
		|08 Rysuj tekst statyczny na polu
		|09 Generuj razem z ustawionym obrazem
		|10 Obraz dynamiczny
		|11 Rysuj ramkę na zewnątrz obrazu
		|12 Zastosuj margines do obrazu
		|13 Zastosuj dodatkowy margines tekstu
		
	% pozycja punktu zaczepienia
	@AnchorPosition
		|00 Góra - Lewo ( lewy górny róg )
		|01 Góra - Prawo ( prawy górny róg )
		|02 Dół - Lewo ( lewy dolny róg )
		|03 Dół - Prawo ( prawy dolny róg )
		
	% wyskakujące wiadomości
	@Messages
		|00 Dopuszczalne znaki:\nZnaki alfabetu, cyfry, - + _ oraz spacja.
		|01 Wzór posiada tylko jedną stronę - nie możesz jej usunąć!
		
% formularz podglądu wydruku
% ============================================================
#PrintoutPreview

	% przyciski
	@Buttons
		|00 Szukaj błędów
		|01 Generuj PDF
		
	% napisy na formularzu
	@Labels
		|00 Strona:
		|01 Pozycja podglądowa statycznego wzoru
		|02 Ilość kopii pozycji:
		|03 Połącz strony wzoru na wydruku
