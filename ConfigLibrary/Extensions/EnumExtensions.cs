using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLibrary.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            try
            {
                var field = value.GetType().GetField(value.GetName());
                var descriptionAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                return descriptionAttribute == null ? value.GetName() : descriptionAttribute.Description;
            }
            catch (System.Exception)
            {
                return value.ToString();
            }
        }
        private static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }
    }
}
