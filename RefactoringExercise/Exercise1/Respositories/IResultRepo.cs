using System;
using System.Collections.Generic;
using com.sbobet.sports.CacheObject;

namespace com.sbobet.sports.Respositories
{
    public interface IResultRepo
    {
        (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) GetAll();
        (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) GetUpdate(DateTime lastModOnMR, DateTime lastModOnOR);

    }
}
