
using System;

namespace LibLastRip
{
	[SerializableAttribute]
	public class PlayListGenerator : System.Runtime.Serialization.ISerializable
	{
		protected System.String _musicPath;
		protected System.String _quarantinePath;
		protected System.String _userName;
		
		protected System.String _excludeFile;
		protected System.Boolean _excludeNewMusic = false;
		protected System.Boolean _excludeExistingMusic = false;

		protected System.String _proxyAddress;
		protected System.String _proxyUsername;
		protected System.String _proxyPassword;
		
		protected PlayListGenerator(){}
		
		public PlayListGenerator(System.String musicPath, System.String userName)
		{
			this._musicPath = musicPath;
			this._userName = userName;
		}
		
		public System.Boolean TopTracks = true;
		public System.Boolean RecentLovedTracks = true;
		public System.Boolean WeeklyTrackChart = true;
		
		public System.Boolean TopTracksMixed = true;
		public System.Boolean RecentLovedTracksMixed = true;
		public System.Boolean WeeklyTrackChartMixed = true;
		
		public System.Boolean Mixed = true;
		
		public System.Boolean m3u = true;
		public System.Boolean pls = true;
		public System.Boolean smil = true;
		
		public virtual void Generate()
		{
			PlayList List1;
			PlayList List2;
			PlayList List3;
			
			List1 = this.GeneratePlayList("http://ws.audioscrobbler.com/1.0/user/"+this._userName+"/toptracks.xml",this.TopTracks,this.TopTracksMixed,"TopTracks");
			List2 = this.GeneratePlayList("http://ws.audioscrobbler.com/1.0/user/"+this._userName+"/recentlovedtracks.xml",this.RecentLovedTracks,this.RecentLovedTracksMixed,"RecentLovedTracks");
			List3 = this.GeneratePlayList("http://ws.audioscrobbler.com/1.0/user/"+this._userName+"/weeklytrackchart.xml",this.WeeklyTrackChart,this.WeeklyTrackChartMixed,"WeeklyTrackChart");
			
			List1.AddRange(List2);
			List1.AddRange(List3);
			List1.Randomize();
			this.SavePlayList(List1, "Mixed");
		}
		
		protected virtual PlayList GeneratePlayList(System.String Feed, System.Boolean Clean, System.Boolean Mixed, System.String FileName)
		{
			PlayList list = new PlayList(this._musicPath);
			
			list.DownloadXML(Feed);
			
			if(Clean)
			{
				this.SavePlayList(list, FileName);
			}
			if(Mixed)
			{
				list.Randomize();
				this.SavePlayList(list, FileName + "Mixed");
			}
			
			return list;
		}
		
		protected virtual void SavePlayList(PlayList List, System.String FileName)
		{
			if(this.m3u)
			{
				List.Save(FileName + ".m3u",PlayListType.m3u);
			}
			if(this.pls)
			{
				List.Save(FileName + ".pls",PlayListType.pls);
			}
			if(this.smil)
			{
				List.Save(FileName + ".smil",PlayListType.smil);
			}
		}
		
		protected PlayListGenerator(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context)
		{
			if (Info == null)
			{
				throw new System.ArgumentNullException("Info");
			}
			this.TopTracks = (System.Boolean)Info.GetValue("TopTracks",typeof(System.Boolean));
			this.RecentLovedTracks = (System.Boolean)Info.GetValue("RecentLovedTracks",typeof(System.Boolean));
			this.WeeklyTrackChart = (System.Boolean)Info.GetValue("WeeklyTrackChart",typeof(System.Boolean));
			
			this.TopTracksMixed = (System.Boolean)Info.GetValue("TopTracksMixed",typeof(System.Boolean));
			this.RecentLovedTracksMixed = (System.Boolean)Info.GetValue("RecentLovedTracksMixed",typeof(System.Boolean));
			this.WeeklyTrackChartMixed = (System.Boolean)Info.GetValue("WeeklyTrackChartMixed",typeof(System.Boolean));
			
			this.Mixed = (System.Boolean)Info.GetValue("Mixed",typeof(System.Boolean));
			
			this.m3u = (System.Boolean)Info.GetValue("m3u",typeof(System.Boolean));
			this.pls = (System.Boolean)Info.GetValue("pls",typeof(System.Boolean));
			this.smil = (System.Boolean)Info.GetValue("smil",typeof(System.Boolean));
			
			this._musicPath = (System.String)Info.GetValue("MusicPath",typeof(System.String));
			this._quarantinePath = (System.String)Info.GetValue("QuarantinePath",typeof(System.String));
			this._userName = (System.String)Info.GetValue("UserName",typeof(System.String));
			
			this._excludeFile = (System.String)Info.GetValue("ExcludeFile",typeof(System.String));
			this._excludeNewMusic = (System.Boolean)Info.GetValue("ExcludeNewMusic",typeof(System.Boolean));
			this._excludeExistingMusic = (System.Boolean)Info.GetValue("ExcludeExistingMusic",typeof(System.Boolean));

		    this._proxyAddress = (System.String)Info.GetValue("ProxyAddress",typeof(System.String));
			this._proxyUsername = (System.String)Info.GetValue("ProxyUsername",typeof(System.String));
			this._proxyPassword = (System.String)Info.GetValue("ProxyPassword",typeof(System.String));
		}
		
		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context)
		{
			if (Info == null)
			{
				throw new System.ArgumentNullException("Info");
			}
			Info.AddValue("TopTracks",this.TopTracks);
			Info.AddValue("RecentLovedTracks",this.RecentLovedTracks);
			Info.AddValue("WeeklyTrackChart",this.WeeklyTrackChart);
			
			Info.AddValue("TopTracksMixed",this.TopTracksMixed);
			Info.AddValue("RecentLovedTracksMixed",this.RecentLovedTracksMixed);
			Info.AddValue("WeeklyTrackChartMixed",this.WeeklyTrackChartMixed);
			
			Info.AddValue("Mixed",this.Mixed);
			
			Info.AddValue("m3u",this.m3u);
			Info.AddValue("pls",this.pls);
			Info.AddValue("smil",this.smil);
			
			Info.AddValue("MusicPath",this._musicPath);
			Info.AddValue("QuarantinePath",this._quarantinePath);
			Info.AddValue("UserName",this._userName);
			
			Info.AddValue("ExcludeFile",this._excludeFile);
			Info.AddValue("ExcludeNewMusic",this._excludeNewMusic);
			Info.AddValue("ExcludeExistingMusic",this._excludeExistingMusic);

			Info.AddValue("ProxyAddress", this._proxyAddress);
			Info.AddValue("ProxyUsername", this._proxyUsername);
			Info.AddValue("ProxyPassword", this._proxyPassword);
		}
	}
}
