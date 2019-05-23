using com.sbobet.bookmaker.SB.Enum;
using bookmaterHelper = com.sbobet.bookmaker.SB.Helper;

namespace RefactoringExercise.Exercise2
{
    public class TiOddsHelper
    {
        public static bool Is5050Market(TiEnumMarketType marketType, bool includeMoneyLine)
        {
            return bookmaterHelper.TiOddsHelper.Is5050Market(marketType, includeMoneyLine);
        }

        public static decimal ConvertPrice(decimal price, TiEnumPriceStyle priceStyle, TiEnumPlayerGroup group)
        {
            return bookmaterHelper.TiOddsHelper.ConvertPrice(price, priceStyle, group);
        }


        public static string ConvertOddsOptionType2DBString(TiEnumOddsOption oddsOptionType)
        {
            switch (oddsOptionType)
            {
                case TiEnumOddsOption.Home:
                case TiEnumOddsOption.Over:
                case TiEnumOddsOption.Odd:
                    return "h";

                case TiEnumOddsOption.Away:
                case TiEnumOddsOption.Under:
                case TiEnumOddsOption.Even:
                    return "a";

                case TiEnumOddsOption._1X2_1:
                case TiEnumOddsOption.ML_1:
                case TiEnumOddsOption.LS_1:
                    return "1";
                case TiEnumOddsOption._1X2_2:
                case TiEnumOddsOption.ML_2:
                case TiEnumOddsOption.LS_2:
                    return "2";
                case TiEnumOddsOption._1X2_X:
                case TiEnumOddsOption.LS_X:
                    return "X";

                case TiEnumOddsOption.DC_1X:
                    return "1X";

                case TiEnumOddsOption.DC_2X:
                    return "X2";

                case TiEnumOddsOption.DC_12:
                    return "12";

                case TiEnumOddsOption.CS_0_0:
                    return "0:0";
                case TiEnumOddsOption.CS_0_1:
                    return "0:1";
                case TiEnumOddsOption.CS_0_2:
                    return "0:2";
                case TiEnumOddsOption.CS_0_3:
                    return "0:3";
                case TiEnumOddsOption.CS_0_4:
                    return "0:4";
                case TiEnumOddsOption.CS_1_0:
                    return "1:0";
                case TiEnumOddsOption.CS_1_1:
                    return "1:1";
                case TiEnumOddsOption.CS_1_2:
                    return "1:2";
                case TiEnumOddsOption.CS_1_3:
                    return "1:3";
                case TiEnumOddsOption.CS_1_4:
                    return "1:4";
                case TiEnumOddsOption.CS_2_0:
                    return "2:0";
                case TiEnumOddsOption.CS_2_1:
                    return "2:1";
                case TiEnumOddsOption.CS_2_2:
                    return "2:2";
                case TiEnumOddsOption.CS_2_3:
                    return "2:3";
                case TiEnumOddsOption.CS_2_4:
                    return "2:4";
                case TiEnumOddsOption.CS_3_0:
                    return "3:0";
                case TiEnumOddsOption.CS_3_1:
                    return "3:1";
                case TiEnumOddsOption.CS_3_2:
                    return "3:2";
                case TiEnumOddsOption.CS_3_3:
                    return "3:3";
                case TiEnumOddsOption.CS_3_4:
                    return "3:4";
                case TiEnumOddsOption.CS_4_0:
                    return "4:0";
                case TiEnumOddsOption.CS_4_1:
                    return "4:1";
                case TiEnumOddsOption.CS_4_2:
                    return "4:2";
                case TiEnumOddsOption.CS_4_3:
                    return "4:3";
                case TiEnumOddsOption.CS_4_4:
                    return "4:4";
                case TiEnumOddsOption.CS_AUP5:
                    return "0:5";
                case TiEnumOddsOption.CS_HUP5:
                    return "5:0";
                case TiEnumOddsOption.CS_Others:
                    return "o";
                case TiEnumOddsOption.TG_0_1:
                    return "0-1";
                case TiEnumOddsOption.TG_2_3:
                    return "2-3";
                case TiEnumOddsOption.TG_4_6:
                    return "4-6";
                case TiEnumOddsOption.TG_7_Up:
                    return "7-over";

                case TiEnumOddsOption.HTFT_AA:
                    return "aa";
                case TiEnumOddsOption.HTFT_AD:
                    return "ad";
                case TiEnumOddsOption.HTFT_AH:
                    return "ah";
                case TiEnumOddsOption.HTFT_DA:
                    return "da";
                case TiEnumOddsOption.HTFT_DD:
                    return "dd";
                case TiEnumOddsOption.HTFT_DH:
                    return "dh";
                case TiEnumOddsOption.HTFT_HA:
                    return "ha";
                case TiEnumOddsOption.HTFT_HD:
                    return "hd";
                case TiEnumOddsOption.HTFT_HH:
                    return "hh";


                case TiEnumOddsOption.FGLG_AF:
                    return "af";
                case TiEnumOddsOption.FGLG_AL:
                    return "al";
                case TiEnumOddsOption.FGLG_HF:
                    return "hf";
                case TiEnumOddsOption.FGLG_HL:
                    return "hl";
                case TiEnumOddsOption.FGLG_NG:
                    return "ng";

                default:
                    return string.Empty;
            }
        }

        public static OddsUpdateType CompareOdds(decimal oldOdds, decimal newOdds)
        {
            if (oldOdds == newOdds) return OddsUpdateType.NoChange;
            if (oldOdds > 0)
            {
                if (newOdds > 0) return newOdds > oldOdds ? OddsUpdateType.Increase : OddsUpdateType.Decrease;
                return OddsUpdateType.Increase;
            }

            if (newOdds > 0) return OddsUpdateType.Decrease;
            return newOdds > oldOdds ? OddsUpdateType.Increase : OddsUpdateType.Decrease;
        }
    }
}