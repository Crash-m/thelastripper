using System;
using System.Xml;
using System.Collections.Generic;

namespace libLastRip
{
	//TODO: Document this class and all members
	public class ErrorEventArgs
	{
		protected System.Exception _Exception = null;
		protected System.String _Message = "An error has ocured";
		protected System.Boolean _Fatal = false;

		public virtual System.Exception Exception
		{
			get
			{
				return this._Exception;
			}
		}

		public virtual System.String Message
		{
			get
			{
				return this._Message;
			}
		}

		public virtual System.Boolean Fatal
		{
			get
			{
				return this._Fatal;
			}
		}

		public ErrorEventArgs(System.String Message)
		{
			this._Message = Message;
		}
		public ErrorEventArgs(System.String Message, System.Exception Exception)
		{
			this._Message = Message;
			this._Exception = Exception;
		}
		
		public ErrorEventArgs(System.String Message, System.Boolean Fatal)
		{
			this._Message = Message;
			this._Fatal = Fatal;
		}
		
		public ErrorEventArgs(System.String Message, System.Exception Exception, System.Boolean Fatal)
		{
			this._Message = Message;
			this._Exception = Exception;
			this._Fatal = Fatal;
		}
	}
}
