using System;

namespace Raudio
{
    public class AudioFile
    {
        private Uri _absolutePath;
        private MusicID3Tag.Id3Tag tag;
        private string tempPath;

        public string FileName => GetFileName(_absolutePath);
        public Uri Path => _absolutePath;
        
        public string Title => tag.Title;
        public string Artist => tag.Artist;
        public string Album => tag.Album;
        public string Year => tag.Year;
        public string Comment => tag.Comment;
        public string Genre => tag.Genre;

        public AudioFile(Uri absolutePath)
        {
            _absolutePath = absolutePath;
            tag = new MusicID3Tag(_absolutePath.OriginalString).getID3();
        }

        private string GetFileName(Uri path)
        {
            string result = "";
            for (int i = path.OriginalString.Length-1; i > 0; i--)
            {
                if(path.OriginalString[i] == @"\".ToCharArray()[0]) break;
                
                result += path.OriginalString[i];
            }

            return Reverse(result);
        }
        
        public static string Reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}