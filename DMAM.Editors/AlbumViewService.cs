using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

using DMAM.Core.Services;

namespace DMAM.Editors
{
    public class AlbumViewService : ServiceBase<AlbumViewService>
    {
        private Dictionary<char, AlbumViewThread> _viewThreads = new Dictionary<char, AlbumViewThread>();

        protected override void OnShutdown()
        {
            var threads = GetAllThreads();
            SendViewCommand(threads, AlbumViewCommand.Close);
            WaitOnAllThreads(threads);
        }

        public void ExecuteCommand(char driveLetter, CDRomCommand command)
        {
            var thread = GetThread(driveLetter);

            switch (command)
            {
                case CDRomCommand.Open:
                {
                    if (thread != null)
                    {
                        thread.SendViewCommand(AlbumViewCommand.BringToFront);
                    }
                    else
                    {
                        LaunchNewView(driveLetter);
                    }

                    break;
                }
                case CDRomCommand.Update:
                {
                    if (thread != null)
                    {
                        thread.SendViewCommand(AlbumViewCommand.Update);
                    }

                    break;
                }
                case CDRomCommand.Close:
                {
                    if (thread != null)
                    {
                        thread.SendViewCommand(AlbumViewCommand.Close);
                    }

                    break;
                }
            };
        }

        private void LaunchNewView(char driveLetter)
        {
            lock (_viewThreads)
            {
                var thread = new AlbumViewThread(driveLetter);
                _viewThreads.Add(driveLetter, thread);
                thread.Initialize();
            }
        }

        private void SendViewCommand(IEnumerable<AlbumViewThread> threads, AlbumViewCommand command)
        {
            foreach (var thread in threads)
            {
                thread.SendViewCommand(command);
            }
        }

        private IEnumerable<AlbumViewThread> GetAllThreads()
        {
            lock (_viewThreads)
            {
                return new List<AlbumViewThread>(_viewThreads.Values);
            }
        }

        private AlbumViewThread GetThread(char driveLetter)
        {
            lock (_viewThreads)
            {
                if (_viewThreads.ContainsKey(driveLetter))
                {
                    return _viewThreads[driveLetter];
                }

                return null;
            }
        }

        private static void WaitOnAllThreads(IEnumerable<AlbumViewThread> threads)
        {
            foreach (var thread in threads)
            {
                thread.Shutdown();
            }
        }
    }
}
