using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Raudio
{
    public class FileControl
    {
        OpenFileDialog _dialog = new();
        public EventHandler FilePickerOpened;
        public EventHandler FilePickerSuccess;
        public EventHandler FilePickerCanceled;
        public List<AudioFile> _audioFiles;

        public FileControl(string fileExtentions)
        {
            _dialog.Multiselect = true;
            _dialog.Filter = fileExtentions;
            _dialog.FileName = "Music";
            _dialog.Title = "Choose music file(s)...";
        }

        public void FilePicker()
        {
            if (FilePickerCanceled == null) FilePickerCanceled = i;
            if (FilePickerSuccess == null) FilePickerSuccess = i;
            if (FilePickerOpened == null) FilePickerOpened = i;

            bool? success = _dialog.ShowDialog();
            FilePickerOpened.Invoke(this, EventArgs.Empty);

            if (success == true)
            {
                _audioFiles = new List<AudioFile>();
                foreach (var fn in _dialog.FileNames)
                {
                    _audioFiles.Add(new AudioFile(new Uri(fn, UriKind.Absolute)));
                }
                FilePickerSuccess.Invoke(this, EventArgs.Empty);
                return;
            }
            
            FilePickerCanceled.Invoke(this, EventArgs.Empty);
        }

        void i(object? sender, EventArgs e)
        {
        }
    }
}