using System.ComponentModel;

namespace CoordinateConverter.ViewModel
{
    [TypeConverter(typeof(EnumToStringConverter))]
    public enum SortType
    {
        [Description("X по возрастанию")]
        MinMaxX,
        [Description("X по убыванию")]
        MaxMinX,
        [Description("Y по возрастанию")]
        MinMaxY,
        [Description("Y по убыванию")]
        MaxMinY
    }
}
