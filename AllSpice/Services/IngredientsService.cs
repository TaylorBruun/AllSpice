using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class IngredientsService
    {


        private readonly IngredientsRepository _repo;

        public IngredientsService(IngredientsRepository repo)
        {
            _repo = repo;
        }

        internal Ingredient GetById(int id)
        {
            Ingredient found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }

            return found;
        }


        internal Ingredient Create(Ingredient ingredientData)
        {
            return _repo.Create(ingredientData);
        }

        internal Ingredient Update(Ingredient updatedIngredientData)
        {
            Ingredient found = _repo.GetById(updatedIngredientData.Id);
            found.Name = updatedIngredientData.Name ?? found.Name;
            found.Quantity = updatedIngredientData.Quantity ?? found.Quantity;
            _repo.Update(found);
            return found;
        }

        internal void Delete(int id)
        {
            _repo.Delete(id);
        }


        internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
        {
            List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
            return ingredients;
        }

    }
}