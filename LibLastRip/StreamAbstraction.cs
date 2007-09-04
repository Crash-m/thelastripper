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

namespace LibLastRip
{
	///<summary>
	///An implementation of IFileAbtraction to enable TagLib usage of FileStreams
	///</summary>
	internal class StreamAbstraction : TagLib.File.IFileAbstraction
	{
		///<summary>
		///Initialize StreamAbstraction from FileStream
		///</summary>
		public StreamAbstraction(System.IO.FileStream File)
		{
			this.FS = File;
		}
		
		///<summery>FileStream parsed at construction</summary>
		protected System.IO.FileStream FS;

		///<summary>Name of the FileStream</summary>
		public string Name 
		{
			get
			{
				return this.FS.Name;
			}
		}

		///<summary>A readable stream of data</summary>
		public System.IO.Stream ReadStream 
		{
			get
			{
				return this.FS;
			}
		}

		///<summary>A writeable stream of data</summary>
		public System.IO.Stream WriteStream 
		{
			get
			{
				return this.FS;
			}
		}
		
		///<summary>Closes stream created by this class</summary>
		///<remarks>
		///Note: this method doesn't do anything, since all we do is to parse a referance the stream,
		///which must be closed manually later! 
		///</remarks>
		public void CloseStream (System.IO.Stream Stream)
		{
			//Do nothing... (See remarks!)
		}
	}
}
