/*
 * Erstellt mit SharpDevelop.
 * Benutzer: alangman
 * Datum: 30.10.2008
 * Zeit: 10:04
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace LibLastRip
{
	/// <summary>
	/// Event arguments for LastManager.Scanning
	/// </summary>
	public class ScanningEventArgs : System.EventArgs {
		protected System.Int32 _Streamprogress;
		
		public static int SCANNING_STARTED = 1;
		public static int SCANNING_STOPPED = 2;
		public static int SCANNING_PROGRESS = 3;

		public ScanningEventArgs(System.Int32 param) {
			this._Streamprogress = param;
		}
		
		public System.Int32 Streamprogress
		{
			get
			{
				return this._Streamprogress;
			}
		}
	}
}
