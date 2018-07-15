using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CDesigner
{
	///
	/// Rozszerzenie kontrolki ContextMenuStrip.
	/// Pozwala na ustawienie własnego marginesu wewnętrznego (domyślnie jest blokowany).
	///
	public class CustomContextMenuStrip : ContextMenuStrip
	{
		/// Wcięcie (margines wewnętrzny).
		private Padding _padding = new Padding( 0 );

		/// 
		/// Margines wewnętrzny (POBIERZ/ZMIEŃ).
		/// ------------------------------------------------------------------------------------------------------------
		public new Padding Padding
		{
			get { return this._padding; }
			set
			{
				this._padding = value;
				base.Padding = value;
			}
		}

		/// 
		/// Konstruktor klasy CustomContextMenuStrip.
		/// ------------------------------------------------------------------------------------------------------------
		public CustomContextMenuStrip() : base()
		{}

		/// 
		/// Zdarzenie wywoływane przy zmianie marginesu wewnętrznego.
		/// ------------------------------------------------------------------------------------------------------------
		protected override void OnPaddingChanged( EventArgs ev )
		{
			base.OnPaddingChanged( ev );
			base.Padding = this._padding;
		}
	}
}
