using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing
{
    public class Helper
    {
        public static object GetEnumObject<T>() where T : struct, IConvertible
        {
            return from T e in Enum.GetValues(typeof(T))
                   select new
                   {
                       Id = (int)Enum.Parse(typeof(T), e.ToString()),
                       Name = e,
                       Description = EnumDescriptor<T>.GetDisplayName(e)
                   };
        }

        public static int GetTimeInMinutes(DateTime gTime)
        {
            var tSpan = new TimeSpan(gTime.Hour, gTime.Minute, gTime.Second);
            return Convert.ToInt32(tSpan.TotalMinutes);
        }
    }
    public static class EnumDescriptor<T>
    {
        public static string GetDisplayName(T enumObject)
        {
            var field = enumObject.GetType()
                .GetField(enumObject.ToString());

            if (field != null)
            {
                var display = ((DisplayAttribute[])field.GetCustomAttributes(typeof(DisplayAttribute), false))
                    .FirstOrDefault();

                if (display != null)
                {
                    return display.Name;
                }
            }

            return enumObject.ToString();
        }
    }
}
