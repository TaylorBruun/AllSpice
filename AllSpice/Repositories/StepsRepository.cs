using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class StepsRepository
    {


        private readonly IDbConnection _db;

        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }


        internal Step GetById(int id)
        {
            string sql = "SELECT * FROM steps WHERE id = @id";
            return _db.QueryFirstOrDefault<Step>(sql, new { id });
        }

        internal List<Step> GetStepsByRecipe(int recipeId)
        {
            string sql = "SELECT * from steps WHERE RecipeId = @recipeId";
            return _db.Query<Step>(sql, new {recipeId}).ToList();
        }

        internal Step Create(Step stepData)
        {
            string sql = "INSERT INTO steps (id, body, position, recipeId) VALUES (@Id, @Body, @Position, @RecipeId); SELECT LAST_INSERT_ID();";

            int id = _db.ExecuteScalar<int>(sql, stepData);
            stepData.Id = id;
            return stepData;
        }
        internal void Update(Step original)
        {
            string sql = "  UPDATE steps SET body=@Body, position=@Position WHERE id = @Id LIMIT 1;";

           _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
              string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

    }
}