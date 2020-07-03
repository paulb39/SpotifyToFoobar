using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotifyToFoobar
{
    public partial class Form1 : Form
    {
        private SpotifyWebAPI _spotifyAPI { get; set; }
        private String _clientId { get; set; }
        private List<FullTrack> _tracks { get; set; }

        public Form1()
        {
            InitializeComponent();
            _clientId = @"YOUR SPOTIFY DEV ID HERE";
            _tracks = new List<FullTrack>();
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            var auth = new ImplicitGrantAuth(
                _clientId,
                "http://localhost:4002",
                "http://localhost:4002",
                Scope.UserLibraryModify | Scope.PlaylistModifyPrivate
            );

            auth.Start();
            auth.OpenBrowser();

            auth.AuthReceived += (s, payload) =>
            {
                auth.Stop();
                _spotifyAPI = new SpotifyWebAPI()
                {
                    TokenType = payload.TokenType,
                    AccessToken = payload.AccessToken
                };

                int page = 0;
                bool load = true;

                while (load)
                {
                    Paging<PlaylistTrack> playlist = _spotifyAPI.GetPlaylistTracks(txtPlaylist.Text, null, 100, page);
                    if (!playlist.HasNextPage())
                    {
                        load = false;
                    }
                    

                    foreach (var track in playlist.Items)
                    {
                        _tracks.Add(track.Track);
                    }

                    page += 100;
                }

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"SpotifyPlaylist.m3u", true))
                {
                    foreach (var song in _tracks)
                    {
                        Console.WriteLine(song.Uri);
                        file.WriteLine(song.Uri);
                    }
                }

                Console.WriteLine("DONE");
            };


        }


    }
}
