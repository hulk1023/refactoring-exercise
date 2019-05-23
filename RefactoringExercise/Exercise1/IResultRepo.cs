using System;
using System.Collections.Generic;

namespace RefactoringExercise.Exercise1
{
    public interface IResultRepo
    {
        (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) GetAll();
        (IEnumerable<MatchResult> matchResults, IEnumerable<OutrightResult> outrightResults) GetUpdate(DateTime lastModOnMR, DateTime lastModOnOR);

    }
}
