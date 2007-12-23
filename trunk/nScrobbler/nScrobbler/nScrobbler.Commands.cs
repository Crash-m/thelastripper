/*
 * Created by SharpDevelop.
 * User: q
 * Date: 21-09-2007
 * Time: 18:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace nScrobbler
{
	public partial class nScrobbler
	{
		
		public virtual void Love()
		{
			//TODO: Initiate love command
		}

		public virtual void Hate()
		{
			//TODO: Initiate hate command
		}
		
		public delegate void CommandReturnEventHandler(System.Object Sender, CommandEventArgs Args);
		public virtual event CommandReturnEventHandler CommandReturn;
	}
}
