using System;

namespace RefactoringExercise.Exercise1
{
    public class MatchResult
    {
        public int MatchResultId { get; set; }

        public int MatchId { get; set; }

        public int LeagueId { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public int BetTypeGroupId { get; set; }

        public DateTime EventDate { get; set; }

        public string EventStatus { get; set; }

        public int LiveHomeScore { get; set; }

        public int LiveAwayScore { get; set; }

        public int FinalHomeScore { get; set; }

        public int FinalAwayScore { get; set; }

        public string MatchCode { get; set; }

        public DateTime? KickOffTime { get; set; }

        public string ShowTime { get; set; }

        public int HTHomeScore { get; set; }

        public int HTAwayScore { get; set; }

        public int SportId { get; set; }

        public string Result { get; set; }

        public byte OtherStatus { get; set; }

        public int OtherStatus2 { get; set; }

        public DateTime AbsKickoffTime { get; set; }

        public int ShowTimeDisplayType { get; set; }

        public DateTime LastModifiedOn { get; set; }

    }
}
