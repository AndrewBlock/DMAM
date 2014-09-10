using System;
using System.Collections.Generic;

using DMAM.Album.Data.Models;
using DMAM.Core.DataModels;
using DMAM.Core.MVVM;

namespace DMAM.Test.Controls
{
    public class ViewModel : ViewModelBase
    {
        private readonly IEnumerable<TrackData> _tracks;
        private readonly TrackData _track1;
        private readonly TrackData _track2;

        private readonly FieldSet _fieldSet1;
        private readonly FieldSet _fieldSet2;

        private readonly FieldValue _testFieldValue1 = new FieldValue("ArtistName", "Artist Name", FieldValueRank.Primary, true);
        private readonly FieldValue _testFieldValue2 = new FieldValue("AlbumTitle", "Album Title", FieldValueRank.Secondary, true);
        private readonly FieldValue _testFieldValue3 = new FieldValue("Year", "Year", FieldValueRank.General, true);
        private readonly FieldValue _testFieldValue4 = new FieldValue("Label", "Label", FieldValueRank.General, true);
        private readonly FieldValue _testFieldValue5 = new FieldValue("Copyright", "Copyright", FieldValueRank.General, true);

        private readonly FieldValue _testFieldValue6 = new FieldValue("ArtistName", "Artist Name", FieldValueRank.Primary, true);
        private readonly FieldValue _testFieldValue7 = new FieldValue("AlbumTitle", "Album Title", FieldValueRank.Secondary, true);
        private readonly FieldValue _testFieldValue8 = new FieldValue("Year", "Year", FieldValueRank.General, true);
        private readonly FieldValue _testFieldValue9 = new FieldValue("Label", "Label", FieldValueRank.General, true);
        private readonly FieldValue _testFieldValue10 = new FieldValue("Copyright", "Copyright", FieldValueRank.General, true);

        public ViewModel()
        {
            _fieldSet1 = new FieldSet(new MetadataFieldCompare());
            _fieldSet2 = new FieldSet(new MetadataFieldCompare());

            _testFieldValue1.SetOriginalValue("Dave Matthews Band");
            _testFieldValue2.SetOriginalValue("Before These Crowded Streets");
            _testFieldValue3.SetOriginalValue("1998");
            _testFieldValue4.SetOriginalValue("RCA Records");
            _testFieldValue5.SetOriginalValue("Copyright © 1998");

            _testFieldValue6.SetOriginalValue("Tears For Fears");
            _testFieldValue7.SetOriginalValue("Raoul and the Kings of Spain");
            _testFieldValue8.SetOriginalValue("1995");
            _testFieldValue9.SetOriginalValue("Atlantic Records");
            _testFieldValue10.SetOriginalValue("Copyright © 1996");

            _fieldSet1.Initialize(new[] {
                _testFieldValue1,
                _testFieldValue2,
                _testFieldValue3,
                _testFieldValue4,
                _testFieldValue5
            });

            _fieldSet2.Initialize(new[] {
                _testFieldValue6,
                _testFieldValue7,
                _testFieldValue8,
                _testFieldValue9,
                _testFieldValue10
            });

            _track1 = new TrackData("10", _fieldSet1, "3:35.00");
            _track2 = new TrackData("11", _fieldSet2, "2:56.00");
            _tracks = new[] { _track1, _track2 };
        }

        public IEnumerable<TrackData> Tracks
        {
            get
            {
                return _tracks;
            }
        }

        public TrackData Track1
        {
            get
            {
                return _track1;
            }
        }

        public FieldSet FieldSet1
        {
            get
            {
                return _fieldSet1;
            }
        }

        public FieldSet FieldSet2
        {
            get
            {
                return _fieldSet2;
            }
        }

        public FieldValue TestFieldValue1
        {
            get
            {
                return _testFieldValue1;
            }
        }

        public FieldValue TestFieldValue2
        {
            get
            {
                return _testFieldValue2;
            }
        }

        public FieldValue TestFieldValue3
        {
            get
            {
                return _testFieldValue3;
            }
        }

        public FieldValue TestFieldValue4
        {
            get
            {
                return _testFieldValue4;
            }
        }

        public FieldValue TestFieldValue5
        {
            get
            {
                return _testFieldValue5;
            }
        }
    }

    public class MetadataFieldCompare : IFieldValueCompare
    {
        public int Compare(FieldValue x, FieldValue y)
        {
            return string.Compare(x.FieldName, y.FieldName);
        }
    }
}
