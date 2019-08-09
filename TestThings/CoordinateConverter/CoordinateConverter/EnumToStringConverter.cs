using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{
    public class EnumToStringConverter : EnumConverter
    {
        private readonly Type enumType;

        public EnumToStringConverter(Type _type) : base(_type)
        {
            enumType = _type;
        }

        public override bool CanConvertTo(ITypeDescriptorContext _context, Type _destType)
        {
            return _destType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext _context, CultureInfo _culture, object _value, Type _destType)
        {
            try
            {
                if (_value != null)
                {
                    var fi = enumType.GetField(Enum.GetName(enumType, _value));
                    var dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

                    if (dna != null)
                        return dna.Description;

                    return _value.ToString();
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext _context, Type _srcType)
        {
            return _srcType == typeof(string);
        }

        /// <summary>        
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext _context, CultureInfo _culture, object _value)
        {
            if (!(_value is string))
                return null;

            foreach (var fi in enumType.GetFields())
            {
                var dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (dna != null)
                {
                    if ((string)_value == dna.Description)
                        return Enum.Parse(enumType, fi.Name);
                }
            }

            return Enum.Parse(enumType, (string)_value);
        }
    }
}
