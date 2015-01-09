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

namespace WpfApplication3
{
    public partial class MainWindow : Window
    {
        void Play_Click(object sender, RoutedEventArgs e)
        {
                myMedia.Play();
        }

        void Pause_Click(object sender, RoutedEventArgs e)
        {
                myMedia.Pause();
        }

        void Stop_Click(object sender, RoutedEventArgs e)
        {
                myMedia.Stop();
        }

        void Photo_Click(object sender, RoutedEventArgs e)
        {
                myPhoto.Play();
        }

/*        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myMedia.Play();
            }
            catch {}
            using (var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\Dangerous.wav"))
            {
                soundPlayer.Play();
            }

        }
    */
    }
}
