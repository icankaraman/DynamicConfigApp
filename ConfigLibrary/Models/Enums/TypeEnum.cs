using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLibrary.Models.Enums
{
    public enum TypeEnum : byte
    {
        [Description("Bool")]
        Bool = 0,

        [Description("Byte")]
        Byte = 1,

        [Description("String")]
        String = 2,

        [Description("Decimal")]
        Decimal = 3,

        [Description("Double")]
        Double = 4,

        [Description("Float")]
        Float = 5,

        [Description("Int32")]
        Int = 6,

        [Description("Long")]
        Long = 7
    }
}
