using System;
using System.ComponentModel;
using com.sbobet.bookmaker.Common.Enum;
using com.sbobet.bookmaker.SB.Enum;

namespace RefactoringExercise.Exercise2
{
    public static class TypeMappingExtension
    {
        public static bool IsStatus(this int source, TiEnumCustomerStatus status)
        {
            var sourceStatus = (TiEnumCustomerStatus) source;
            return (sourceStatus & status) == status;
        }




        public static TiEnumDisplayType ToDispayType(this TiEnumMarketType marketType)
        {
            return (TiEnumDisplayType) (int) marketType;
        }

        public static TiEnumMarketType ToMarketType(this TiEnumMarketDisplayType marketType)
        {
            return (TiEnumMarketType) (int) marketType;
        }

        public static T TryParse<T>(this object value)
        {
            T returnValue;

            if (value is T variable)
                returnValue = variable;
            else
                try
                {
                    //Handling Nullable types i.e, int?, double?, bool? .. etc
                    if (Nullable.GetUnderlyingType(typeof(T)) != null)
                    {
                        var conv = TypeDescriptor.GetConverter(typeof(T));
                        returnValue = (T) conv.ConvertFrom(value);
                    }
                    else
                    {
                        returnValue = (T) Convert.ChangeType(value, typeof(T));
                    }
                }
                catch (Exception)
                {
                    returnValue = default(T);
                }

            return returnValue;
        }
    }
}