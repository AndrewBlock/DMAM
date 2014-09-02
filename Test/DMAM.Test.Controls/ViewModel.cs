using System;
using System.Collections.Generic;

using DMAM.Core.MVVM;
using DMAM.DataModels;

namespace DMAM.Test.Controls
{
    public class ViewModel : ViewModelBase
    {
        private readonly FieldValue _testFieldValue1 = new FieldValue("ArtistName", "Artist Name", true);
        private readonly FieldValue _testFieldValue2 = new FieldValue("AlbumTitle", "Album Title", true);
        private readonly FieldValue _testFieldValue3 = new FieldValue("Year", "Year", true);
        private readonly FieldValue _testFieldValue4 = new FieldValue("Label", "Label", true);
        private readonly FieldValue _testFieldValue5 = new FieldValue("Copyright", "Copyright", true);

        public ViewModel()
        {
            _testFieldValue1.SetOriginalValue("Dave Matthews Band");
            _testFieldValue2.SetOriginalValue("Before These Crowded Streets");
            _testFieldValue3.SetOriginalValue("1998");
            _testFieldValue4.SetOriginalValue("RCA Records");
            _testFieldValue5.SetOriginalValue("Copyright © 1998");
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
}
