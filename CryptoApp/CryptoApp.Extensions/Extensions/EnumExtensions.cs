using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CryptoApp.Extensions.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить название из атрибута <see cref="DescriptionAttribute"/>
        /// для элемента перечисления
        /// </summary>
        public static string GetDescription(this Enum genericEnum)
        {
            Type genericEnumType = genericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            
            if (memberInfo.Length > 0)
            {
                var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribs.Any())
                {
                    return ((DescriptionAttribute)attribs.ElementAt(0)).Description;
                }
            }
            
            return genericEnum.ToString();
        }
        
        /// <summary>
        /// Получить коллекцию элементов перечисления
        /// </summary>
        public static IEnumerable<T> GetValues<T>() 
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}