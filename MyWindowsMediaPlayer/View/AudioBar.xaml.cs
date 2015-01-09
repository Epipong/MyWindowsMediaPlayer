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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWindowsMediaPlayer.View
{
    /// <summary>
    /// Interaction logic for AudioBar.xaml
    /// </summary>
    public partial class AudioBar : UserControl
    {
        public AudioBar()
        {
            InitializeComponent();
        }

        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            Play.Visibility = Visibility.Collapsed;
            Pause.Visibility = Visibility.Visible;
        }

        private void OnPauseClick(object sender, RoutedEventArgs e)
        {
            Pause.Visibility = Visibility.Collapsed;
            Play.Visibility = Visibility.Visible;
        }
    }
}
