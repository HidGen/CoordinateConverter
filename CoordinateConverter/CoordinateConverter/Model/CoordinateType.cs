using System.ComponentModel;

namespace CoordinateConverter.ViewModel
{
    [TypeConverter(typeof(EnumToStringConverter))]
    public enum CoordinateType
    {
        [Description("МСК-46 зона 1")]
        MSK461,
        [Description("МСК-46 зона 2")]
        MSK462,        
        [Description("СК-42 зона 2")]
        SK42zone2,
        [Description("СК-42 зона 3")]
        SK42zone3,
        [Description("СК-42 зона 4")]
        SK42zone4,
        [Description("СК-42 зона 5")]
        SK42zone5,
        [Description("СК-42 зона 6")]
        SK42zone6,
        [Description("СК-42 зона 7")]
        SK42zone7,
        [Description("СК-42 зона 8")]
        SK42zone8,
        [Description("СК-42 зона 9")]
        SK42zone9,
        [Description("СК-42 зона 10")]
        SK42zone10,
        [Description("СК-42 зона 11")]
        SK42zone11,
        [Description("СК-42 зона 12")]
        SK42zone12,
        [Description("СК-42 зона 13")]
        SK42zone13,
        [Description("СК-42 зона 14")]
        SK42zone14,
        [Description("СК-42 зона 15")]
        SK42zone15,
        [Description("СК-42 зона 16")]
        SK42zone16,
        [Description("СК-42 зона 17")]
        SK42zone17,
        [Description("СК-42 зона 18")]
        SK42zone18,
        [Description("СК-42 зона 19")]
        SK42zone19,
        [Description("СК-42 зона 20")]
        SK42zone20,
        [Description("СК-42 зона 21")]
        SK42zone21,
        [Description("СК-42 зона 22")]
        SK42zone22,
        [Description("СК-42 зона 23")]
        SK42zone23,
        [Description("СК-42 зона 24")]
        SK42zone24,
        [Description("СК-42 зона 25")]
        SK42zone25,
        [Description("СК-42 зона 26")]
        SK42zone26,
        [Description("СК-42 зона 27")]
        SK42zone27,
        [Description("СК-42 зона 28")]
        SK42zone28,
        [Description("СК-42 зона 29")]
        SK42zone29,
        [Description("СК-42 зона 30")]
        SK42zone30,
        [Description("СК-63")]
        SK63
    }


}
