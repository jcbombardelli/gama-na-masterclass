using MasterclassDapper.Db;

namespace MasterclassDapper.Repositories
{

  public abstract class BaseRepository<T> 
  {
        private string _connectionString;
        protected string ConnectionString => _connectionString;
        public BaseRepository(IConfiguration configuration){
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            Seed.CreateDb(configuration);
            Seed.SeedDb(configuration);
        }
        public abstract void Add(T item);
        public abstract void Remove(int id);
        public abstract T FindByID(int id);
        public abstract void Update(T item);
        public abstract IEnumerable<T> FindAll();
    }
}