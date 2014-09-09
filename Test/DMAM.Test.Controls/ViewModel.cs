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
        private readonly TrackData _track;

        private readonly FieldSet _fieldSet;

        private readonly FieldValue _testFieldValue1 = new FieldValue("ArtistName", "Artist Name", FieldValueRank.Primary, true);
        private readonly FieldValue _testFieldValue2 = new FieldValue("AlbumTitle", "Album Title", FieldValueRank.Secondary, true);
        private readonly FieldValue _testFieldValue3 = new FieldValue("Year", "Year", FieldValueRank.General, true);
        private readonly FieldValue _testFieldValue4 = new FieldValue("Label", "Label", FieldValueRank.General, true);
        private readonly FieldValue _testFieldValue5 = new FieldValue("Copyright", "Copyright", FieldValueRank.General, true);

        public ViewModel()
        {
            _fieldSet = new FieldSet(new MetadataFieldCompare());

            _testFieldValue1.SetOriginalValue("Dave Matthews Band");
            _testFieldValue2.SetOriginalValue("Before These Crowded Streets");
            _testFieldValue3.SetOriginalValue("1998");
            _testFieldValue4.SetOriginalValue("RCA Records");
            _testFieldValue5.SetOriginalValue("Copyright © 1998");

            _fieldSet.Initialize(new[] {
                _testFieldValue1,
                _testFieldValue2,
                _testFieldValue3,
                _testFieldValue4,
                _testFieldValue5
            });

            _track = new TrackData("12", _fieldSet, "3:35.00");
            _tracks = new[] { _track };
        }

        public IEnumerable<TrackData> Tracks
        {
            get
            {
                return _tracks;
            }
        }

        public TrackData Track
        {
            get
            {
                return _track;
            }
        }

        public FieldSet FieldSet
        {
            get
            {
                return _fieldSet;
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
