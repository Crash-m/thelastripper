using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormsMultipleStreamsClient
{
    public class User
    {

        #region Properties
     
        //User specific settings
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string StationText { get; set; }
        public string RadioGenre { get; set; }
        public string IpAdress { get; set; }

        //form that this user created
        [System.Xml.Serialization.XmlIgnore]
        public StreamForm Form { get; set; }
        #endregion

        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            Port = 8000;
            StationText = string.Empty;
            RadioGenre = "Artist";
            IpAdress = "127.0.0.1";
        }
    }
}
