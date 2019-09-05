using System.ComponentModel;

namespace CoordinateConverter.ViewModel
{
    [TypeConverter(typeof(EnumToStringConverter))]
    public enum CoordViewType
    {

        [Description("Десятичные градусы")]
        Decimal,
        [Description("Градусы, минуты, секунды")]
        MinSec
    }


}
