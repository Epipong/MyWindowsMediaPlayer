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

        public WindowMedia()
        {
            Initialize();
            InitializeComponent();
            MediaBar.Source = new Uri(PathMedias[CurrentStateIndex], UriKind.Relative);
        }

        private void Initialize()
        {
            CurrentStateIndex = (int)MediaState.Audio;
            PathMedias = new string[(int)MediaState.Limit];

            PathMedias[(int)MediaState.Audio] = "AudioBar.xaml";
            PathMedias[(int)MediaState.Image] = "ImageBar.xaml";
            PathMedias[(int)MediaState.Video] = "VideoBar.xaml";
            PathMedias[(int)MediaState.Unknown] = "";
        }

        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            MediaView.Play();
        }

        private void OnPauseClick(object sender, RoutedEventArgs e)
        {
            MediaView.Pause();
        }
    }
}
