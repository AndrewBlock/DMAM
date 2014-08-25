using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DMAM.Interop.IO;

namespace DMAM.Device
{
    public class AudioCDUtils
    {
        private const int CDSectorOffset = 150;
        private const int CDBlocksPerSecond = 75;
        private const int SecondsPerMinute = 60;
        private const int MillisecondsPerSecond = 1000;
        
        public static bool DoesDriveContainMusicCD(char driveLetter)
        {
            var driveHandle = IntPtr.Zero;

            try
            {
                driveHandle = OpenVolume(driveLetter);
                var toc = ReadToc(driveHandle);

                return true;
            }
            catch (AudioCDUtilException)
            {
                return false;
            }
            finally
            {
                if (driveHandle != IntPtr.Zero)
                {
                    VolumeInterop.CloseHandle(driveHandle);
                }
            }
        }

        public static IEnumerable<AudioCDTrack> GetAudioCDTracks(char driveLetter)
        {
            var driveHandle = IntPtr.Zero;

            try
            {
                driveHandle = OpenVolume(driveLetter);
                var toc = ReadToc(driveHandle);
                return ReadTracksFromToc(toc);
            }
            catch (AudioCDUtilException)
            {
                return null;
            }
            finally
            {
                if (driveHandle != IntPtr.Zero)
                {
                    VolumeInterop.CloseHandle(driveHandle);
                }
            }
        }

        public static string GetAudioCDToc(char driveLetter)
        {
            var driveHandle = IntPtr.Zero;

            try
            {
                driveHandle = OpenVolume(driveLetter);
                var toc = ReadToc(driveHandle);

                var tocOffsets = GetTocOffsets(toc);
                var searchToc = string.Join(" ", tocOffsets);

                return searchToc;
            }
            catch (AudioCDUtilException)
            {
                return null;
            }
            finally
            {
                if (driveHandle != IntPtr.Zero)
                {
                    VolumeInterop.CloseHandle(driveHandle);
                }
            }
        }

        private static IntPtr OpenVolume(char driveLetter)
        {
            var driveDevicePath = string.Format("\\\\.\\{0}:", driveLetter);

            var driveHandle = VolumeInterop.CreateFile(driveDevicePath, FileAccess.GENERIC_READ, FileShare.FILE_SHARE_READ,
                IntPtr.Zero, CreationDisposition.OPEN_EXISTING, FileAttribute.FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

            if (driveHandle == VolumeInterop.INVALID_HANDLE_VALUE)
            {
                throw new AudioCDUtilException();
            }

            return driveHandle;
        }

        private static _CDROM_TOC ReadToc(IntPtr deviceHandle)
        {
            var toc = new _CDROM_TOC();
            var tocSize = Marshal.SizeOf(toc);
            var bytesReturned = 0;

            if (!VolumeInterop.DeviceIoControl(deviceHandle, IoControlCode.IOCTL_CDROM_READ_TOC,
                IntPtr.Zero, 0, out toc, tocSize, out bytesReturned, IntPtr.Zero))
            {
                throw new AudioCDUtilException();
            }

            return toc;
        }

        private static IEnumerable<AudioCDTrack> ReadTracksFromToc(_CDROM_TOC toc)
        {
            var tracks = new List<AudioCDTrack>();

            for (var trackIndex = toc.FirstTrack; trackIndex <= toc.LastTrack; trackIndex++)
            {
                var arrayIndex = trackIndex - toc.FirstTrack;

                var trackStartAddress = ConvertCDAddressToSectors(
                    toc.TrackData[arrayIndex].Address);
                var trackEndAddress = ConvertCDAddressToSectors(
                    toc.TrackData[arrayIndex+1].Address);

                tracks.Add(new AudioCDTrack(toc.TrackData[arrayIndex].TrackNumber,
                    trackStartAddress, trackEndAddress - trackStartAddress));
            }

            return tracks;
        }

        private static string[] GetTocOffsets(_CDROM_TOC toc)
        {
            var offsets = new List<string>();
            
            for (var trackIndex = 0; trackIndex <= toc.LastTrack; trackIndex++)
            {
                var offset = GetSectorOffset(toc.TrackData[trackIndex].Address);
                offsets.Add(offset.ToString());
            }

            return offsets.ToArray();
        }

        private static int GetSectorOffset(byte[] cdAddress)
        {
            return ((int) cdAddress[1] * (CDBlocksPerSecond * SecondsPerMinute))
                + ((int) cdAddress[2] * CDBlocksPerSecond) + (int) cdAddress[3];
        }

        private static int ConvertCDAddressToSectors(byte[] cdAddress)
        {
            var sectors = ((int) cdAddress[1] * (CDBlocksPerSecond * SecondsPerMinute))
                + ((int) cdAddress[2] * CDBlocksPerSecond) + (int) cdAddress[3];

            return sectors - CDSectorOffset;
        }

        public static double GetTrackLengthInSeconds(int sectors)
        {
            return (Convert.ToDouble(sectors) / Convert.ToDouble(CDBlocksPerSecond));
        }

        public static string GetTrackLengthDisplay(int sectors)
        {
            var totalseconds = sectors / CDBlocksPerSecond;
            var minutes = totalseconds / SecondsPerMinute;
            var seconds = totalseconds % SecondsPerMinute;

            var millisecondBlocks = sectors % CDBlocksPerSecond;
            var milliseconds = (millisecondBlocks * MillisecondsPerSecond) / CDBlocksPerSecond;

            return string.Format("{0:D}:{1:D2}.{2:D3}", minutes, seconds, milliseconds);
        }
    }
}
