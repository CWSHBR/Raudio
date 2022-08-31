using System;
using System.Media;
using System.Windows;
using System.Windows.Media;

namespace Raudio
{
    public class AudioControl 
    {
        private MediaPlayer player = new MediaPlayer();
        private bool is_playing;
        private double maxVolume;
        private string audioFileName;

        public bool IsPlaying => is_playing;
        public TimeSpan TimePosition => player.Position;
        public Duration Duration => player.NaturalDuration;
        public double Balance => player.Balance;
        public double Volume => maxVolume;
        public string FileName => audioFileName;
        public EventHandler AudioOpened;

        public AudioControl()
        {
            player.MediaOpened += onOpened;
        }

        public void Open(Uri uri)
        {
            Stop();
            player.Open(uri);
            audioFileName = uri.LocalPath;
        }

        private void onOpened(object? sender, EventArgs e)
        {
            AudioOpened.Invoke(this, EventArgs.Empty);
        }
        
        public void Play()
        {
            if (player.HasAudio & !is_playing)
            {
                player.Play();
                is_playing = true;
            }
        }

        public void Wind(TimeSpan pos)
        {
            bool temp = false;
            if (is_playing)
            {
                temp = true;
                Stop();
            }
            player.Position = pos;
            if(temp) Play();
        }

        public void SetVolume(double volume)
        {
            if (volume >= 1d)
            {
                maxVolume = 1;
                return;
            }

            maxVolume = volume;
        }

        public void ChangeBalance(double balance) // -1 - 1
        {
            player.Balance = balance switch // bounds
            {
                < -1d => -1d,
                > 1d => 1d,
                _ => balance
            };
        }

        public void Pause()
        {
            if (player.HasAudio & player.CanPause & IsPlaying)
            {
                player.Pause();
                is_playing = false;
            }
        }

        public void Stop()
        {
            if (player.HasAudio & IsPlaying)
            {
                player.Stop();
                is_playing = false;
            }
        }
        
    }
}