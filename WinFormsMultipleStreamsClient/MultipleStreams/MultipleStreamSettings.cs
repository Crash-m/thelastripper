using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WinFormsMultipleStreamsClient
{
    public class MultipleStreamSettings
    {
        #region Properties
        public List<User> Users { get; set; }

        //Storage
        private string musicDirectory;

        public string MusicDirectory
        {
            get { return musicDirectory; }
            set { musicDirectory = value; }
        }

        private string saveMode;

        public string SaveMode
        {
            get { return saveMode; }
            set { saveMode = value; }
        }

        private string filenamePattern;

        public string FilenamePattern
        {
            get { return filenamePattern; }
            set { filenamePattern = value; }
        }

        private string iD3Comment;

        public string ID3Comment
        {
            get { return iD3Comment; }
            set { iD3Comment = value; }
        }

        //Playlist Settings
        private bool topTracks;

        public bool TopTracks
        {
            get { return topTracks; }
            set { topTracks = value; }
        }

        private bool topTracksMixed;

        public bool TopTracksMixed
        {
            get { return topTracksMixed; }
            set { topTracksMixed = value; }
        }

        private bool recentlyLovedLeft;

        public bool RecentlyLovedLeft
        {
            get { return recentlyLovedLeft; }
            set { recentlyLovedLeft = value; }
        }

        private bool recentlyLovedRight;

        public bool RecentlyLovedRight
        {
            get { return recentlyLovedRight; }
            set { recentlyLovedRight = value; }
        }
        private bool wecklyTrackChart;

        public bool WecklyTrackChart
        {
            get { return wecklyTrackChart; }
            set { wecklyTrackChart = value; }
        }

        private bool wecklyTrackChartMixed;

        public bool WecklyTrackChartMixed
        {
            get { return wecklyTrackChartMixed; }
            set { wecklyTrackChartMixed = value; }
        }
        private bool allListsMixed;

        public bool AllListsMixed
        {
            get { return allListsMixed; }
            set { allListsMixed = value; }
        }

        //Playlist Settings Format
        private bool m3U;

        public bool M3U
        {
            get { return m3U; }
            set { m3U = value; }
        }

        private bool pLS;

        public bool PLS
        {
            get { return pLS; }
            set { pLS = value; }
        }

        private bool sMIL;

        public bool SMIL
        {
            get { return sMIL; }
            set { sMIL = value; }
        }

        //Advanced
        private string excludeFile;

        public string ExcludeFile
        {
            get { return excludeFile; }
            set { excludeFile = value; }
        }

        private string quaratineDirectory;

        public string QuaratineDirectory
        {
            get { return quaratineDirectory; }
            set { quaratineDirectory = value; }
        }

        private bool skipExistingMusic1;

        public bool SkipExistingMusic1
        {
            get { return skipExistingMusic1; }
            set { skipExistingMusic1 = value; }
        }

        private bool skipExistingMusic2;

        public bool SkipExistingMusic2
        {
            get { return skipExistingMusic2; }
            set { skipExistingMusic2 = value; }
        }

        private bool renamePossibleDamagedFiles;

        public bool RenamePossibleDamagedFiles
        {
            get { return renamePossibleDamagedFiles; }
            set { renamePossibleDamagedFiles = value; }
        }

        private int health;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        //Commands
        private string afterRipCommand;

        public string AfterRipCommand
        {
            get { return afterRipCommand; }
            set { afterRipCommand = value; }
        }

        private string newSongCommand;

        public string NewSongCommand
        {
            get { return newSongCommand; }
            set { newSongCommand = value; }
        }

        //MP3tunes
        private bool uploadToMP3Tunes;

        public bool UploadToMP3Tunes
        {
            get { return uploadToMP3Tunes; }
            set { uploadToMP3Tunes = value; }
        }

        private string eMail;

        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }

        private string mp3TunesPassword;

        public string Mp3TunesPassword
        {
            get { return mp3TunesPassword; }
            set { mp3TunesPassword = value; }
        }

        //Network
        private bool proxyEnabled;

        public bool ProxyEnabled
        {
            get { return proxyEnabled; }
            set { proxyEnabled = value; }
        }

        private string proxyHost;

        public string ProxyHost
        {
            get { return proxyHost; }
            set { proxyHost = value; }
        }

        private string proxyUsername;

        public string ProxyUsername
        {
            get { return proxyUsername; }
            set { proxyUsername = value; }
        }

        private string proxyPassword;

        public string ProxyPassword
        {
            get { return proxyPassword; }
            set
            {
                proxyPassword = value; 
            }
        }
        #endregion

        private String appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public MultipleStreamSettings()
        {
            Users = new List<User>();
            MusicDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
            SaveMode = "Buffer in memory and after save to disc";
            FilenamePattern = @"%a\%r\[%N - ]%t";
            ID3Comment = "Recorded from %s";
            ExcludeFile= string.Empty;
            QuaratineDirectory = string.Empty;
            Health = 0;
            AfterRipCommand = string.Empty;
            NewSongCommand = string.Empty;
            EMail = string.Empty;
            Mp3TunesPassword= string.Empty;
            M3U = true;
            PLS = true;
            SMIL = true;
            
        }

        /// <summary>
        /// Saves this class to the ApplicationData folder
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            TextWriter textWriter = new StreamWriter(appData + "\\multipleStreams.xml");
            serializer.Serialize(textWriter, this);
            textWriter.Close();

        }

        /// <summary>
        /// Loads this class from the corresponding file in the ApplicationData folder
        /// </summary>
        /// <returns></returns>
        public MultipleStreamSettings Load()
        {
            TextReader textReader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                System.String AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                textReader = new StreamReader(appData + "\\multipleStreams.xml");
                MultipleStreamSettings returnObject = (MultipleStreamSettings)serializer.Deserialize(textReader);
                textReader.Close();
                return returnObject;
            }
            catch
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
                return null;
            }

        }
    }
}
