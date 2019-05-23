using System;
using com.sbobet.bookmaker.SB.Enum;

namespace RefactoringExercise.Exercise2
{
    public class Order
    {
//        public MemberAccount Account { get; set; }
        public int SimulatorId { get; set; }
        public string SimulatorName { get; set; }
        public string CookieIdentity { get; set; }
        public string HttpReferrer { get; set; }
        public string Ip { get; set; }
        public string LoginApiToken { get; set; }
        public decimal Stake { get; set; }
        public decimal ActualStake => Odds < 0 ? Stake * Math.Abs(Odds) : Stake;
        public decimal Odds { get; set; }
        public bool IsLive { get; set; }
        public TiEnumOddsOption OpType { get; set; }
        public int OddsId { get; set; }
        public int MatchResultId { get; set; }
    }
}