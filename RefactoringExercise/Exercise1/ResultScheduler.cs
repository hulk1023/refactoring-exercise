using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using com.sbobet.bookmaker.SB.Enum;
using sg.com.titansoft.Base.Scheduler;
using sg.com.titansoft.Common.BusinessEntity;

namespace RefactoringExercise.Exercise1
{
    public class ResultScheduler : TiSchedulerBase
    {
        private readonly ResultProcessor _ResultProcessor = new ResultProcessor();

        internal ResultScheduler(ResultProcessor resultProcessor) : this()
        {
            _ResultProcessor = resultProcessor;
        }

        public ResultScheduler()
        {}

        public override bool Process()
        {
            var timestamp = _ResultProcessor.Process(CurrentTimestamp as TiBETimestamp);
            if (timestamp != null)
            {
                LatestTimestamp = timestamp;
            }

            return true;
        }

    }

    public class ResultProcessor
    {
        private IResultRepo ResultRepo { get; }

        public ResultProcessor(IResultRepo resultRepo)
        {
            ResultRepo = resultRepo;
        }

        public ResultProcessor()
        {
            ResultRepo = new ResultRepo();
        }

        public TiBETimestamp Process(TiBETimestamp ts)
        {
            (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) data;

            if (ts == null)
            {
                data = ResultRepo.GetAll();
                ts = new TiBETimestamp(2);
            }
            else
            {
                data = ResultRepo.GetUpdate((DateTime)ts.Items[0] , (DateTime)ts.Items[1] );
                    ts = new TiBETimestamp(ts);
            }

            if (data.matchResults != null && data.matchResults.Any())
            {
                foreach (var matchResult in data.matchResults)
                {
                    var sportType = (TiEnumSportType)matchResult.SportId;
                    if (!TiCoResultCache.MatchResults.ContainsKey(sportType))
                    {
                        TiCoResultCache.MatchResults.TryAdd(sportType, new ConcurrentDictionary<int, ConcurrentDictionary<int, MatchResult>>());
                    }
                    var matchResultsBySport = TiCoResultCache.MatchResults[sportType];

                    var leagueId = matchResult.LeagueId;

                    if (!matchResultsBySport.ContainsKey(leagueId))
                    {
                        matchResultsBySport.TryAdd(leagueId, new ConcurrentDictionary<int, MatchResult>());
                    }

                    var matchResultsByLeague = matchResultsBySport[leagueId];

                    matchResultsByLeague.AddOrUpdate(matchResult.MatchResultId, matchResult,
                                     (key, oldValue) => matchResult);
                }

                ts.Items[0] = data.matchResults.Max(x => x.LastModifiedOn);
            }

            if (data.outrightResults != null &&  data.outrightResults.Any())
            {
                foreach (var outrightResult in data.outrightResults)
                {
                    var sportType = (TiEnumSportType)outrightResult.SportId;
                    if (!TiCoResultCache.OutrightResults.ContainsKey(sportType))
                    {
                        TiCoResultCache.OutrightResults.TryAdd(sportType, new ConcurrentDictionary<int, ConcurrentDictionary<int, OutrightResult>>());
                    }
                    var outrightResultBySport = TiCoResultCache.OutrightResults[sportType];

                    var leagueId = outrightResult.LeagueId;

                    if (!outrightResultBySport.ContainsKey(leagueId))
                    {
                        outrightResultBySport.TryAdd(leagueId, new ConcurrentDictionary<int, OutrightResult>());
                    }

                    var outrightResultByLeague = outrightResultBySport[leagueId];

                    outrightResultByLeague.AddOrUpdate(outrightResult.Orid, outrightResult,
                        (key, oldValue) => outrightResult);
                }

                ts.Items[1] = data.outrightResults.Max(x => x.LastModifiedOn);
            }

            return ts;
        }
    }


}
