using sg.com.titansoft.TiDALBase.Entity;
using sg.com.titansoft.TiDALBase.Repository;

namespace RefactoringExercise.Exercise1
{
    public class Repo : TiDapperRepositoryBase<object>
    {
        private readonly string _databaseName;

        public Repo(TiDbContext dbContext, string databaseName) : base(dbContext)
        {
            _databaseName = databaseName;
        }

        public override string ToString()
        {
            return _databaseName;
        }
    }

}
