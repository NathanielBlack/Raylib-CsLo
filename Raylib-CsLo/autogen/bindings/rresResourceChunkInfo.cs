//# raylib 4.0 bindings.   MPL 2.0 Licensed.  Source here: https://github.com/NotNotTech/Raylib-CsLo
//# Find Raylib+docs here:   https://github.com/raysan5/raylib/blob/master/src/raylib.h
//# This file, and it's containing folder are automatically generated.  Do not Modify.
namespace Raylib_CsLo
{
    public unsafe partial struct rresResourceChunkInfo
    {
        [NativeTypeName("unsigned char [4]")]
        public fixed byte type[4];

        [NativeTypeName("unsigned int")]
        public uint id;

        [NativeTypeName("unsigned char")]
        public byte compType;

        [NativeTypeName("unsigned char")]
        public byte cipherType;

        [NativeTypeName("unsigned short")]
        public ushort flags;

        [NativeTypeName("unsigned int")]
        public uint packedSize;

        [NativeTypeName("unsigned int")]
        public uint baseSize;

        [NativeTypeName("unsigned int")]
        public uint nextOffset;

        [NativeTypeName("unsigned int")]
        public uint reserved;

        [NativeTypeName("unsigned int")]
        public uint crc32;
    }
}
