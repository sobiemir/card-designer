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
	|00 {{FiltrowanieDanych}}
	|01 _PLIKBAZYDANYCH_
	|02 _ZARZADZAJKOLUMNAMI_
	|03 {{EdycjaDanych}}
	|04 {{UstawieniaTypuKolumny}}
	
% nazwy wyskakujących okienek
@MessageNames
	|00 {{NieprawidlowyFormatPliku}}
	|01 _WYBORSTRUMIENIABAZY_
	|02 {{DodawanieKolumny}}
	|03 {{WczytywaniePliku}}
	|04 {{BladKrytyczny}}
	|05 {{UstawieniaLokalizacji}}

% błędy globalne
@GlobalErrors
	|00 {{BladKrytyczny}}
	|01 {{AplikacjaJestJuzUruchomiona!\nNieMoznaUruchomicDwochInstancjiAplikacjiCDesigner!}}
	|02 {{NieMoznaUruchomicAplikacjiCDesignerPoniewazAplikacjaCDRestoreJestUruchomiona.}}
	|03 {{WybranaOpcjaNieJestDostepnaWTejWersjiProgramu.}}
	
% formularz ustawień pliku bazy danych i wczytywania pliku
% ============================================================
#DatafileSettings

	% nazwy rozszerzeń
	@Extensions
		|00 _KOLROZDZPRZEC_
		
	% pole wyboru kodowania
	@Encoding
		|00 _DOMKOD_
		|01 _ASCII_
		|02 _UTF8_
		|03 _UTF16BE_
		|04 _UTF16LE_
		|05 _UTF32_
		|06 _UTF7_
	
	% pole wyboru typu separatora
	@Separator
		|00 _SREDNIK_
		|01 _PRZECINEK_
		|02 _KROPKA_
		|03 _TABULATOR_
		|04 _SPACJA_
		|05 _INNYZNAK_
		
	% nagłówki tabel
	@Headers
		|00 _PODGLADWIERSZY_
		|01 _KOLUMNY_
		
	% przyciski
	@Buttons
		|00 _ZMIEN_
		|01 _WCZYTAJ_
		|02 _ANULUJ_
		
	% wiadomości informacji / błędów
	@Messages
		|00 _BRAKOBSLUGIBAZYDANYCH:{0}\nOBSLUGA:{1}_

	% napisy na formularzu
	@Labels
		|00 _AUTOWYKRYJ_
		|01 _BEZNAGL_

% formularz edycji kolumn i wierszy
% ============================================================
#EditColumns

	% nagłówki tabel
	@Headers
		|00 _NAZWA_
		|01 _KOLUMNY_
		|02 _PODGLADWIERSZY_
		|03 _DOSTEPNEKOLUMNY_
		|04 _LISTAFILTROW_
		|05 _STAREKOLUMNY_
		|06 _NOWEKOLUMNY_
		
	% napisy na oknie
	@Labels
		|00 _TYPKOLUMNY_
		|01 _USTAWIENIAFILTRA_
		|02 _TYP_
		|03 _MODYFER_
		|04 _WYNIK_
		|05 _WYKLUCZWIERSZE_
		|06 _ZASTOSUJDLAKOPII_
		
	% kroki w formularzu (przejścia)
	@Steps
		|00 _KREATORKOLUMN_
		|01 _FILTRYITYPY_
		
	% przyciski
	@Buttons
		|00 _DODAJ_
		|01 _CZYSC_
		|02 _USUN_
		|03 _ZAPISZ_
		|04 _ANULUJ_
		|05 _ZMIEN_
		|06 _ZMIENTYP_
		|07 _ZAAWANSO_
		
	% typy kolumn
	@ColumnTypes
		|00 _TEKST_
		|01 _LICZBA_
		|02 _PROCENT_
		|03 _DATA_
		|04 _WALUTA_
		|05 _ZNAK_
		|06 _DOMYSLNY_
		
	@FilterType
		|00 _MALELITERY_
		|01 _DUZELITERY_
		|02 _NAZWAWLASNA_
		|03 _ROWNY_
		|04 _ROZNY_
		|05 _FORMAT_
		
	% wyskakujące okienka z wiadomościami
	@Messages
		|00 _DOPUSZCZALNEZNAKI_
		|01 _PODAJNAZWEKOLUMNY_
		|02 _KOLUMNAJUZUTWORZONA:{0}_
		|03 _KOLUMNA{0}ISTNIEJENADPISAC_
	
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
