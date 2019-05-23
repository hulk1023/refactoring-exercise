using System;
using System.Collections.Generic;
using System.Linq;
using com.sbobet.sports.CacheObject;

namespace com.sbobet.sports.Respositories
{
    public class ResultRepo : IResultRepo
    {
        public (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) GetAll()
        {
            var data = (matchResults : new List<MatchResult>() , outrightResult: new List<OutrightResult>());

            RepoFactory.SportsBookDb.QueryMultiple(
                ds =>
                {
                    var match = ds.Read<MatchResult>().ToList();
                    var matchhistory = ds.Read<MatchResult>().ToList();

                    data.matchResults.AddRange(match);
                    data.matchResults.AddRange(matchhistory);

                    var outright = ds.Read<OutrightResult>().ToList();
                    var outrighthistory = ds.Read<OutrightResult>().ToList();

                    data.outrightResult.AddRange(outright);
                    data.outrightResult.AddRange(outrighthistory);
                }
                , "[dbo].[Nike_SB_Result_GetMatchResultInit_13.09]");
            return data;
        }

        public (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) GetUpdate(DateTime lastModOnMR, DateTime lastModOnOR)
        {
            var data = (matchResults: new List<MatchResult>(), outrightResult: new List<OutrightResult>());

            RepoFactory.SportsBookDb.QueryMultiple(
                ds =>
                {
                    var match = ds.Read<MatchResult>().ToList();
                    ds.Read<int>().FirstOrDefault();
                    data.matchResults.AddRange(match);

                    var outright = ds.Read<OutrightResult>().ToList();
                    data.outrightResult.AddRange(outright);
                }
                , "[dbo].[Nike_SB_Result_GetMatchResultByLastModOn_13.09]",
                new { lastModOnMR , lastModOnOR }
                );
            return data;

        }
    }
}