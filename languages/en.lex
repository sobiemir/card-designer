% ------------------------------------------------------
% Język   : Angielski
% Program : CardDesigner
% Wersja  : 0.8.x
% Tłumacz : Kamil Biały
% ------------------------------------------------------

% nazwy formularzy
@FormNames
	|0 Data filtering

% formularz filtrowania danych
#FilterForm

	% nagłówki tabeli
	@Headers
		|0 Column
		|1 Filter type
		|2 Modifier
		|3 Result
		|4 E
		
	% nazwy kolumn dla listy rozwijanej
	@GroupComboBox
		|0 Columns
		|1 Old columns
		|2 New columns
		
	% nazwy filtrów
	@ComboBox
		|0 Format
		|1 Upper case
		|2 Lower case
		|3 Title case
		|4 Not equal [<>]
		|5 Equal [==]

	% przyciski na formularzu
	@Buttons
		|0 Add
		|1 Remove
		|2 Restore
		|3 Apply
