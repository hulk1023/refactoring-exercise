using System;
using com.sbobet.bookmaker.SB.Enum;

namespace RefactoringExercise.Exercise2
{
    public class TiOddsFormatter
    {
        public static decimal CorrectPrice(decimal price, TiEnumDisplayType marketDisplayType)
        {
            switch (marketDisplayType)
            {
                case TiEnumDisplayType.MoneyLine:
                case TiEnumDisplayType.TotalGoal:
                case TiEnumDisplayType.FGLG:
                    return Math.Floor(price * 100) / 100;
                case TiEnumDisplayType.Handicap:
                case TiEnumDisplayType.OverUnder:
                case TiEnumDisplayType.OddEven:
                case TiEnumDisplayType.FH_Handicap:
                case TiEnumDisplayType.FH_OverUnder:
                case TiEnumDisplayType.FH_OddEven:
                case TiEnumDisplayType._1X2:
                case TiEnumDisplayType.FH_1X2:
                case TiEnumDisplayType.LiveScore:
                case TiEnumDisplayType.FH_LiveScore:
                case TiEnumDisplayType.DoubleChance:
                case TiEnumDisplayType.Outright:
                    return Math.Floor(price * 1000) / 1000;
                case TiEnumDisplayType.CorrectScore:
                case TiEnumDisplayType.FH_CorrectScore:
                case TiEnumDisplayType.HTFT:
                default:
                    return Math.Floor(price * 100) / 100;
            }
        }

        public static decimal CorrectPrice(decimal price, TiEnumMarketType marketType)
        {
            return CorrectPrice(price, marketType.ToDispayType());
        }
    }
}