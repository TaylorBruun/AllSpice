using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class RecipesRepository
    {

        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> GetAll()
        {
            string sql = "SELECT recipes.*, accounts.* FROM recipes JOIN accounts ON recipes.creatorId = accounts.id";
            return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            }).ToList();
        }

        internal Recipe GetById(int id)
        {
            string sql = "SELECT recipes.*, accounts.* FROM recipes JOIN accounts ON recipes.creatorId = accounts.id WHERE recipes.id = @id";
            return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            }, new {id}).FirstOrDefault();
        }

        internal Recipe Create(Recipe recipeData)
        {
            string sql = "INSERT INTO recipes (id, picture, title, subtitle, category, creatorId) VALUES (@Id, @Picture, @Title, @Subtitle, @Category, @CreatorId); SELECT LAST_INSERT_ID();";

            int id = _db.ExecuteScalar<int>(sql, recipeData);
            recipeData.Id = id;
            return recipeData;
        }

        internal void Update(Recipe original)
        {
            string sql = "  UPDATE recipes SET picture=@Picture, title=@title, subtitle=@Subtitle, category=@Category WHERE id = @Id LIMIT 1;";

            _db.Execute(sql, original);

        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}