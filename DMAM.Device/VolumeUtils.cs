using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DMAM.Interop.IO;

namespace DMAM.Device
{
    internal class VolumeUtils
    {
        public static char[] GetDriveLettersFromUnitsMask(uint unitMask)
        {
            var driveLetters = new List<char>();

            uint currentMask = 0x00000001;
            for (var driveIndex = 0; driveIndex < 26; driveIndex++)
            {
                if ((unitMask & currentMask) == currentMask)
                {
                    driveLetters.Add(GetDriveLetterFromDriveIndex(driveIndex));
                }

                currentMask <<= 1;
            }

            return driveLetters.ToArray();
        }

        public static char GetDriveLetterFromDriveIndex(int driveIndex)
        {
            return (char) ('A' + driveIndex);
        }

        public static string GetDrivePathFromDriveLetter(char driveLetter)
        {
            return string.Format("{0}:\\", driveLetter);
        }

        public static VolumeType Convert(DriveType driveType)
        {
            switch (driveType)
            {
                case DriveType.DRIVE_NO_ROOT_DIR: return VolumeType.NoRootDirectory;
                case DriveType.DRIVE_REMOVABLE: return VolumeType.Removable;
                case DriveType.DRIVE_FIXED: return VolumeType.HardDrive;
                case DriveType.DRIVE_REMOTE: return VolumeType.NetworkLocation;
                case DriveType.DRIVE_CDROM: return VolumeType.CDRomDrive;
                case DriveType.DRIVE_RAMDISK: return VolumeType.RAMDisk;
            }

            return VolumeType.Unknown;
        }
    }
}
