using System.Runtime.InteropServices;

namespace TQ.Animation
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header
    {
        unsafe fixed byte _magic[3];
        public byte Version;
        public uint BoneCount;
        public uint FrameCount;
        public uint Fps;
    }
}