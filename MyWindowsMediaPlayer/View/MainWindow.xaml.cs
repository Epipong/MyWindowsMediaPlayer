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
            MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        /***************** RADIO BUTTON *****************/
        public void OnClickAudioRadio(object sender, RoutedEventArgs e)
        {
            CurrentStateIndex = (int)MediaState.Audio;
            FilesListBox.ItemsSource = NModel.GetMusics();
            MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        public void OnClickImageRadio(object sender, RoutedEventArgs e)
        {
            CurrentStateIndex = (int)MediaState.Image;
            FilesListBox.ItemsSource = NModel.GetImages();
            MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        public void OnClickVideoRadio(object sender, RoutedEventArgs e)
        {
            CurrentStateIndex = (int)MediaState.Video;
            FilesListBox.ItemsSource = NModel.GetVideos();
            MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }
        /***************** END *****************/

        public void OnSelectItem(object sender, RoutedEventArgs e)
        {
            if (FilesListBox.SelectedItem == null)
                return;
            CurrentFilePath = ((HandleFile.FileData)FilesListBox.SelectedItem).path;
            MediaView.Source = new Uri(CurrentFilePath, UriKind.Relative);
            MediaView.Play();
        }

        public void OnSelectPath(object sender, RoutedEventArgs e)
        {
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
   }
}
