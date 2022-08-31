using System;
using System.IO;
using System.Text;

namespace Raudio
{
    public class MusicID3Tag
    {
        class i
        {
            public byte[] TAGID = new byte[3];      //  3
            public byte[] Title = new byte[30];     //  30
            public byte[] Artist = new byte[30];    //  30
            public byte[] Album = new byte[30];     //  30 
            public byte[] Year = new byte[4];       //  4 
            public byte[] Comment = new byte[30];   //  30 
            public byte[] Genre = new byte[1];      //  1
        }
        
        public class Id3Tag
        {
            public string Title;
            public string Artist;
            public string Album;
            public string Year;
            public string Comment;
            public string Genre;

            public Id3Tag(string title, string artist, string album, string year, string comment, string genre)
            {
                Title = title;
                Artist = artist;
                Album = album;
                Year = year;
                Comment = comment;
                Genre = genre;
            }
        }

        private string filePath;

        public MusicID3Tag(string filePath)
        {
            this.filePath = filePath;
        }

        public Id3Tag getID3()
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                if (fs.Length >= 128)
                {
                    i tag = new i();
                    fs.Seek(-128, SeekOrigin.End);
                    fs.Read(tag.TAGID, 0, tag.TAGID.Length);
                    fs.Read(tag.Title, 0, tag.Title.Length);
                    fs.Read(tag.Artist, 0, tag.Artist.Length);
                    fs.Read(tag.Album, 0, tag.Album.Length);
                    fs.Read(tag.Year, 0, tag.Year.Length);
                    fs.Read(tag.Comment, 0, tag.Comment.Length);
                    fs.Read(tag.Genre, 0, tag.Genre.Length);
                    string theTAGID = Encoding.Default.GetString(tag.TAGID);

                    if (theTAGID.Equals("TAG"))
                    {
                        string Title = Encoding.Default.GetString(tag.Title);
                        string Artist = Encoding.Default.GetString(tag.Artist);
                        string Album = Encoding.Default.GetString(tag.Album);
                        string Year = Encoding.Default.GetString(tag.Year);
                        string Comment = Encoding.Default.GetString(tag.Comment);
                        string Genre = Encoding.Default.GetString(tag.Genre);

                        return new Id3Tag(Title, Artist, Album, Year, Comment, Genre);

                    }

                    return new Id3Tag(null, null, null, null, null, null);
                }
                
                return new Id3Tag(null, null, null, null, null, null);
            }
        }

    }
}