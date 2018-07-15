% ------------------------------------------------------
% Język   : Polski
% Program : CardDesigner
% Wersja  : 0.8.x
% Tłumacz : Kamil Biały
% ------------------------------------------------------

% nazwy formularzy
@FormNames
	|00 Filtrowanie danych
	|01 Ustawienia pliku bazy danych
	|02 Zarządzanie kolumnami
	|03 Edycja danych
	
% nazwy wyskakujących okienek
@MessageNames
	|00 Nieprawidłowy format pliku
	|01 Wybór pliku bazy danych
	|02 Dodawanie kolumny
	|03 Wczytywanie pliku
	
@GlobalErrors
	|00 Błąd krytyczny
	|01 Aplikacja jest już uruchomiona!\nNie można uruchomić dwóch instancji aplikacji CDesigner!
	|02 Nie można uruchomić aplikacji CDesigner ponieważ aplikacja CDRestore jest uruchomiona.
	
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

% formularz ustawień pliku bazy danych
#DatabaseSettings

	% pole wyboru kodowania
	@ComboBox
		|00 Domyślne
		|01 ASCII
		|02 UTF-8
		|03 UTF-16 BigEndian
		|04 UTF-16 LittleEndian
		|05 UTF-32
		|06 UTF-7
		
	% nagłówki tabel
	@Headers
		|00 Podgląd wierszy
		|01 Kolumny
		
	% przyciski
	@Buttons
		|00 Zmień
		|01 Zatwierdź
		
	% wiadomości - z DatabaseReader
	@Messages
		|00 Brak obsługi bazy danych o podanym rozszerzeniu: {0}.\nObsługiwane rozszerzenia: {1}.
		
% edycja kolumn i wierszy		
#EditColumns

	% nagłówki tabel
	@Headers
		|00 Nazwa
		|01 Kolumny
		|02 Podgląd wierszy
		|03 Dostępne kolumny
		
	% przyciski
	@Buttons
		|00 Dodaj
		|01 Wyczyść
		|02 Usuń
		|03 Filtruj dane
		|04 Zapisz
	
	% wyskakujące okienka z wiadomościami
	@Messages
		|00 Dopuszczalne znaki:\nZnaki alfabetu, cyfry, . - + _ oraz spacja.
		|01 Aby dodać kolumnę musisz podać jej nazwę.
		|02 Dodano już kolumnę o nazwie {0}.
		|03 Kolumna o nazwie {0} już istnieje. Zapisanie zmian spowoduje nadpisanie wartości starej kolumny. Dopóki zmiany nie zostaną zapisane, będziesz jeszcze mógł z niej korzystać w tym oknie.\n\nCzy na pewno chcesz utworzyć kolumnę o tej nazwie?

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
