using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using LibHandleFile;

namespace MyWindowsMediaPlayer.Model
{
    class PlaylistModel : HandleFile
    {
        /*
        ** Playlist configuration: folder + extension
        */
        private string playlistDirectory = "Playlist";
        private string playlistExtension = ".playlist";

        /*
        ** Attribut
        */
        private Dictionary<string, List<string>> playlists = new Dictionary<string, List<string>>();
        private string currentPlaylist;
        private XmlSerializer serializer = new XmlSerializer(typeof(List<string>));

        /*
        ** Constructor / Destructor
        */
        public PlaylistModel()
        {
            string[] playlistPaths;
            List<string> playlistFiles;
            FileStream stream;

            CheckPlaylistDirectory();
            playlistPaths = Directory.GetFiles(playlistDirectory);

            foreach (string playlistPath in playlistPaths)
            {
                if (System.IO.Path.GetExtension(playlistPath) != playlistExtension)
                    continue;
                stream = File.OpenRead(playlistPath);
                playlistFiles = serializer.Deserialize(stream) as List<string>;
                playlists.Add(System.IO.Path.GetFileNameWithoutExtension(playlistPath), playlistFiles);
                stream.Close();
            }
        }
        ~PlaylistModel()
        {
            FileStream stream;
            string playlistFile;

            CheckPlaylistDirectory();

            foreach (KeyValuePair<string, List<string>> elem in playlists)
            {
                playlistFile = GetPlaylistFile(elem.Key);
                stream = File.OpenWrite(playlistFile);
                serializer.Serialize(stream, elem.Value);
                stream.Close();
            }
        }

        /*
        ** Dry + tools
        */
        private void CheckPlaylistDirectory()
        {
            if (Directory.Exists(playlistDirectory) == false)
                Directory.CreateDirectory(playlistDirectory);
        }
        private string GetPlaylistFile(string playlistName)
        {
            string playlistFile;

            playlistFile = playlistDirectory;
            playlistFile += "/";
            playlistFile += playlistName;
            playlistFile += playlistExtension;
            return playlistFile;
        }

        /*
        ** Playlists manipulation for ViewModel
        */
        public void AddPlaylist(string playlistName)
        {
            playlists.Add(playlistName, new List<string>());
        }
        public void DeletePlayliste(string playlistName)
        {
            playlists.Remove(playlistName);
        }
        public string[] GetPlaylistes()
        {
            List<string> playlistNames = new List<string>();

            foreach (KeyValuePair<string, List<string>> elem in playlists)
            {
                playlistNames.Add(elem.Key);
            }
            return playlistNames.ToArray();
        }
        public bool LoadPlaylist(string playlistName)
        {
            currentPlaylist = playlistName;
            base.HandleLoadFiles(playlists[currentPlaylist].ToArray());
            return true;
        }

        /*
        ** Playlist's file manipulation for ViewModel
        */
        public void AddFile(string filePath)
        {
            playlists[currentPlaylist].Add(filePath);
            LoadPlaylist(currentPlaylist);
        }
        public void DeleteFile(string filePath)
        {
            playlists[currentPlaylist].Remove(filePath);
            LoadPlaylist(currentPlaylist);
        }
    }
}
