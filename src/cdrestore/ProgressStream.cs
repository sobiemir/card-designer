using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace CDRestore
{
	///
	/// Strumień postepu.
	/// Wylicza postęp tylko przy odczytywaniu danych - otwiera go tylko do odczytu.
	/// Pozwala na przypisanie strumienia do określonego wątku, dzięki czemu postęp będzie raportowany
	/// bezpośrednio do wątku. W przeciwnym razie raport zostanie przekazany do zdarzenia OnProgressChanged.
	///
	class ProgressStream : Stream
	{
		/// Zdarzenie wywoływane przy zmianie postępu.
		public event ProgressChangedEventHandler OnProgressChanged = null;
		
		/// Oryginalny strumień.
		private Stream _stream;

		/// Postęp od którego funkcja ma zaczynać (domyślnie 0)
		private int _start = 0;

		/// Postęp na którym funkcja ma kończyć (domyślnie 100)
		private int _stop;

		/// Ilość przeczytanych bajtów ze strumienia.
		private long _bytes = 0;

		/// Ostatni znany postęp (zapobiega kilkukrotnemu wywołaniu zdarzenia dla tego samego postępu).
		private int _last;

		/// Wątek na którym strumień będzie pracował.
		private BackgroundWorker _worker;

		/// 
		/// Konstruktor klasy ProgressStream.
		/// Tworzy strumień postępu z wcześniej utworzonego strumienia.
		/// ------------------------------------------------------------------------------------------------------------
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

		/// 
		/// Odczyt strumienia.
		/// ------------------------------------------------------------------------------------------------------------
		public override bool CanRead
		{
			get { return this._stream.CanRead; }
		}

		/// 
		/// Przewijanie strumienia (nie jest możliwy).
		/// ------------------------------------------------------------------------------------------------------------
		public override bool CanSeek
		{
			get { return false; }
		}

		/// 
		/// Zapis do strumienia (nie jest możliwy).
		/// ------------------------------------------------------------------------------------------------------------
		public override bool CanWrite
		{
			get { return this._stream.CanWrite; }
		}

		/// 
		/// Czyszczenie strumienia.
		/// ------------------------------------------------------------------------------------------------------------
		public override void Flush()
		{
			this._stream.Flush();
		}

		/// 
		/// Długość strumienia.
		/// ------------------------------------------------------------------------------------------------------------
		public override long Length
		{
			get { return this._stream.Length; }
		}

		/// 
		/// Zmiana pozycji (nie jest możliwa).
		/// ------------------------------------------------------------------------------------------------------------
		public override long Position
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		/// 
		/// Odczyt strumienia.
		/// ------------------------------------------------------------------------------------------------------------
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

		/// 
		/// Przewijanie strumienia (nie jest możliwe).
		/// ------------------------------------------------------------------------------------------------------------
		public override long Seek( long offset, SeekOrigin origin )
		{
			throw new NotImplementedException();
		}

		/// 
		/// Zmiana długości strumienia (nie jest możliwa).
		/// ------------------------------------------------------------------------------------------------------------
		public override void SetLength( long value )
		{
			throw new NotImplementedException();
		}

		/// 
		/// Zapis do strumienia (nie jest możliwy).
		/// ------------------------------------------------------------------------------------------------------------
		public override void Write( byte[] buffer, int offset, int count )
		{
			throw new NotImplementedException();
		}
	}
}
