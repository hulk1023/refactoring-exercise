using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using com.sbobet.bookmaker.SB.Enum;

namespace RefactoringExercise.Exercise1
{
    public class TiCoResultCache
    {
        internal static 
            ConcurrentDictionary<TiEnumSportType,
                ConcurrentDictionary<int, 
                    ConcurrentDictionary<int, MatchResult>>> MatchResults =
                new ConcurrentDictionary<TiEnumSportType, ConcurrentDictionary<int, ConcurrentDictionary<int, MatchResult>>>();

        internal static ConcurrentDictionary<TiEnumSportType, 
                ConcurrentDictionary<int, 
                    ConcurrentDictionary<int,OutrightResult>>> OutrightResults = 
             new ConcurrentDictionary<TiEnumSportType, ConcurrentDictionary<int, ConcurrentDictionary<int, OutrightResult>>>();



        private static IEnumerable<MatchResult> GetMatchResultsForAllLeagues(ConcurrentDictionary<int,
            ConcurrentDictionary<int, MatchResult>> matchResultsBySport, DateTime startDate)
        {
            var result = new List<MatchResult>();

            var allLeagues = matchResultsBySport.Values;
            foreach (var item in allLeagues)
            {
                result.AddRange(item.Values);
            }

            return FilterBy(result,startDate);
        }

        public static IEnumerable<MatchResult> GetMatchResultsBy(TiEnumSportType sportType, int tournamentId, DateTime starDate)
        {
            var result = MatchResults.TryGetValue(sportType, out var matchResultsBySport);

            if (!result)
                return Enumerable.Empty<MatchResult>();

            if (tournamentId == 0)
            {
                return GetMatchResultsForAllLeagues(matchResultsBySport, starDate);
            }

            result = matchResultsBySport.TryGetValue(tournamentId, out var matchResultsByLeague);
            if (!result)
                return Enumerable.Empty<MatchResult>();

            var data = matchResultsByLeague.Values.ToList();

            return FilterBy(data, starDate);
        }

        private static IEnumerable<MatchResult> FilterBy(IEnumerable<MatchResult> data, DateTime startDate)
        {
            return data.Where(x => x.EventDate == startDate).ToList();
        }

        public static IEnumerable<OutrightResult> GetOutrightResultsBy(TiEnumSportType sportType, int tournamentId, DateTime startDate)
        {
            var result = OutrightResults.TryGetValue(sportType, out var outrightResultsBySport);

            if (!result)
                return Enumerable.Empty<OutrightResult>();

            if (tournamentId == 0)
            {
                return GetOutrightResultsForAllLeagues(outrightResultsBySport,startDate);
            }

            result = outrightResultsBySport.TryGetValue(tournamentId, out var outrightResultsByLeague);

            if (!result)
                return Enumerable.Empty<OutrightResult>();

            var data = outrightResultsByLeague.Values.ToList();

            return FilterBy(data, startDate);
        }

        private static IEnumerable<OutrightResult> FilterBy(IEnumerable<OutrightResult> data, DateTime startDate)
        {
            return data.Where(x => x.EventDate == startDate).ToList();
        }


        private static IEnumerable<OutrightResult> GetOutrightResultsForAllLeagues(ConcurrentDictionary<int,
            ConcurrentDictionary<int, OutrightResult>> outrightResultsBySport, DateTime startDate)
        {
            var result = new List<OutrightResult>();

            var allLeagues = outrightResultsBySport.Values;
            foreach (var item in allLeagues)
            {
                result.AddRange(item.Values);
            }
            return FilterBy(result,startDate);
        }


    }
}
