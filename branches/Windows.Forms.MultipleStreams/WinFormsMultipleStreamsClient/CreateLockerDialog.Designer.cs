/*
 * Created by SharpDevelop.
 * User: q
 * Date: 16-02-2009
 * Time: 19:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WinFormsMultipleStreamsClient
{
	partial class CreateLockerDialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.Okbutton = new System.Windows.Forms.Button();
			this.AbortButton = new System.Windows.Forms.Button();
			this.FirstNameTextBox = new System.Windows.Forms.TextBox();
			this.LastNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.NameGroupBox = new System.Windows.Forms.GroupBox();
			this.LoginInfogroupBox = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.Password2TextBox = new System.Windows.Forms.MaskedTextBox();
			this.PasswordTextBox = new System.Windows.Forms.MaskedTextBox();
			this.EmailTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.NameGroupBox.SuspendLayout();
			this.LoginInfogroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// Okbutton
			// 
			this.Okbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Okbutton.Location = new System.Drawing.Point(93, 256);
			this.Okbutton.Name = "Okbutton";
			this.Okbutton.Size = new System.Drawing.Size(168, 23);
			this.Okbutton.TabIndex = 1;
			this.Okbutton.Text = "Create new MP3tunes account";
			this.Okbutton.UseVisualStyleBackColor = true;
			this.Okbutton.Click += new System.EventHandler(this.OkbuttonClick);
			// 
			// AbortButton
			// 
			this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.AbortButton.Location = new System.Drawing.Point(267, 256);
			this.AbortButton.Name = "AbortButton";
			this.AbortButton.Size = new System.Drawing.Size(75, 23);
			this.AbortButton.TabIndex = 2;
			this.AbortButton.Text = "Cancel";
			this.AbortButton.UseVisualStyleBackColor = true;
			this.AbortButton.Click += new System.EventHandler(this.AbortButtonClick);
			// 
			// FirstNameTextBox
			// 
			this.FirstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.FirstNameTextBox.Location = new System.Drawing.Point(112, 19);
			this.FirstNameTextBox.Name = "FirstNameTextBox";
			this.FirstNameTextBox.Size = new System.Drawing.Size(212, 20);
			this.FirstNameTextBox.TabIndex = 3;
			// 
			// LastNameTextBox
			// 
			this.LastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.LastNameTextBox.Location = new System.Drawing.Point(112, 45);
			this.LastNameTextBox.Name = "LastNameTextBox";
			this.LastNameTextBox.Size = new System.Drawing.Size(212, 20);
			this.LastNameTextBox.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "First name:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Last name:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(330, 51);
			this.label3.TabIndex = 9;
			this.label3.Text = "This dialog will help you create an MP3tunes account to where you can upload all " +
			"you music. See MP3tunes.com for information on what else you can use this accoun" +
			"t for...";
			// 
			// NameGroupBox
			// 
			this.NameGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.NameGroupBox.Controls.Add(this.LastNameTextBox);
			this.NameGroupBox.Controls.Add(this.FirstNameTextBox);
			this.NameGroupBox.Controls.Add(this.label2);
			this.NameGroupBox.Controls.Add(this.label1);
			this.NameGroupBox.Location = new System.Drawing.Point(12, 63);
			this.NameGroupBox.Name = "NameGroupBox";
			this.NameGroupBox.Size = new System.Drawing.Size(330, 77);
			this.NameGroupBox.TabIndex = 10;
			this.NameGroupBox.TabStop = false;
			this.NameGroupBox.Text = "Name";
			// 
			// LoginInfogroupBox
			// 
			this.LoginInfogroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.LoginInfogroupBox.Controls.Add(this.label6);
			this.LoginInfogroupBox.Controls.Add(this.Password2TextBox);
			this.LoginInfogroupBox.Controls.Add(this.PasswordTextBox);
			this.LoginInfogroupBox.Controls.Add(this.EmailTextBox);
			this.LoginInfogroupBox.Controls.Add(this.label5);
			this.LoginInfogroupBox.Controls.Add(this.label4);
			this.LoginInfogroupBox.Location = new System.Drawing.Point(12, 146);
			this.LoginInfogroupBox.Name = "LoginInfogroupBox";
			this.LoginInfogroupBox.Size = new System.Drawing.Size(330, 104);
			this.LoginInfogroupBox.TabIndex = 11;
			this.LoginInfogroupBox.TabStop = false;
			this.LoginInfogroupBox.Text = "Login information";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 23);
			this.label6.TabIndex = 5;
			this.label6.Text = "Password again:";
			// 
			// Password2TextBox
			// 
			this.Password2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.Password2TextBox.Location = new System.Drawing.Point(112, 71);
			this.Password2TextBox.Name = "Password2TextBox";
			this.Password2TextBox.Size = new System.Drawing.Size(212, 20);
			this.Password2TextBox.TabIndex = 4;
			this.Password2TextBox.UseSystemPasswordChar = true;
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.PasswordTextBox.Location = new System.Drawing.Point(112, 45);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.Size = new System.Drawing.Size(212, 20);
			this.PasswordTextBox.TabIndex = 3;
			this.PasswordTextBox.UseSystemPasswordChar = true;
			// 
			// EmailTextBox
			// 
			this.EmailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.EmailTextBox.Location = new System.Drawing.Point(112, 19);
			this.EmailTextBox.Name = "EmailTextBox";
			this.EmailTextBox.Size = new System.Drawing.Size(212, 20);
			this.EmailTextBox.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "Password:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "E-mail:";
			// 
			// CreateLockerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(354, 286);
			this.Controls.Add(this.LoginInfogroupBox);
			this.Controls.Add(this.NameGroupBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.AbortButton);
			this.Controls.Add(this.Okbutton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(360, 311);
			this.Name = "CreateLockerDialog";
			this.Text = "Create new MP3tunes account";
			this.NameGroupBox.ResumeLayout(false);
			this.NameGroupBox.PerformLayout();
			this.LoginInfogroupBox.ResumeLayout(false);
			this.LoginInfogroupBox.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.GroupBox NameGroupBox;
		private System.Windows.Forms.GroupBox LoginInfogroupBox;
		private System.Windows.Forms.TextBox FirstNameTextBox;
		private System.Windows.Forms.TextBox LastNameTextBox;
		private System.Windows.Forms.MaskedTextBox Password2TextBox;
		private System.Windows.Forms.MaskedTextBox PasswordTextBox;
		private System.Windows.Forms.TextBox EmailTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button AbortButton;
		private System.Windows.Forms.Button Okbutton;
	}
}
