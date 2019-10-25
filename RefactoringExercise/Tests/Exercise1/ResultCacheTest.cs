using com.sbobet.bookmaker.SB.Enum;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RefactoringExercise.Exercise1;
using sg.com.titansoft.Common.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringExercise.Tests.Exercise1
{

    [TestFixture()]
    public class ResultCacheTest
    {
        private ResultProcessor _target;
        private IResultRepo _resultRepo;
        private List<MatchResult> _matchResults;
        private List<OutrightResult> _outrightResults;
        private TiBETimestamp _Ts;

        [SetUp]
        public void Setup()
        {
            //TiCoResultCache.MatchResults.Clear();
            //TiCoResultCache.OutrightResults.Clear();

            _matchResults = new List<MatchResult>()
            {
                new MatchResult() {SportId = 1, LeagueId = 1, MatchResultId = 1, EventDate = DateTime.Today},
                new MatchResult() {SportId = 1, LeagueId = 2, MatchResultId = 1, EventDate = DateTime.Today}
            };
            _outrightResults = new List<OutrightResult>()
            {
                new OutrightResult() {SportId = 1, LeagueId = 1, Orid = 1, EventDate = DateTime.Today}
            };
        }

        [Test, Order(1)]
        public void resultcache_is_insert_correctly()
        {

            GivenResultsAs((_matchResults, _outrightResults));

            WhenInsertToResultCache();

            ShouldInsertCorrectly();
        }

        [Test, Order(2)]
        public void resultcache_is_update_correctly()
        {
            GivenResultInCacheAs(_matchResults, _outrightResults);

            AndUpdateResultsChangeTo("Updated");

            WhenUpdateToResultCache();

            ShouldUpdateCorrectly("Updated");

        }

        [Test, Order(3)]
        public void resultcache_is_get_correctly()
        {
            GivenResultInCacheAs(_matchResults, _outrightResults);

            ShouldGetResultCacheCorrectly(_matchResults[0].SportId, _matchResults[0].LeagueId, _matchResults[0].EventDate);

        }

        private void ShouldGetResultCacheCorrectly(int sportId, int leagueId, DateTime eventDate)
        {
            var result = TiCoResultCache.GetMatchResultsBy((TiEnumSportType)sportId, leagueId, eventDate);
            result.Count().Should().Be(_matchResults.Count(x =>
                x.SportId == sportId && x.LeagueId == leagueId && x.EventDate == eventDate));

            var resultOr = TiCoResultCache.GetOutrightResultsBy((TiEnumSportType)sportId, leagueId, eventDate);
            resultOr.Count().Should().Be(_outrightResults.Count(x =>
                x.SportId == sportId && x.LeagueId == leagueId && x.EventDate == eventDate));

        }

        private void ShouldUpdateCorrectly(string expected)
        {

            var result = TiCoResultCache.GetMatchResultsBy((TiEnumSportType)_matchResults[0].SportId, _matchResults[0].LeagueId,
                _matchResults[0].EventDate);
            result.Single().EventStatus.Should().Be(expected);


            var resultOr = TiCoResultCache.GetOutrightResultsBy((TiEnumSportType)_outrightResults[0].SportId, _outrightResults[0].LeagueId,
                _outrightResults[0].EventDate);
            resultOr.Single().OddsStatus.Should().Be(expected);


        }

        private void WhenUpdateToResultCache()
        {
            _target.Process(_Ts);
        }

        private void AndUpdateResultsChangeTo(string changedData)
        {
            _matchResults[0].EventStatus = changedData;
            _outrightResults[0].OddsStatus = changedData;

            _resultRepo.GetUpdate(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns((_matchResults.ToList(), _outrightResults.ToList()));

        }

        private void GivenResultInCacheAs(List<MatchResult> matchResults, List<OutrightResult> outrightResults)
        {
            GivenResultsAs((matchResults, outrightResults));

            WhenInsertToResultCache();
        }


        private void ShouldInsertCorrectly()
        {
            var result = TiCoResultCache.GetMatchResultsBy((TiEnumSportType)_matchResults[0].SportId, 1,
                _matchResults[0].EventDate);

            result.Count().Should().Be(_matchResults.Count(x => x.SportId == 1 && x.LeagueId == 1 && x.MatchResultId == 1));


            var resultOr = TiCoResultCache.GetOutrightResultsBy((TiEnumSportType)_outrightResults[0].SportId, 1,
                _outrightResults[0].EventDate);

            resultOr.Count().Should().Be(_outrightResults.Count(x => x.SportId == 1 && x.LeagueId == 1 && x.Orid == 1));
        }

        private void WhenInsertToResultCache()
        {
            _target = new ResultProcessor(_resultRepo);
            _Ts = _target.Process(null);
        }

        private void GivenResultsAs((List<MatchResult>, List<OutrightResult>) resultData)
        {
            _resultRepo = Substitute.For<IResultRepo>();
            _resultRepo.GetAll().Returns(
                resultData
            );
        }

        [TearDown]
        public void CleanUp()
        {
        }

    }
}