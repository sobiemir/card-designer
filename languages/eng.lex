% ------------------------------------------------------
% Język   : Polski
% Program : CardDesigner
% Wersja  : 0.8.x
% Tłumacz : Kamil Biały
% ------------------------------------------------------
	
% tłumaczenia globalne
% ============================================================

% nazwy formularzy
@FormNames
	|00 Filtrowanie danych
	|01 Ustawienia pliku bazy danych
	|02 Zarządzanie kolumnami
	|03 Edycja danych
	
% nazwy wyskakujących okienek
@MessageNames
	|00 Nieprawidłowy format pliku
	|01 Wybór pliku z bazą danych
	|02 Dodawanie kolumny
	|03 Wczytywanie pliku
	|04 Błąd krytyczny

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
		|00 Kolumny rozdzielone przecinkiem
		
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
		|01 Kolumny
		|02 Podgląd wierszy
		|03 Dostępne kolumny
		|04 Lista filtrów dla kolumny
		|05 Wszystkie kolumny
		|06 STARE KOLUMNY
		|07 NOWE KOLUMNY
		
	% napisy na oknie
	@Labels
		|00 Typ kolumny
		|01 Ustawienia filtrowania
		|02 Typ filtra:
		|03 Modyfikator:
		|04 Wynik:
		|05 Wyklucz znalezione wiersze ze zbioru
		|06 Zastosuj filtr dla wszystkich kopii kolumny
		
	% kroki w formularzu (przejścia)
	@Steps
		|00 1. Kreator kolumn
		|01 2. Filtry i typy
		
	% przyciski
	@Buttons
		|00 Dodaj
		|01 Wyczyść
		|02 Usuń
		|03 Zapisz
		|04 Zaniechaj
		|05 Zmień
		|06 Zmień typ kolumny
		|07 Dopasuj najlepszy
		
	% typy kolumn
	@ColumnTypes
		|00 Tekst
		|01 Liczba
		|02 Data
		|03 Waluta
		|04 Znak
		
	% rodzaje
	@TypeDetail
		|00 Całkowita (32b)
		|01 Całkowita (64b)
		|02 Rzeczywista (1,33)
		|03 Rzeczywista (1.33)
	
	% wyskakujące okienka z wiadomościami
	@Messages
		|00 Dopuszczalne znaki:\nZnaki alfabetu, cyfry, . - + _ oraz spacja.
		|01 Aby dodać kolumnę musisz podać jej nazwę.
		|02 Dodano już kolumnę o nazwie {0}.
		|03 Kolumna o nazwie {0} już istnieje. Zapisanie zmian spowoduje nadpisanie wartości starej kolumny. Dopóki zmiany nie zostaną zapisane, będziesz jeszcze mógł z niej korzystać w tym oknie.\n\nCzy na pewno chcesz utworzyć kolumnę o tej nazwie?
	

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
