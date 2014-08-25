using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;

using DMAM.Core.Metadata;
using DMAM.Core.MVVM;
using DMAM.Device;
using DMAM.Gracenote;

namespace DMAM.Editors
{
    internal class AlbumViewModel : ViewModelBase
    {
        private readonly char _driveLetter;
        private string _toc;

        private ImageSource _coverArt;
        private string _artistName = string.Empty;
        private string _albumName = string.Empty;
        private string _year = string.Empty;

        private IEnumerable<AlbumTrack> _tracks;

        public AlbumViewModel(char driveLetter)
        {
            _driveLetter = driveLetter;
        }

        public ImageSource CoverArt
        {
            get
            {
                return _coverArt;
            }
            set
            {
                if (value == _coverArt)
                {
                    return;
                }

                _coverArt = value;
                NotifyPropertyChanged("CoverArt");
            }
        }

        public string ArtistName
        {
            get
            {
                return _artistName;
            }
            set
            {
                if (value == _artistName)
                {
                    return;
                }

                _artistName = value;
                NotifyPropertyChanged("ArtistName");
            }
        }

        public string AlbumName
        {
            get
            {
                return _albumName;
            }
            set
            {
                if (value == _albumName)
                {
                    return;
                }

                _albumName = value;
                NotifyPropertyChanged("AlbumName");
            }
        }

        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (value == _year)
                {
                    return;
                }

                _year = value;
                NotifyPropertyChanged("Year");
            }
        }

        public IEnumerable<AlbumTrack> Tracks
        {
            get
            {
                return _tracks;
            }
            set
            {
                if (value == _tracks)
                {
                    return;
                }

                _tracks = value;
                NotifyPropertyChanged("Tracks");
            }
        }

        public void UpdateTrackListing()
        {
            ClearDisplayValues();
            
            _toc = AudioCDUtils.GetAudioCDToc(_driveLetter);
            if (_toc == null)
            {
                ClearDisplayValues();
                return;
            }

            CDDBService.GetInstance().QueueAlbumLookupByToc(_toc, OnAlbumLookupByTocComplete, null);
            Tracks = ProcessAlbumTracks(AudioCDUtils.GetAudioCDTracks(_driveLetter));
        }

        private void OnAlbumLookupByTocComplete(AlbumLookupInfo info)
        {
            if (info.Albums.Count() == 0)
            {
                return;
            }

            var album = info.Albums.First();

            ResolveAlbumMetadata(album.AlbumMetadata);
            ResolveTrackMetadata(album.TrackMetadata);
            ResolveCoverArtUrls(album.CoverArtUrls);
        }

        private void ClearDisplayValues()
        {
            CoverArt = null;
            ArtistName = string.Empty;
            AlbumName = string.Empty;
            Year = string.Empty;

            Tracks = new List<AlbumTrack>();
        }

        private IEnumerable<AlbumTrack> ProcessAlbumTracks(IEnumerable<AudioCDTrack> tracks)
        {
            var albumTracks = new List<AlbumTrack>();

            foreach (var track in tracks)
            {
                albumTracks.Add(new AlbumTrack(track.Index, track.DisplayLength));
            }

            return albumTracks;
        }

        private void ResolveAlbumMetadata(ItemMetadata albumMetadata)
        {
            ArtistName = albumMetadata["ARTIST"];
            AlbumName = albumMetadata["TITLE"];
            Year = albumMetadata["DATE"];
        }

        private void ResolveTrackMetadata(IEnumerable<ItemMetadata> tracks)
        {
            foreach (var track in tracks)
            {
                var trackNumberString = track["TRACK_NUM"];
                var trackTitle = track["TITLE"];

                var trackNumber = int.Parse(trackNumberString);
                var albumTrack = FindAlbumTrack(trackNumber);
                if (albumTrack == null)
                {
                    continue;
                }

                albumTrack.TrackTitle = trackTitle;
            }
        }

        private AlbumTrack FindAlbumTrack(int trackNumber)
        {
            foreach (var albumTrack in Tracks)
            {
                if (albumTrack.TrackNumber == trackNumber)
                {
                    return albumTrack;
                }
            }

            return null;
        }

        private void ResolveCoverArtUrls(IEnumerable<Uri> coverArtUrls)
        {
            if (coverArtUrls.Count() == 0)
            {
                return;
            }

            CDDBService.GetInstance().QueueCoverArtDownload(coverArtUrls.First(), OnCoverArtDownloadComplete, null);
        }

        private void OnCoverArtDownloadComplete(CoverArtInfo info)
        {
            var imageStream = new MemoryStream(info.CoverArtImage);
            var decoder = JpegBitmapDecoder.Create(imageStream, BitmapCreateOptions.None, BitmapCacheOption.None);
            CoverArt = decoder.Frames[0];
        }
    }
}
