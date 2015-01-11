using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

using LibHandleFile;
using System.ComponentModel;

namespace MyWindowsMediaPlayer.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    enum MediaState
    {
        Audio,
        Image,
        Video,
        Unknown,
        Limit
    }

    public partial class WindowMedia : Window
    {
        private string[] PathMedias;
        private int CurrentStateIndex { get; set; }
        private string CurrentFilePath { get; set; }
        private Model.NavigationModel NModel;
        private Model.PlaylistModel PModel;
        private string typeCurrentList;

        public WindowMedia()
        {
            InitializeComponent();
            Initialize();
        }

        // initialize all components
        private void Initialize()
        {
            CurrentStateIndex = (int)MediaState.Image;
            CurrentFilePath = "";
            PathMedias = new string[(int)MediaState.Limit];
            PathMedias[(int)MediaState.Audio] = "AudioBar.xaml";
            PathMedias[(int)MediaState.Image] = "ImageBar.xaml";
            PathMedias[(int)MediaState.Video] = "VideoBar.xaml";
            PathMedias[(int)MediaState.Unknown] = "";
            NModel = new Model.NavigationModel();
            PModel = new Model.PlaylistModel();
            //MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        /***************** RADIO BUTTON *****************/
        public void OnClickAudioRadio(object sender, RoutedEventArgs e)
        {
            CurrentStateIndex = (int)MediaState.Audio;
            if (typeCurrentList == "folder")
                FilesListBox.ItemsSource = NModel.GetMusics();
            else if (typeCurrentList == "playlistFile")
                FilesListBox.ItemsSource = PModel.GetMusics();
            //MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        public void OnClickImageRadio(object sender, RoutedEventArgs e)
        {
            CurrentStateIndex = (int)MediaState.Image;
            if (typeCurrentList == "folder")
                FilesListBox.ItemsSource = NModel.GetImages();
            else if (typeCurrentList == "playlistFile")
                FilesListBox.ItemsSource = PModel.GetImages();
            //MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        public void OnClickVideoRadio(object sender, RoutedEventArgs e)
        {
            CurrentStateIndex = (int)MediaState.Video;
            if (typeCurrentList == "folder")
                FilesListBox.ItemsSource = NModel.GetVideos();
            else if (typeCurrentList == "playlistFile")
                FilesListBox.ItemsSource = PModel.GetVideos();
            //MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }
        /***************** END *****************/

        public void OnSelectItem(object sender, RoutedEventArgs e)
        {
            if (FilesListBox.SelectedItem == null)
                return;
            CurrentFilePath = ((HandleFile.FileData)FilesListBox.SelectedItem).path;
            MediaView.Source = new Uri(CurrentFilePath, UriKind.Relative);
            Filename.Content = ((HandleFile.FileData)FilesListBox.SelectedItem).path;
            Play.Visibility = Visibility.Collapsed;
            Pause.Visibility = Visibility.Visible;
            MediaView.Play();
        }

        public void OnSelectPath(object sender, RoutedEventArgs e)
        {
            typeCurrentList = "folder";
            NModel.LoadFolder();
        }

        public void OnDoubleClickItem(object sender, RoutedEventArgs e)
        {

        }

        public void OnEnterPressKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {/*
                MediaView.Source = new Uri(SearchURLData.Text, UriKind.Relative);
                MediaView.Play();*/
            }
        }

        public void OnAdd(object sender, RoutedEventArgs e){
            if (typeCurrentList != "folder")
                return;
            if (FilesListBox.SelectedItem != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != "")
                PModel.AddFile(((HandleFile.FileData)FilesListBox.SelectedItem).path);
        }
        public void OnRemove(object sender, RoutedEventArgs e)
        {
            if (typeCurrentList != "playlistFile")
                return;
            if (FilesListBox.SelectedItem != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != "")
                PModel.DeleteFile(((HandleFile.FileData)FilesListBox.SelectedItem).name);
        }
        public void OnSeeAll(object sender, RoutedEventArgs e)
        {
            typeCurrentList = "playlist";
            FilesListBox.ItemsSource = PModel.GetPlaylistes();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            if (typeCurrentList != "playlist")
                return;
            if (FilesListBox.SelectedItem != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != "")
            {
                PModel.LoadPlaylist(((HandleFile.FileData)FilesListBox.SelectedItem).name);
                typeCurrentList = "playlistFile";
            }

        }
        public void OnCreate(object sender, RoutedEventArgs e)
        {
            typeCurrentList = "playlist";
            if (SearchURLData.Text != null && SearchURLData.Text != "")
                PModel.AddPlaylist(SearchURLData.Text);
            FilesListBox.ItemsSource = PModel.GetPlaylistes();
        }
        public void OnDelete(object sender, RoutedEventArgs e)
        {
            if (typeCurrentList != "playlist")
                return;
            if (FilesListBox.SelectedItem != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != null &&
                ((HandleFile.FileData)FilesListBox.SelectedItem).name != "")
            {
                PModel.DeletePlaylist(((HandleFile.FileData)FilesListBox.SelectedItem).name);
                FilesListBox.ItemsSource = PModel.GetPlaylistes();
            }
        }

        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            if (CurrentFilePath == null || CurrentFilePath == "")
                return;
            Play.Visibility = Visibility.Collapsed;
            Pause.Visibility = Visibility.Visible;
            MediaView.Play();
        }

        private void OnPauseClick(object sender, RoutedEventArgs e)
        {
            Pause.Visibility = Visibility.Collapsed;
            Play.Visibility = Visibility.Visible;
            MediaView.Pause();
        }
    }
}
