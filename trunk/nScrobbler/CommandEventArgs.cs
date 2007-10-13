using System;
using System.Xml;
using System.Collections.Generic;

namespace nScrobbler
{
	//TODO: Document this class and all members
	public class CommandEventArgs : System.EventArgs
	{
		protected System.Boolean _Success = false;
		protected CommandType _Type;
		protected System.Exception _Exception = null;

		public virtual System.Boolean Success
		{
			get
			{
				return this._Success;
			}
		}

		public virtual CommandType Type
		{
			get
			{
				return this._Type;
			}
		}

		/// <summary>
		/// Returns the exception that caused it to fail, or null if no exception occured.
		/// </summary>
		public virtual System.Exception Exception
		{
			get
			{
				return this._Exception;
			}
		}

		public CommandEventArgs(System.Boolean Success, CommandType Type)
		{
			this._Success = Success;
			this._Type = Type;
		}

		public CommandEventArgs(System.Boolean Success, CommandType Type, System.Exception Exception)
		{
			this._Success = Success;
			this._Type = Type;
			this._Exception = Exception;
		}
	}
}
