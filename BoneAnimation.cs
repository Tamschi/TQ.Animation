using SpanUtils;
using System;
using System.Collections.Generic;
using System.Text;
using TQ.Common;

namespace TQ.Animation
{
    public readonly ref struct BoneAnimation
    {
        readonly Span<byte> _data;
        public BoneAnimation(Span<byte> data) => _data = data;

        int _NameLength => _data.View<int>(0);

        public string Name => Definitions.Encoding.GetString(_data.Slice(sizeof(int), _NameLength));
        public Span<BoneAnimationFrame> Frames => _data.Slice(sizeof(int) + _NameLength).Cast<BoneAnimationFrame>();
    }
}