using System;
using ObjCRuntime;

namespace Intune.iOS
{
    public static class Common
    {
        public static string DeviceAccountStoreName = "IntuneTechnologiesApp";
        public static bool IsRunningOnDevice()
        {
            return Runtime.Arch == ObjCRuntime.Arch.DEVICE;
        }
    }
}
