using System.Numerics;
using System.Runtime.InteropServices;

namespace TQ.Animation
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BoneAnimationFrame
    {
        public Vector3 Offset;
        public Quaternion Rotation1;
        public Vector3 Scale;
        public Quaternion Rotation2;

        public override string ToString()
            => $"O:{Offset} R1:{Rotation1} S:{Scale} R2:{Rotation2}";
    }
}