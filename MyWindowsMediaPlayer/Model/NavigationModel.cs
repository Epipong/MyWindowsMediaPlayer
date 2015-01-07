using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using LibHandleFile;

namespace MyWindowsMediaPlayer.Model
{
    class NavigationModel : HandleFile
    {
        /*
        ** Files loader for ViewModel
        */
        public bool LoadFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string[] files = Directory.GetFiles(fbd.SelectedPath);

            base.HandleLoadFiles(files);
            return true;
        }
    }
}
