using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace Raudio
{
    public partial class musicFile : Grid
    {
        private string _titleArtist = "";
        private string _path;
        private Duration _duration;
        private bool choosen = false;
        public AudioFile File;
        public bool IsChoosed => choosen;
        public musicFile(AudioFile af)
        {
            InitializeComponent();
            File = af;
            if (af.Title != null)
                _titleArtist = af.Title;
            else _titleArtist = af.FileName;
            
            _path = af.Path.OriginalString;
            _duration = new Duration(GetVideoDuration(af.Path.LocalPath));

            this.titleArtist.Content = _titleArtist; 
            this.duration.Content = $"{MainWindow.NormalizeStr(_duration.TimeSpan.Minutes)}:{MainWindow.NormalizeStr(_duration.TimeSpan.Seconds)}";
            //this.duration.Content = SoundInfo.GetSoundLength(_path);
            this.Path.Content = _path;
        }

        public void UnChoose()
        {
            choosen = false;
            track.Background = new SolidColorBrush {Color = new Color {A = 255, B = 23, R = 23, G = 23}};
        }

        public void Choose()
        {
            choosen = true;
            track.Background = new SolidColorBrush {Color = new Color {A = 255, B = 32, R = 32, G = 32}};
        }
        
        private static TimeSpan GetVideoDuration(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                IShellProperty prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
            }
        }

        private void mouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            track.Background = !choosen ? new SolidColorBrush {Color = new Color {A = 255, B = 27, R = 27, G = 27}} : new SolidColorBrush {Color = new Color {A = 255, B = 37, R = 37, G = 37}};
        }

        private void mouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            track.Background = !choosen ? new SolidColorBrush {Color = new Color {A = 255, B = 23, R = 23, G = 23}} : new SolidColorBrush {Color = new Color {A = 255, B = 32, R = 32, G = 32}};
        }

        private void cliked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(!choosen)
                MainWindow.mw.Choose(this);
            else MainWindow.mw.PlayPause(this, e);
        }

        private void longLabelEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label lab = (Label) sender;
            lab.HorizontalContentAlignment = HorizontalAlignment.Right;
            lab.FlowDirection = FlowDirection.RightToLeft;
        }

        private void longLabelLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label lab = (Label) sender;
            lab.HorizontalContentAlignment = HorizontalAlignment.Left;
            lab.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}