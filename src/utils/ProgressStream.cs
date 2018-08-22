///
/// $u05 ProgressStream.cs
/// 
/// Klasa strumienia postępu.
/// Pozwala na odczyt danych ze strumienia i raport aktualnego stanu.
/// Po przypisaniu zdarzenia OnProgressChanged klasa raportuje zmiane postępu wczytywania danych.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.6.x.x
/// Ostatnia zmiana: 2016-12-25
/// 
/// [23.06.2015] Pierwsza wersja klasy strumienia.
/// [25.12.2016] Komentarze, regiony.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace CDesigner.Utils
{
	///
	/// <summary>
	/// Strumień postepu wczytywanych danych.
	/// Wylicza postęp tylko przy odczytywaniu danych - otwiera go tylko do odczytu.
	/// Pozwala na przypisanie strumienia do określonego wątku, dzięki czemu postęp będzie raportowany
	/// bezpośrednio do wątku. W przeciwnym razie raport zostanie przekazany do zdarzenia OnProgressChanged.
	/// </summary>
	///
	class ProgressStream : Stream
	{
#region ZMIENNE

		/// <summary>Zdarzenie wywoływane przy zmianie postępu.</summary>
		public event ProgressChangedEventHandler OnProgressChanged;
		
		/// <summary>Oryginalny strumień.</summary>
		private Stream _stream;

		/// <summary>Postęp od którego funkcja ma zaczynać (domyślnie 0).</summary>
		private int _start;

		/// <summary>Postęp na którym funkcja ma kończyć (domyślnie 100).</summary>
		private int _stop;

		/// <summary>Ilość przeczytanych bajtów ze strumienia.</summary>
		private long _bytes;

		/// <summary>Ostatni znany postęp (wywołanie zdarzenia raz dla tego samego postępu).</summary>
		private int _last;

		/// <summary>Wątek na którym strumień będzie pracował.</summary>
		private BackgroundWorker _worker;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

		/// <summary>
		/// Konstruktor klasy strumienia.
		/// Tworzy strumień postępu z wcześniej utworzonego już strumienia pliku.
		/// Można uruchamiać kilka następujących po sobie na jednej kontrolce, manipulując argumentami Start i Stop.
		/// Funkcja może rozpocząć postęp dla sprawdzania pliku od wartości innej niż 0 i zakończyć na innej niż 100.
		/// </summary>
		/// 
		/// <param name="stream">Strumień danych do sprawdzania.</param>
		/// <param name="start">Postęp początkowy (zakres możliwy od 0).</param>
		/// <param name="stop">Postęp końcowy (zakres możliwy do 100).</param>
		/// <param name="worker">Oddzielny wątek dla operacji.</param>
		//* ============================================================================================================
		public ProgressStream( Stream stream, int start = 0, int stop = 100, BackgroundWorker worker = null )
		{
			// pilnuj granic...
			start = start < 0 ? 0 : start;
			start = start > stop ? 0 : start;
			stop  = stop < 0 ? 100 : stop;
			stop  = stop > 100 ? 100 : stop;

			this._bytes  = 0;
			this._stream = stream;
			this._start  = start;
			this._stop   = stop;
			this._last   = start;
			this._worker = worker;

			this.OnProgressChanged = null;
		}

		/// <summary>
		/// Czy strumień może być przeznaczony do odczytu?
		/// Właściwość tylko do odczytu, sprawdza czy strumień otworzony jest w trybie do odczytu.
		/// </summary>
		//* ============================================================================================================
		public override bool CanRead
		{
			get { return this._stream.CanRead; }
		}

		/// <summary>
		/// Czy strumień może być przewijany?
		/// Właściwość tylko do odczytu, sprawdza czy strumień może być przewijany.
		/// Ta klasa nie dopuszcza przewijania strumienia.
		/// </summary>
		//* ============================================================================================================
		public override bool CanSeek
		{
			get { return false; }
		}

		/// <summary>
		/// Czy strumień może być nadpisywany?
		/// Właściwość tylko do odczytu, sprawdza czy strumień może być nadpisywany.
		/// Ta klasa nie dopuscza do nadpisywania i dopisywania danych do strumieniu.
		/// </summary>
		//* ============================================================================================================
		public override bool CanWrite
		{
			get { return this._stream.CanWrite; }
		}

		/// <summary>
		/// Długość strumienia w bajtach.
		/// Właściwość tylko do odczytu, pobiera długość strumienia w bajtach.
		/// </summary>
		//* ============================================================================================================
		public override long Length
		{
			get { return this._stream.Length; }
		}

		/// <summary>
		/// Zmiana pozycji strumienia.
		/// Zarówno odczyt jak i zmiana pozycji wskaźnika w strumieniu jest zabronione.
		/// </summary>
		//* ============================================================================================================
		public override long Position
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Czyszczenie strumienia danych.
		/// Funkcja czyści podany w konstruktorze strumień danych.
		/// </summary>
		//* ============================================================================================================
		public override void Flush()
		{
			this._stream.Flush();
		}

		/// <summary>
		/// Odczyt strumienia w kawałkach.
		/// Funkcja pozwala na odczyt strumienia z raportowaniem aktualnego postępu w odczycie.
		/// Dzięki temu możliwe jest podpięcie paska postępu i raportowania ilości wczytanych danych.
		/// </summary>
		/// 
		/// <param name="buffer">Bufor do odczytu.</param>
		/// <param name="offset">Aktualna pozycja wskaźnika.</param>
		/// <param name="count">Ilość znaków do odczytu.</param>
		/// 
		/// <returns>Ilość odczytanych danych.</returns>
		//* ============================================================================================================
		public override int Read( byte[] buffer, int offset, int count )
		{
			int read = this._stream.Read( buffer, offset, count );
			
			this._bytes += read;

			// raportuj postep
			if( this._worker != null || this.OnProgressChanged != null )
			{
				// oblicz postęp odczytu strumienia
				var fzero   = (double)this._bytes / (double)this._stream.Length * (double)(this._stop - this._start);
				int percent = this._start + Convert.ToInt32( fzero );
			
				// granice...
				percent = percent > this._stop ? this._stop : percent;
				percent = percent < this._start ? this._start : percent;

				// raportowanie postępu
				if( percent > this._last )
					if( this._worker != null )
						this._worker.ReportProgress( percent, (object)0 );
					else if( this.OnProgressChanged != null )
					{
						var ev = new ProgressChangedEventArgs( percent, (object)0 );
						this.OnProgressChanged( this, ev );
					}

				this._last = percent;
			}

			return read;
		}
#endregion

#region FUNKCJE KTÓRE NIE SĄ POTRZEBNE

		/// <summary>
		/// Przewijanie strumienia (nie jest możliwe).
		/// </summary>
		/// <param name="offset">Do którego miejsca przewinąć.</param>
		/// <param name="origin">Od którego miejsca ma się rozpocząć przewijanie.</param>
		/// 
		/// <returns>Aktualna pozycja wskaźnika.</returns>
		//* ============================================================================================================
		public override long Seek( long offset, SeekOrigin origin )
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Zmiana długości strumienia (nie jest możliwa).
		/// </summary>
		/// 
		/// <param name="value">Nowa długość strumienia.</param>
		//* ============================================================================================================
		public override void SetLength( long value )
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Zapis do strumienia (nie jest możliwy).
		/// </summary>
		/// 
		/// <param name="buffer">Dane do zapisu.</param>
		/// <param name="offset">Indeks początkowy od którego ma się rozpocząć zapis.</param>
		/// <param name="count">Ilość bajtów w buffer.</param>
		//* ============================================================================================================
		public override void Write( byte[] buffer, int offset, int count )
		{
			throw new NotImplementedException();
		}

#endregion
	}
}
