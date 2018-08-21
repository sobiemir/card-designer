using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

///
/// $c05 ProgressStream.cs
/// 
/// Klasa strumienia postępu.
/// Pozwala na odczyt danych ze strumienia i raport aktualnego stanu.
/// Po przypisaniu zdarzenia OnProgressChanged klasa raportuje zmiane postępu wczytywania danych.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2015-06-23
///

namespace CDesigner
{
	///
	/// <summary>
	/// Strumień postepu.
	/// Wylicza postęp tylko przy odczytywaniu danych - otwiera go tylko do odczytu.
	/// Pozwala na przypisanie strumienia do określonego wątku, dzięki czemu postęp będzie raportowany
	/// bezpośrednio do wątku. W przeciwnym razie raport zostanie przekazany do zdarzenia OnProgressChanged.
	/// </summary>
	///
	class ProgressStream : Stream
	{
		// ===== PRIVATE VARIABLES =====

		/// <summary>Zdarzenie wywoływane przy zmianie postępu.</summary>
		public event ProgressChangedEventHandler OnProgressChanged = null;
		
		/// <summary>Oryginalny strumień.</summary>
		private Stream _stream;

		/// <summary>Postęp od którego funkcja ma zaczynać (domyślnie 0).</summary>
		private int _start = 0;

		/// <summary>Postęp na którym funkcja ma kończyć (domyślnie 100).</summary>
		private int _stop;

		/// <summary>Ilość przeczytanych bajtów ze strumienia.</summary>
		private long _bytes = 0;

		/// <summary>Ostatni znany postęp (wywołanie zdarzenia raz dla tego samego postępu).</summary>
		private int _last;

		/// <summary>Wątek na którym strumień będzie pracował.</summary>
		private BackgroundWorker _worker;

		// ===== CONSTRUCTORS / DESTRUCTORS =====================================================

		/**
		 * <summary>
		 * Konstruktor klasy ProgressStream.
		 * Tworzy strumień postępu z wcześniej utworzonego strumienia.
		 * Można uruchamiać kilka na jednej kontrolce ProgressBar manipulując argumentami start i stop.
		 * </summary>
		 * 
		 * <param name="stream">Strumień danych.</param>
		 * <param name="start">Postęp początkowy (zakres od - od 0).</param>
		 * <param name="stop">Postęp końcowy (zakres do - do 100).</param>
		 * <param name="worker">Oddzielny wątek dla operacji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public ProgressStream( Stream stream, int start = 0, int stop = 100, BackgroundWorker worker = null )
		{
			// pilnuj granic...
			start = start < 0 ? 0 : start;
			start = start > stop ? 0 : start;
			stop  = stop < 0 ? 100 : stop;
			stop  = stop > 100 ? 100 : stop;

			this._stream = stream;
			this._start  = start;
			this._stop   = stop;
			this._last   = start;
			this._worker = worker;
		}

		// ===== GETTERS / SETTERS ==============================================================

		/**
		 * <summary>
		 * Odczyt strumienia.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override bool CanRead
		{
			get { return this._stream.CanRead; }
		}

		/**
		 * <summary>
		 * Przewijanie strumienia (nie jest możliwe).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override bool CanSeek
		{
			get { return false; }
		}

		/**
		 * <summary>
		 * Zapis do strumienia (nie jest możliwy).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override bool CanWrite
		{
			get { return this._stream.CanWrite; }
		}

		/**
		 * <summary>
		 * Długość strumienia danych.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override long Length
		{
			get { return this._stream.Length; }
		}

		/**
		 * <summary>
		 * Zmiana pozycji (nie jest możliwa).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override long Position
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		// ===== PUBLIC FUNCTIONS ===============================================================

		/**
		 * <summary>
		 * Czyszczenie strumienia.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override void Flush()
		{
			this._stream.Flush();
		}

		/**
		 * <summary>
		 * Odczyt strumienia.
		 * </summary>
		 * 
		 * <param name="buffer">Bufor do zapisu danych.</param>
		 * <param name="offset">Indeks początkowy zapisu danych.</param>
		 * <param name="count">Ilość pobieranych bajtów.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override int Read( byte[] buffer, int offset, int count )
		{
			int read = this._stream.Read( buffer, offset, count );
			
			this._bytes += read;

			// raportuj postep
			if( this._worker != null || this.OnProgressChanged != null )
			{
				// oblicz postęp odczytu strumienia
				double fzero = (double)this._bytes / (double)this._stream.Length * (double)(this._stop - this._start);
				int percent  = this._start + Convert.ToInt32( fzero );
			
				// granice...
				percent = percent > this._stop ? this._stop : percent;
				percent = percent < this._start ? this._start : percent;

				// raportowanie postępu
				if( percent > this._last )
					if( this._worker != null )
						this._worker.ReportProgress( percent, (object)0 );
					else if( this.OnProgressChanged != null )
					{
						ProgressChangedEventArgs ev = new ProgressChangedEventArgs( percent, (object)0 );
						this.OnProgressChanged( this, ev );
					}

				this._last = percent;
			}

			return read;
		}

		/**
		 * <summary>
		 * Przewijanie strumienia (nie jest możliwe).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override long Seek( long offset, SeekOrigin origin )
		{
			throw new NotImplementedException();
		}

		/**
		 * <summary>
		 * Zmiana długości strumienia (nie jest możliwa).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override void SetLength( long value )
		{
			throw new NotImplementedException();
		}

		/**
		 * <summary>
		 * Zapis do strumienia (nie jest możliwy).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public override void Write( byte[] buffer, int offset, int count )
		{
			throw new NotImplementedException();
		}
	}
}
