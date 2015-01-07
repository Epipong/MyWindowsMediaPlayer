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
                                            ".mks"};

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
                data = GetDataFromPath(filePath);
                SortLoadFiles(data);
            }
        }

        /*
        ** Fct for List<fileData>::sort
        */
        private static int CompareName(FileData first, FileData second)
        {
            return first.name.CompareTo(second.name);
        }
        private static int CompareCreate(FileData first, FileData second)
        {
            return first.create.CompareTo(second.create);
        }
        private static int CompareAccess(FileData first, FileData second)
        {
            return first.lastAccess.CompareTo(second.lastAccess);
        }
        private static int CompareSize(FileData first, FileData second)
        {
            return first.size.CompareTo(second.size);
        }

        /*
        ** Handle files sort
        */
        private void SortFiles(List<FileData> files, string sortType)
        {
            switch (sortType)
            {
                case "create":
                    files.Sort(CompareCreate);
                    break;
                case "access":
                    files.Sort(CompareAccess);
                    break;
                case "size":
                    files.Sort(CompareSize);
                    break;
                default:
                    files.Sort(CompareName);
                    break;
            }
        }

        /*
        ** Files acces + sort for ViewModel
        */
        public List<FileData> GetImages(string sortType = "name")
        {
            SortFiles(images, sortType);
            return images;
        }
        public List<FileData> GetVideos(string sortType = "name")
        {
            SortFiles(videos, sortType);
            return videos;
        }
        public List<FileData> GetMusics(string sortType = "name")
        {
            SortFiles(musics, sortType);
            return musics;
        }
    }
}
