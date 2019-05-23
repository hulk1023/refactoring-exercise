using System;
using com.sbobet.bookmaker.SB.Enum;

namespace RefactoringExercise.Exercise2
{
    public class SingleOrder : Order
    {

        public string TransDesc { get; set; }

        public int TournamentId { get; set; }

        public string TournamentName { get; set; }

        public TiEnumSportType SportType { get; set; }

        public int MatchId { get; set; }


        public int LiveHomeScore { get; set; }

        public int LiveAwayScore { get; set; }

        //odds

        public TiEnumMarketType MarketType { get; set; }

        public int MarketGroupId { get; set; }

        public decimal ActualOdds { get; set; }

        public decimal Point { get; set; }

        public decimal PriceMaxBet { get; set; }

        public decimal Risk { get; set; }

        //bet

        public bool AcceptBetterOdds { get; set; }

        public decimal Hdp1 => GetHdp().hdp1;

        public decimal Hdp2 => GetHdp().hdp2;


        public string CountryCode { get; set; }


        public DateTime DateTime { get; set; }

        public string BetFrom { get; set; }


        public int OddsStyle { get; set; }

        public byte BetPage { get; set; }

        public long ExtRefId { get; set; }

        public bool AcceptAnyOdds { get; set; }

        public int BetStatus { get; set; }

        public string BetCondition { get; set; }

        public long VoucherId { get; set; }

        public bool IsFreeBet => VoucherId > 0;

        public bool MarketAllowFollowBet => MarketType != TiEnumMarketType.CorrectScore &&
                                            MarketType != TiEnumMarketType.FH_CorrectScore;

        public int OddsGroupSpread { get; set; }

        public void CreateSingleBet(SingleOrderRequest singleOrderRequest)
        {

            SetProperties(singleOrderRequest);

            AdjustOddsPrice();
        }


        private void AdjustOddsPrice()
        {
            if (Is5050() && OddsStyle == (int)TiEnumPriceStyle.Euro) Odds -= 1;
        }


        public void SetProperties(SingleOrderRequest request)
        {
            SimulatorId = request.SimulatorId;
            SimulatorName = request.SimulatorName;
            TournamentId = request.TournamentId;
            TournamentName = request.TournamentName;
            SportType = request.SportType;
            MatchId = request.MatchId;
            MatchResultId = request.MatchresultId;
            LiveHomeScore = request.LiveHomeScore;
            LiveAwayScore = request.LiveAwayScore;
            OddsId = request.OddsId;
            MarketType = request.MarketType;
            MarketGroupId = request.MarketGroupId;
            IsLive = request.IsLive;

            Odds = request.Price;
            ActualOdds = request.ActualPrice;

            Point = request.Point;
            PriceMaxBet = request.OddsMaxBet;
            Risk = request.Risk;
            CookieIdentity = request.CookieIdentity;
            OpType = request.Option;
            OddsStyle = (int)request.PriceStyle;
            Ip = request.IP;
            CountryCode = request.CountryCode;
            Stake = request.Stake;
            DateTime = DateTime.Now;
            HttpReferrer = request.HttpReferral;

            BetCondition = request.BetCondition;
            BetPage = request.Betpage;
            AcceptAnyOdds = request.AcceptAnyOdds;
            AcceptBetterOdds = !AcceptAnyOdds;
            ExtRefId = request.ExtRefId;
            TransDesc = request.TransDesc;
            VoucherId = request.VoucherId;
        }

        public bool Is5050(bool includingMoneyline = false)
        {
            return MarketType == TiEnumMarketType.Handicap ||
                   MarketType == TiEnumMarketType.OddEven ||
                   MarketType == TiEnumMarketType.OverUnder ||
                   MarketType == TiEnumMarketType.FH_Handicap ||
                   MarketType == TiEnumMarketType.FH_OverUnder ||
                   MarketType == TiEnumMarketType.FH_OddEven ||
                   includingMoneyline && MarketType == TiEnumMarketType.MoneyLine;
        }

