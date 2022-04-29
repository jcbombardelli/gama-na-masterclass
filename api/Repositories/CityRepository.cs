using System.Data;
using Dapper;
using MasterclassDapper.Models;
using Microsoft.Data.Sqlite;

namespace MasterclassDapper.Repositories
{
  public class CityRepository : BaseRepository<City>
  {
    public CityRepository(IConfiguration configuration) : base(configuration) { }

    public override void Add(City city)
    {
      using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
      {
        string sQuery = "INSERT INTO cities (name, uf) VALUES(@Name, @Uf)";
        dbConnection.Open();
        var result = dbConnection.Execute(sQuery, city);
        if(result > 0){
          city.Id = dbConnection.QuerySingle<int>("SELECT last_insert_rowid()");
        }
      }
    }
    public override void Remove(int id)
    {
      using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
      {
        string sQuery = "DELETE FROM cities WHERE id = @Id";
        dbConnection.Open();
        dbConnection.Execute(sQuery, new { Id = id });
      }
    }

    public override City FindByID(int id)
    {
      using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
      {
        string sQuery = "SELECT * FROM cities WHERE Id = @Id";
        dbConnection.Open();
        return dbConnection.Query<City>(sQuery, new { Id = id }).FirstOrDefault();
      }
    }
    public override IEnumerable<City> FindAll()
    {
      using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
      {
        dbConnection.Open();
        return dbConnection.Query<City>("SELECT * FROM cities");
        
      }
    }

    public override void Update(City item)
    {
      throw new NotImplementedException();
    }
  }
}