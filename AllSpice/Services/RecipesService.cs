using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class RecipesService
    {

        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal List<Recipe> GetAll()
        {
            return _repo.GetAll();
        }

        internal Recipe GetById(int id)
        {
            Recipe found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }

            return found;
        }

        internal Recipe Create(Recipe recipeData)
        {
            return _repo.Create(recipeData);
        }

        internal void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}