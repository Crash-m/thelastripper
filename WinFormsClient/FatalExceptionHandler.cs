/*
 * Created by SharpDevelop.
 * User: Jop
 * Date: 9-08-2007
 * Time: 15:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsClient
{
	/// <summary>
	/// Show an exception to the user in a window, and tells where to go for help.
	/// </summary>
	public partial class FatalExceptionHandler : Form
	{
		public FatalExceptionHandler(System.Exception e)
		{
			InitializeComponent();
			
			//TODO: View the exception in a nice manner.
			//TODO: Attach system information.
			//TODO: Get SVN Revision thing working
			System.Diagnostics.FileVersionInfo VersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
			this.ExceptionBox.Text = "Following exception occured while running TheLastRipper:\r\n" + VersionInfo.FileVersion + " \r\n$Revision$\r\n" + e.ToString().Replace("\n","\r\n");
			this.infoLabel.Text = "An unexpected exception has occured which resulted in a crash. You may help improve TheLastRipper by submitting a bug report to the developer team. You must include following information that may help developers solve your problem:\n - What were you doing at the time of the crash?\n - Has this happened before?\n - Howto reproduce this issue?\n - Information about your system\n - And the exception shown in the textbox below.\nPlease help the developer team by checking that your issue haven't been reported, before submitting it; if already reported please confirm it, provide additional information and/or subscribe to it to be notified whenever there's a change.";
		}
		
		
		void ButtonCloseClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
