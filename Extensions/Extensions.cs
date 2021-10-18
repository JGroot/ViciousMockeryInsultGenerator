using System.ComponentModel;
using DeezNDeezTools.Data.Models;

namespace DeezNDeezTools.Extensions
{
    public static class Extensions
    {

        public static string ToDescriptionString(this FailureCategory val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
