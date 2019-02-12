using SpanUtils;
using System;
using System.Runtime.InteropServices;
using TQ.Common;

namespace TQ.Animation
{
    public readonly ref struct Animation
    {
        readonly Span<byte> _data;
        public Animation(Span<byte> data)
        {
            _data = data;
            for (int i = 0; i < _magic.Length; i++)
            {
                if (_data[i] != _magic[i])
                { throw new ArgumentException("Wrong magic! Expected file to start with ANM.", nameof(data)); }
            }
        }

        static readonly byte[] _magic = Definitions.Encoding.GetBytes("ANM");

        public ref Header Header => ref _data.View<Header>(0);

        public Enumerator GetEnumerator() => new Enumerator(this);
        public ref struct Enumerator
        {
            readonly Animation _animation;
            internal int _index;
            internal int _offset;
            public Enumerator(Animation animation)
            {
                _animation = animation;
                _index = -1;
                _offset = -1;
            }

            int _CurrentSize => sizeof(int) + _animation._data.View<int>(_offset) + (int)_animation.Header.FrameCount * Marshal.SizeOf<BoneAnimationFrame>();

            public BoneAnimation Current => new BoneAnimation(_animation._data.Slice(_offset, _CurrentSize));

            public bool MoveNext()
            {
                if (_offset == -1) _offset = Marshal.SizeOf<Header>();
                else _offset += _CurrentSize;
                switch (++_index)
                {
                    case var i when i < _animation.Header.BoneCount: return true;
                    case var i when i == _animation.Header.BoneCount: return false;
                    default /* var i when i > _animation.Header.FrameCount */: throw new InvalidOperationException("Tried to move beyond end of bone data.");
                }
            }

            public string TextAfterEnd =>
                _index != _animation.Header.BoneCount ? throw new InvalidOperationException("Can only read text after iterating all bones.")
                : Definitions.Encoding.GetString(_animation._data.Slice(_offset));
        }
    }
}