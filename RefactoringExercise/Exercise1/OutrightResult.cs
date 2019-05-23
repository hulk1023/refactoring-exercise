using System;

namespace RefactoringExercise.Exercise1
{
    public class OutrightResult
    {
        public int Orid { get; set; }

        public int LeagueId { get; set; }

        public int Teamid { get; set; }

        public DateTime EventDate { get; set; }

        public string OddsStatus { get; set; }
        public string WinlostStatus { get; set; }
        public string MatchCode { get; set; }

        public int SportId { get; set; }

        public int SettlementStatus { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}
