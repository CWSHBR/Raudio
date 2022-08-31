using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;
using DataFormats = System.Windows.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;
using Label = System.Windows.Controls.Label;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Raudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mw;
        
        private AudioControl ac = new();

        private FileControl fc = new FileControl(
            "All Supported File Formats|*.aac; *.m4a; *.3gp;*.wma;*.ogg;*.opus;*.mp3;*.aiff;*.aif;*.wav|" +
            "Waveform Audio File (*.wav)|*.wav|" +
            "Audio Interchange File (*.aiff)|*.aiff;*.aif|" +
            "MP3 Audio File (*.mp3)|*.mp3|" +
            "OGG Audio File (*.ogg)|*.ogg; *.opus|" +
            "Windows Media Audio File (*.wma)|*.wma|" +
            "Advanced Audio Coding Audio File (*.aac)|*.aac; *.m4a; *.3gp");
        
        private bool canPlay;
        
        private Label trackName;
        private Label albumName;
        private Slider _slider;
        private Image button;
        private Image venyl;

        private TimeSpan lastPos = new TimeSpan();
        private bool sliderDown;
        private bool lastState;
        private float rot;

        private List<musicFile> _toPlay = new ();
        private AudioFile _nowPlaying;
        private musicFile _last;
        private RepeatMode _repeatMode;
        private int repeatCounter = 0;
        private bool wasPlaying;

        private enum RepeatMode
        {
            Repeat,
            RepeatOnce,
            NoRepeat
        }

        public MainWindow()
        {
            InitializeComponent();
            mw = this;
            // ac.Open(new Uri(audioPath));
            ac.AudioOpened += Opened;
            lastState = ac.IsPlaying;
            fc.FilePickerSuccess += OnFileOpenSuccess;
            SetRepeatState(RepeatMode.NoRepeat);
        }

        public void Choose(musicFile sender)
        {
            if(_repeatMode == RepeatMode.RepeatOnce) repeatCounter = 0;
            ac.Stop();
            canPlay = false;
            sender.Choose();
            if (_last != null) _last.UnChoose();
            _last = sender;
            ac.Open(_last.File.Path);
            _nowPlaying = sender.File;
        }

        private void Update(object? sender, EventArgs e)
        {
            if (lastPos.Seconds != ac.TimePosition.Seconds & !sliderDown)
            {
                _slider.Value = HoursToMinutesToSeconds(ac.TimePosition);
                
                lastPos = ac.TimePosition;
            }

            if (canPlay)
            {
                if (Math.Abs(ac.TimePosition.TotalMilliseconds - ac.Duration.TimeSpan.TotalMilliseconds) < 0.01f &
                    ac.IsPlaying)
                {
                    ac.Stop();
                    _slider.Value = 0;
                    if(_repeatMode == RepeatMode.NoRepeat)
                        nextAudio();
                    else
                    {
                        if(_repeatMode == RepeatMode.Repeat) ac.Play();
                        if (_repeatMode == RepeatMode.RepeatOnce & repeatCounter < 1)
                        {
                            ac.Play();
                            repeatCounter++;
                        }else nextAudio();
                    }
                }
            }

            if (!canPlay & ac.IsPlaying)
            {
                ac.Stop();
            }

            if (ac.IsPlaying)
            {
                rot += 100 * 0.001f;
                if (Math.Abs(rot) >= 360f) rot = 0;
                RotateTransform i = new() {Angle = rot};
                venyl.RenderTransform = i;
            }
            
            
            if (ac.IsPlaying != lastState)
            {
                ChangeButtonIcon(button);
                lastState = ac.IsPlaying;
            }
            
        }

        private void nextAudio()
        {
            var i = findIndex(_nowPlaying, _toPlay);
            if (i == null) return;
            int k = (int) i;
            if(k+1 <= _toPlay.Count-1)
                Choose(_toPlay[k+1]);
        }

        private int? findIndex(AudioFile comparable, List<musicFile> i)
        {
            int iter = 0;
            foreach (var j in i)
            {
                if (j.File == comparable)
                {
                    return iter;
                }

                iter++;
            }

            return null;
        }

        public void PlayPause(object sender, MouseButtonEventArgs e)
        {
            if (!canPlay) return;
            if(ac.IsPlaying)ac.Pause();
            else ac.Play();
        }

        private void ChangeButtonIcon(Image butt)
        {
            butt.Source = newImage(ac.IsPlaying ? "Resources/pause.png" : "Resources/play.png");
        }

        private BitmapImage newImage(string relativePath)
        {
            BitmapImage bi = new();
            bi.BeginInit();
            bi.UriSource = new Uri(relativePath, UriKind.Relative);
            bi.EndInit();
            return bi;
        }

        private void Opened(object? sender, EventArgs e)
        {
            canPlay = true;
            
            _slider.Maximum = HoursToMinutesToSeconds(ac.Duration.TimeSpan);
            AudioLength.Content = $"{NormalizeStr(ac.Duration.TimeSpan.Minutes)}:{NormalizeStr(ac.Duration.TimeSpan.Seconds)}";
            TimeSpend.Content = "00:00";
            
            ac.Play();

            if (_nowPlaying.Artist != null & _nowPlaying.Title != null)
            {
                trackName.Content = _nowPlaying.Artist + " - " + _nowPlaying.Title;
                
                albumName.Content = _nowPlaying.Path.OriginalString;
            
                // if (nowPlaying.Album != null)
                // {
                //     if(nowPlaying.Album.Length != 0) albumName.Content = nowPlaying.Album;
                //     else albumName.Content = nowPlaying.FileName;
                //
                // }
                // else
                // {
                //     albumName.Content = nowPlaying.Path.OriginalString;
                // }
            }
            else
            {
                trackName.Content = _nowPlaying.FileName;
                albumName.Content = _nowPlaying.Path.OriginalString;
            }
        }

        private void Mouse_Enter(object sender, MouseEventArgs e) //hover effect 
        {
            Image image = (Image)sender;

            image.Opacity = 0.85f;
            DropShadowEffect dse = (DropShadowEffect)image.Effect;
            dse.ShadowDepth = 0;
            image.Effect = dse;
        }

        private void Mouse_Leave(object sender, MouseEventArgs e) //hover effect 
        {
            Image image = (Image)sender;

            image.Opacity = 1f;
            DropShadowEffect dse = (DropShadowEffect)image.Effect;
            dse.ShadowDepth = 5;
            image.Effect = dse;
        }

        private void TrackNameLabelLoaded(object sender, RoutedEventArgs e)
        {
            trackName = (Label)sender;
        }

        private void AlbumNameLabelLoaded(object sender, RoutedEventArgs e)
        {
            albumName = (Label)sender;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer timer = new();
 
            timer.Tick += Update;
            timer.Interval = new TimeSpan(10000);
            timer.Start();
        }

        public static string NormalizeStr(int i)
        {
            if (i < 10) return "0" + i;
            return i.ToString();
        }

        private int HoursToMinutesToSeconds(TimeSpan ts) // returning seconds
        {
            return ts.Seconds + ts.Minutes * 60 + ts.Hours * 3600;
        }

        private TimeSpan tTimeSpan(int sec)
        {
            return new TimeSpan(0, 0, sec);    
        }

        private void SliderLoaded(object sender, RoutedEventArgs e)
        {
            _slider = (Slider) sender;
            _slider.PreviewMouseUp += SliderMouseUP;
            _slider.PreviewMouseDown += SliderMouseDown;
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(!canPlay) {ac.Stop();return;}
            
            if(sliderDown)ac.Wind(tTimeSpan((int)_slider.Value));
            TimeSpend.Content = $"{NormalizeStr(tTimeSpan((int)_slider.Value).Minutes)}:{NormalizeStr(tTimeSpan((int)_slider.Value).Seconds)}";
        }

        private void SliderMouseDown(object sender, MouseButtonEventArgs e)
        {
            sliderDown = true;
            wasPlaying = ac.IsPlaying;
            ac.Pause();
        }

        private void SliderMouseUP(object sender, MouseButtonEventArgs e)
        {
            sliderDown = false;
            if(wasPlaying)ac.Play();
        }

        private void ButtonLoaded(object sender, RoutedEventArgs e)
        {
            button = (Image)sender;
        }

        private void DropEnter(object sender, DragEventArgs e) // TODO
        {
            Grid w = (Grid)sender;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                Console.WriteLine(files[0]);
            }

            Color c = new Color();
            c.A = 255;
            c.R = 40;
            c.G = 40;
            c.B = 40;

            w.Background = new SolidColorBrush(c);
        }

        private void DropLeave(object sender, DragEventArgs e) // TODO
        {
            Grid w = (Grid)sender;

            Color c = new Color {A = 255, R = 26, G = 26, B = 26};

            w.Background = new SolidColorBrush(c);
        }
        
        private void moveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void closeButtEnter(object sender, MouseEventArgs e)
        {
            Label close = (Label) sender;

            Color c = new Color { A = 255, R = 40, G = 40, B = 40 };

            close.Background = new SolidColorBrush(c);

        }
        
        private void closeButtLeave(object sender, MouseEventArgs e)
        {
            Label close = (Label)sender;

            Color c = new Color {A = 0, R = 40, G = 40, B = 40};
            
            close.Background = new SolidColorBrush(c);
        }

        private void venylLoaded(object sender, RoutedEventArgs e)
        {
            venyl = (Image) sender;
        }

        private void Droped(object sender, DragEventArgs e) // TODO
        {
            
        }

        private void addMusicUp(object sender, MouseButtonEventArgs e)
        {
            fc.FilePicker();
        }

        private void OnFileOpenSuccess(object? sender, EventArgs e)
        {
            foreach (AudioFile au in fc._audioFiles)
                if(!hasIn(au, _toPlay))_toPlay.Add(new musicFile(au));
            UpdateList();
        }


        private void UpdateList()
        {
            List<musicFile> k = new();
            musicFile mf = null;
            foreach (musicFile i in musicStackPanel.Children)
            {
                if (!i.IsChoosed) k.Add(i);
                else mf = i;
            }

            foreach (var l in k)
                musicStackPanel.Children.Remove(l);

            foreach (var af in _toPlay)
            {
                if (mf == null) musicStackPanel.Children.Add(af);
                else if (mf.File.Path.AbsolutePath != af.File.Path.AbsolutePath)
                    musicStackPanel.Children.Add(af);
                
            }
        }

        bool hasIn(AudioFile af, List<musicFile> list)
        {
            foreach (var i in list)
            {
                if(i.File.Path.AbsolutePath == af.Path.AbsolutePath) return true;
            }

            return false;
        }

        private void CloseClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PrevNextClick(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image) sender;
            if (im.Name == "next") nextAudio();
            if (im.Name == "prev") Prev();
        }

        private void Prev()
        {
            var i = findIndex(_nowPlaying, _toPlay);
            if (i == null) return;
            int k = (int) i;
            if(k-1 >= 0)
                Choose(_toPlay[k-1]);
        }

        private void Repeat(object sender, MouseButtonEventArgs e)
        {
            // norepeat -> repeatOnce -> repeat -> !!

            switch (_repeatMode)
            {
                case RepeatMode.NoRepeat:
                    SetRepeatState(RepeatMode.RepeatOnce);
                    break;
                case RepeatMode.RepeatOnce:
                    SetRepeatState(RepeatMode.Repeat);
                    break;
                default:
                    SetRepeatState(RepeatMode.NoRepeat);
                    break;
            }
        }

        private void SetRepeatState(RepeatMode rm)
        {
            _repeatMode = rm;
            switch (rm)
            {
                case RepeatMode.NoRepeat:
                    repeat.Source = newImage("Resources/noRepeat.png");
                    repeat.ToolTip = "No repeat";
                    break;
                case RepeatMode.RepeatOnce:
                    repeat.Source = newImage("Resources/repeatOnce.png");
                    repeat.ToolTip = "Repeat once";
                    break;
                default:
                    repeat.Source = newImage("Resources/repeat.png");
                    repeat.ToolTip = "Repeat";
                    break;
            }
            
        }
    }
}