        public (decimal hdp1, decimal hdp2) GetHdp()
        {
            var hdp1 = Is5050() && Point >= 0 ? Point : 0;
            var hdp2 = Is5050() && Point < 0 ? Math.Abs(Point) : 0;
            if (MarketType == TiEnumMarketType.CorrectScore ||
                MarketType == TiEnumMarketType.FH_CorrectScore)
                switch (OpType)
                {
                    case TiEnumOddsOption.CS_0_0:
                        hdp1 = 0;
                        hdp2 = 0;
                        break;
                    case TiEnumOddsOption.CS_0_1:
                        hdp1 = 0;
                        hdp2 = 1;
                        break;
                    case TiEnumOddsOption.CS_0_2:
                        hdp1 = 0;
                        hdp2 = 2;
                        break;
                    case TiEnumOddsOption.CS_0_3:
                        hdp1 = 0;
                        hdp2 = 3;
                        break;
                    case TiEnumOddsOption.CS_0_4:
                        hdp1 = 0;
                        hdp2 = 4;
                        break;
                    case TiEnumOddsOption.CS_1_0:
                        hdp1 = 1;
                        hdp2 = 0;
                        break;
                    case TiEnumOddsOption.CS_1_1:
                        hdp1 = 1;
                        hdp2 = 1;
                        break;
                    case TiEnumOddsOption.CS_1_2:
                        hdp1 = 1;
                        hdp2 = 2;
                        break;
                    case TiEnumOddsOption.CS_1_3:
                        hdp1 = 1;
                        hdp2 = 3;
                        break;
                    case TiEnumOddsOption.CS_1_4:
                        hdp1 = 1;
                        hdp2 = 4;
                        break;
                    case TiEnumOddsOption.CS_2_0:
                        hdp1 = 2;
                        hdp2 = 0;
                        break;
                    case TiEnumOddsOption.CS_2_1:
                        hdp1 = 2;
                        hdp2 = 1;
                        break;
                    case TiEnumOddsOption.CS_2_2:
                        hdp1 = 2;
                        hdp2 = 2;
                        break;
                    case TiEnumOddsOption.CS_2_3:
                        hdp1 = 2;
                        hdp2 = 3;
                        break;
                    case TiEnumOddsOption.CS_2_4:
                        hdp1 = 2;
                        hdp2 = 4;
                        break;
                    case TiEnumOddsOption.CS_3_0:
                        hdp1 = 3;
                        hdp2 = 0;
                        break;
                    case TiEnumOddsOption.CS_3_1:
                        hdp1 = 3;
                        hdp2 = 1;
                        break;
                    case TiEnumOddsOption.CS_3_2:
                        hdp1 = 3;
                        hdp2 = 2;
                        break;
                    case TiEnumOddsOption.CS_3_3:
                        hdp1 = 3;
                        hdp2 = 3;
                        break;
                    case TiEnumOddsOption.CS_3_4:
                        hdp1 = 3;
                        hdp2 = 4;
                        break;
                    case TiEnumOddsOption.CS_4_0:
                        hdp1 = 4;
                        hdp2 = 0;
                        break;
                    case TiEnumOddsOption.CS_4_1:
                        hdp1 = 4;
                        hdp2 = 1;
                        break;
                    case TiEnumOddsOption.CS_4_2:
                        hdp1 = 4;
                        hdp2 = 2;
                        break;
                    case TiEnumOddsOption.CS_4_3:
                        hdp1 = 4;
                        hdp2 = 3;
                        break;
                    case TiEnumOddsOption.CS_4_4:
                        hdp1 = 4;
                        hdp2 = 4;
                        break;
                    case TiEnumOddsOption.CS_HUP5:
                        hdp1 = 5;
                        hdp2 = 0;
                        break;
                    case TiEnumOddsOption.CS_AUP5:
                        hdp1 = 0;
                        hdp2 = 5;
                        break;
                    case TiEnumOddsOption.CS_Others:
                        hdp1 = 5;
                        hdp2 = 5;
                        break;
                    default:
                        throw new NotSupportedException("Invalid Hdp point");
                }

            return (hdp1: hdp1, hdp2: hdp2);
        }


        public void Validate()
        {
            ValidateDisplayOddsAcceptAnyOdds();
        }

        private void ValidateDisplayOddsAcceptAnyOdds()
        {
            var passInOdds = Odds;
            var displayOdds = TiOddsFormatter.CorrectPrice(GetDisplayPrice(TiEnumPlayerGroup.A),MarketType);

            //revert back the changes from AdjustOddsPrice()
            if (Is5050() && OddsStyle == (int)TiEnumPriceStyle.Euro)
                passInOdds += 1;

            if (AcceptAnyOdds) return;
            if (displayOdds == passInOdds) return;
            if (BetFrom == "Corvus" &&
                TiOddsHelper.CompareOdds(passInOdds, displayOdds) == OddsUpdateType.Increase)
            {
                Odds = displayOdds;
                return;
            }

            throw new Exception($"OrderService.ValidateDisplayOddsAcceptAnyOdds - Price not equal. actual price = {ActualOdds}, pass in = {passInOdds}, compute = {displayOdds} PriceStyle = {(TiEnumPriceStyle)OddsStyle}");
        }

        public decimal GetDisplayPrice(TiEnumPlayerGroup oddsDisplayUGroup)
        {
            if (Is5050())
            {
                return com.sbobet.bookmaker.SB.Helper.TiOddsHelper.ConvertPrice(ActualOdds, (TiEnumPriceStyle)OddsStyle, oddsDisplayUGroup);
            }
            if (MarketType == TiEnumMarketType.MoneyLine)
                return com.sbobet.bookmaker.SB.Helper.TiOddsHelper.ConvertPrice(ActualOdds, (TiEnumPriceStyle)OddsStyle, TiEnumPlayerGroup.A);
            return ActualOdds;
        }
    }
}