using System;

using DMAM.Core.Services;
using DMAM.Device;

namespace DMAM.Editors
{
    internal class AlbumViewThread : UIThreadBase
    {
        private readonly char _driveLetter;
        private AlbumView _view;
        private AlbumViewModel _viewModel;

        public AlbumViewThread(char driveLetter)
            : base("AlbumViewThread")
        {
            _driveLetter = driveLetter;
        }
        
        protected override bool OnInitialize()
        {
            _view = new AlbumView();
            _viewModel = new AlbumViewModel(_driveLetter);

            _view.DataContext = _viewModel;
            _view.Closed += View_Closed;
            _view.Show();

            _viewModel.UpdateTrackListing();

            VolumeService.GetInstance().CDRomDriveUpdate.Subscribe(HandleCDRomDriveStatus);

            return true;
        }

        protected override void OnShutdown()
        {
            VolumeService.GetInstance().CDRomDriveUpdate.Unsubscribe(HandleCDRomDriveStatus);
            
            _view.Closed -= View_Closed;
            _view = null;
            _viewModel = null;
        }

        public void SendViewCommand(AlbumViewCommand command)
        {
            ThreadDispatcher.BeginInvoke(new AlbumViewCommandHandler(HandleViewCommand),
                    new object[] { command });
        }

        private void HandleCDRomDriveStatus(CDRomDriveStatus status)
        {
            if (status.DriveLetter == _driveLetter)
            {
                HandleViewCommand(AlbumViewCommand.Update);
            }
        }

        private void View_Closed(object sender, EventArgs e)
        {
            ThreadDispatcher.InvokeShutdown();
        }

        private void HandleViewCommand(AlbumViewCommand command)
        {
            switch (command)
            {
                case AlbumViewCommand.BringToFront:
                {
                    _view.BringIntoView();
                    _view.Focus();
                    break;
                }
                case AlbumViewCommand.Update:
                {
                    _viewModel.UpdateTrackListing();
                    break;
                }
                case AlbumViewCommand.Close:
                {
                    _view.Close();
                    break;
                }
            }
        }
    }
}
