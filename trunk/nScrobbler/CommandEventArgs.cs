using System;
using System.Xml;
using System.Collections.Generic;

namespace nScrobbler
{
	/// <summary>
	/// Event arguments used whenever a command request is returned.
	/// </summary>
	public class CommandEventArgs : System.EventArgs
	{
		/// <summary>
		/// Defines if the command execution/request was succesfull
		/// </summary>
		protected System.Boolean _Success = false;
		
		/// <summary>
		/// Defines the type of the command
		/// </summary>
		protected CommandType _Type;
		
		/// <summary>
		/// The exception that may have occured during request/execution of command. Default to null if no exception available.
		/// </summary>
		protected System.Exception _Exception = null;

		/// <summary>
		/// Gets whether or not the command request was successfull
		/// </summary>
		public virtual System.Boolean Success
		{
			get
			{
				return this._Success;
			}
		}

		/// <summary>
		/// Gets the command type
		/// </summary>
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

		/// <summary>
		/// Create a new instance of CommandEventArgs
		/// </summary>
		/// <param name="Success">Indication of whether or not the request was succesfull</param>
		/// <param name="Type">The type if command</param>
		public CommandEventArgs(System.Boolean Success, CommandType Type)
		{
			this._Success = Success;
			this._Type = Type;
		}

		/// <summary>
		/// Create a new instance of CommandEventArgs
		/// </summary>
		/// <param name="Success">Indication of whether or not the request was succesfull</param>
		/// <param name="Type">The type if command</param>
		/// <param name="Exception">Exception that occured during command request/execution.</param>
		public CommandEventArgs(System.Boolean Success, CommandType Type, System.Exception Exception)
		{
			this._Success = Success;
			this._Type = Type;
			this._Exception = Exception;
		}
	}
}
