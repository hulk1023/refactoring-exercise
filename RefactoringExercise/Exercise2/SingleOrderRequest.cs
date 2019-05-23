using com.sbobet.bookmaker.SB.Enum;

namespace RefactoringExercise.Exercise2
{
    public class SingleOrderRequest 
    {
        public string ServerId { get; set; }

        //customer
        public int CustomerId { get; set; }
        public string AccountId { get; set; } //username
        public int SimulatorId { get; set; }
        public string SimulatorName { get; set; }

        //event
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public TiEnumSportType SportType { get; set; }
        public int MatchId { get; set; }
        public int MatchresultId { get; set; }
        public int LiveHomeScore { get; set; }
        public int LiveAwayScore { get; set; }

        //odds
        public int OddsId { get; set; }
        public TiEnumMarketType MarketType { get; set; }
        public int MarketGroupId { get; set; }
        public bool IsLive { get; set; }
        public decimal Price { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Point { get; set; }
        public decimal OddsMaxBet { get; set; }
        public decimal Risk { get; set; }

        //bet
        public string CookieIdentity { get; set; }
        public TiEnumOddsOption Option { get; set; }
        public TiEnumPriceStyle PriceStyle { get; set; }
        public string IP { get; set; }
        public string CountryCode { get; set; }
        public decimal Stake { get; set; }
        public string HttpReferral { get; set; }
        public string BetCondition { get; set; }
        public byte Betpage { get; set; }
        public bool AcceptAnyOdds { get; set; }
        public long ExtRefId { get; set; }
        public string TransDesc { get; set; }

        //freebet
        public long VoucherId { get; set; }
    }
}