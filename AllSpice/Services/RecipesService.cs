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


        internal Recipe Update(Recipe recipeData)
        {
            Recipe found = _repo.GetById(recipeData.Id);
            if (found.CreatorId != recipeData.CreatorId)
            {
                throw new Exception("You cannot edit a recipe you did not create");
            }
            found.Picture = recipeData.Picture ?? found.Picture;
            found.Title = recipeData.Title ?? found.Title;
            found.Subtitle = recipeData.Subtitle ?? found.Subtitle;
            found.Category = recipeData.Category ?? found.Category;
            _repo.Update(found);
           
            return found;
            
        }
        internal void Delete(int id, string userId)
        {
            Recipe found = _repo.GetById(id);
            if (found.CreatorId != userId)
            {
                throw new Exception("You cannot delete a recipe you did not create");
            }
            _repo.Delete(id);
        }
    }
}