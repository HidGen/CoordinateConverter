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
        [Description("СК-42")]
        SK42,
        [Description("СК-63")]
        SK63
    }


}
