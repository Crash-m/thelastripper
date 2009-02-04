// LibLastRip - A Last.FM ripping library for TheLastRipper
// Copyright (C) 2007  Jop... (Jonas F. Jensen).
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Net;
using System.IO;
using System.Text;

namespace LibLastRip
{
	/*
	This part of the LastManager class contains all MetaInfo related logic.
	*/
	public partial class LastManager
	{
		protected MetaInfo currentSong = MetaInfo.GetEmptyMetaInfo();
	
		///<summary>
		///Gets the meta info about the current song
		///</summary>
		public MetaInfo CurrentSong
		{
			get
			{
				return this.currentSong;
			}
		}
		/// <summary>
		/// Occurs when a new song is detected.
		/// </summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler OnNewSong;
		
		/// <summary>
		/// Occurs when ripping make progress.
		/// </summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler OnProgress;

		/// <summary>
		/// Occurs when log messages are written.
		/// </summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler OnLog;

		/// <summary>
		/// Occurs when scanning event occurs.
		/// </summary>
		/// <remarks>This event may be called on a seperate thread, make sure to invoke any Windows.Forms or GTK# controls modified in EventHandlers</remarks>
		public event System.EventHandler OnScanning;
	}
}
