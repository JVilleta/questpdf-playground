using System.ComponentModel;
using System.Reflection;

namespace PDFPlayground.Extensions
{
    public static class IntExtensions
    {
        public static string GetEnumDescription<TEnum>(this int value) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new ArgumentException($"No enum value found for integer: {value}");

            var enumValue = (TEnum)(object)value;
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString())!;

            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
            return attribute?.Description ?? enumValue.ToString();
        }

        public static string GetEnumDescription(this int value, Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("Provided type must be an Enum.");

            if (!Enum.IsDefined(enumType, value))
                throw new ArgumentException($"No enum value found for integer: {value}");

            var enumValue = Enum.ToObject(enumType, value);
            FieldInfo field = enumType.GetField(enumValue.ToString()!)!;

            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
            return attribute?.Description ?? enumValue.ToString()!;
        }
    }
}
