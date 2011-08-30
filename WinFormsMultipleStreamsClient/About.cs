/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 11-02-2007
 * Time: 14:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsMultipleStreamsClient
{
	/// <summary>
	/// Description of About.
	/// </summary>
	public partial class About : Form
	{
		public About()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
