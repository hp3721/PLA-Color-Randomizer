using System;
using System.ComponentModel;
using FlatSharp.Attributes;
using pkNX.Structures.FlatBuffers;

namespace PLA_Color_Randomizer
{
    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtr8a
    {
        public byte[] Write() => FlatBufferConverter.SerializeFrom(this);

        [FlatBufferItem(00)] public uint Field_00 { get; set; }
        [FlatBufferItem(01)] public trmtrMaterial8a[] Materials { get; set; } = Array.Empty<trmtrMaterial8a>();
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrMaterial8a
    {
        [FlatBufferItem(00)] public string Name { get; set; } = string.Empty;
        [FlatBufferItem(01)] public trmtrShader8a[] Flags { get; set; } = Array.Empty<trmtrShader8a>();
        [FlatBufferItem(02)] public trmtrMap8a[] Textures { get; set; } = Array.Empty<trmtrMap8a>();
        [FlatBufferItem(03)] public trmtrSamplerState8a[] SamplerStates { get; set; } = Array.Empty<trmtrSamplerState8a>();
        [FlatBufferItem(04)] public trmtrFloatValue8a[] FloatValues { get; set; } = Array.Empty<trmtrFloatValue8a>();
        [FlatBufferItem(05)] public uint[] Field_05 { get; set; } = Array.Empty<uint>();
        [FlatBufferItem(06)] public uint[] Field_06 { get; set; } = Array.Empty<uint>();
        [FlatBufferItem(07)] public trmtrColorValue8a[] ColorValues { get; set; } = Array.Empty<trmtrColorValue8a>();
        [FlatBufferItem(08)] public uint[] Field_08 { get; set; } = Array.Empty<uint>();
        [FlatBufferItem(09)] public trmtrIntValue8a[] IntValues { get; set; } = Array.Empty<trmtrIntValue8a>();
        [FlatBufferItem(10)] public uint[] Field_10 { get; set; } = Array.Empty<uint>();
        [FlatBufferItem(11)] public uint[] Field_11 { get; set; } = Array.Empty<uint>();
        [FlatBufferItem(12)] public uint[] Field_12 { get; set; } = Array.Empty<uint>();
        [FlatBufferItem(13)] public trmtrUnkTable8a Field_13 { get; set; }
        [FlatBufferItem(14)] public trmtrUnkTable8a Field_14 { get; set; }
        [FlatBufferItem(15)] public string AlphaType { get; set; } = string.Empty;
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrUnkTable8a
    {
        [FlatBufferItem(00)] public uint Field_00 { get; set; }
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrShader8a
    {
        [FlatBufferItem(00)] public string ShaderType { get; set; } = string.Empty;
        [FlatBufferItem(01)] public trmtrStringValue8a[] Flags { get; set; } = Array.Empty<trmtrStringValue8a>();
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrUnkValue8a
    {
        [FlatBufferItem(00)] public string UnkName { get; set; } = string.Empty;
        [FlatBufferItem(01)] public uint UnkValue { get; set; }
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrColorValue8a
    {
        [FlatBufferItem(00)] public string ColorName { get; set; } = string.Empty;
        [FlatBufferItem(01)] public trmtrRGBA8b ColorValue { get; set; }
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrFloatValue8a
    {
        [FlatBufferItem(00)] public string FloatName { get; set; } = string.Empty;
        [FlatBufferItem(01)] public float FloatValue { get; set; }
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrIntValue8a
    {
        [FlatBufferItem(00)] public string IntName { get; set; } = string.Empty;
        [FlatBufferItem(01)] public uint IntValue { get; set; }
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrStringValue8a
    {
        [FlatBufferItem(00)] public string StringName { get; set; } = string.Empty;
        [FlatBufferItem(01)] public string StringValue { get; set; } = string.Empty;
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrMap8a
    {
        [FlatBufferItem(00)] public string MapName { get; set; } = string.Empty;
        [FlatBufferItem(01)] public string MapFile { get; set; } = string.Empty;
        [FlatBufferItem(02)] public uint MapSlot { get; set; }
    }

    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class trmtrSamplerState8a
    {
        [FlatBufferItem(00)] public uint SamplerState0 { get; set; }
        [FlatBufferItem(01)] public uint SamplerState1 { get; set; }
        [FlatBufferItem(02)] public uint SamplerState2 { get; set; }
        [FlatBufferItem(03)] public uint SamplerState3 { get; set; }
        [FlatBufferItem(04)] public uint SamplerState4 { get; set; }
        [FlatBufferItem(05)] public uint SamplerState5 { get; set; }
        [FlatBufferItem(06)] public uint SamplerState6 { get; set; }
        [FlatBufferItem(07)] public uint SamplerState7 { get; set; }
        [FlatBufferItem(08)] public uint SamplerState8 { get; set; }
        [FlatBufferItem(09)] public trmtrUVWrapMode8a RepeatU { get; set; }
        [FlatBufferItem(10)] public trmtrUVWrapMode8a RepeatV { get; set; }
        [FlatBufferItem(11)] public trmtrUVWrapMode8a Repeat2 { get; set; }
        [FlatBufferItem(12)] public trmtrRGBA8b BorderColor { get; set; }
        [FlatBufferItem(13)] public float MipMapBias { get; set; }
    }

    [FlatBufferEnum(typeof(uint))]
    public enum trmtrUVWrapMode8a : uint
    {
        WRAP = 0,
        CLAMP = 1,
        MIRROR = 2,
        BORDER = 3,
        MIRROR_ONCE = 4,
    }

    [FlatBufferStruct]
    public class trmtrRGBA8b
    {
        [FlatBufferItem(00)] public float R { get; set; }
        [FlatBufferItem(01)] public float G { get; set; }
        [FlatBufferItem(02)] public float B { get; set; }
        [FlatBufferItem(03)] public float A { get; set; }
    }

}