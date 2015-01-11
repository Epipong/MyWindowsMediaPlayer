using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibHandleFile
{
    public class HandleFile
    {
        /*
        ** type return by HandleFile
        */
        public class FileData
        {
            public string name { get; set; }
            public string path { get; set; }
            public long size { get; set; }
            public DateTime lastAccess { get; set; }
            public DateTime create { get; set; }
        }

        /*
        ** List of files from the select folder
        */
        private List<FileData> images = new List<FileData>();
        private List<FileData> videos = new List<FileData>();
        private List<FileData> musics = new List<FileData>();

        /*
        ** All extension for each type of file (music / image / video)
        */
        private string[] imageExtensions = {".gif",
                                            ".bmp",
                                            ".dib",
                                            ".jpg",
                                            ".jpeg",
                                            ".jpe",
                                            ".jfif",
                                            ".tif",
                                            ".tiff",
                                            ".png"};

        private string[] videoExtensions = {".avi",
                                            ".wmv",
                                            ".mkv",
                                            ".mka",
                                            ".mks",
                                            ".mp4"};

        private string[] musicExtensions = {".mp2",
                                            ".mp3",
                                            ".wav",
                                            ".ogg",
                                            ".gsm",
                                            ".dct",
                                            ".flac",
                                            ".au",
                                            ".aiff",
                                            ".vox",
                                            ".wma",
                                            ".aac",
                                            ".atract",
                                            ".ra",
                                            ".msv",
                                            ".dvf",
                                            ".ape",
                                            ".raw"};

        /*
        ** Handle extension
        */
        private bool extensionCmp(string filePath, string[] extensions)
        {
            string fileExtension = System.IO.Path.GetExtension(filePath);

            foreach (string extension in extensions)
            {
                if (fileExtension == extension)
                    return true;
            }
            return false;
        }
        private bool isMusic(string filePath)
        {
            return extensionCmp(filePath, musicExtensions);
        }
        private bool isVideo(string filePath)
        {
            return extensionCmp(filePath, videoExtensions);
        }
        private bool isImage(string filePath)
        {
            return extensionCmp(filePath, imageExtensions);
        }

        /*
        ** Handle files from the select folder
        */
        private FileData GetDataFromPath(string path)
        {
            FileInfo info = new FileInfo(path);
            FileData data = new FileData();

            data.name = System.IO.Path.GetFileNameWithoutExtension(path);
            data.path = path;
            data.size = info.Length;
            data.create = info.CreationTime;
            data.lastAccess = info.LastAccessTime;

            return data;
        }
        private void SortLoadFiles(FileData file)
        {
            if (isVideo(file.path))
                videos.Add(file);
            else if (isMusic(file.path))
                musics.Add(file);
            else if (isImage(file.path))
                images.Add(file);
        }
        private void CleanOldFile()
        {
            images.Clear();
            videos.Clear();
            musics.Clear();
        }
        protected void HandleLoadFiles(string[] files)
        {
            FileData data;

            CleanOldFile();
            foreach (string filePath in files)
            {
                try {
                    data = GetDataFromPath(filePath);
                }
                catch (Exception e){
                    continue;
                }
                SortLoadFiles(data);
            }
        }

        /*
        ** Handle files sort
        */
        private void SortFiles(ref List<FileData> files, string sortType)
        {
            IEnumerable<FileData> query;

            switch (sortType)
            {
                case "create":
                    query = from data in files orderby data.create descending select data;
                    break;
                case "access":
                    query = from data in files orderby data.lastAccess descending select data;
                    break;
                case "size":
                    query = from data in files orderby data.size descending select data;
                    break;
                default:
                    query = from data in files orderby data.name select data;
                    break;
            }
            files = query.ToList();
        }

        /*
        ** Files acces + sort for ViewModel
        */
        public List<FileData> GetImages(string sortType = "name")
        {
            SortFiles(ref images, sortType);
            return images;
        }
        public List<FileData> GetVideos(string sortType = "name")
        {
            SortFiles(ref videos, sortType);
            return videos;
        }
        public List<FileData> GetMusics(string sortType = "name")
        {
            SortFiles(ref musics, sortType);
            return musics;
        }
    }
}
