using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class StepsService
    {


        private readonly StepsRepository _repo;

        public StepsService(StepsRepository repo)
        {
            _repo = repo;
        }

        internal Step GetById(int id)
        {
            Step found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }

            return found;
        }


        internal Step Create(Step stepData)
        {
            return _repo.Create(stepData);
        }

        internal Step Update(Step updatedStepData)
        {
            Step found = _repo.GetById(updatedStepData.Id);
            if (updatedStepData.Position != 0)
            {
                found.Position = updatedStepData.Position;
            }
            else
            {
                found.Position = found.Position;
            }
            found.Body = updatedStepData.Body ?? found.Body;
            _repo.Update(found);
            return found;
        }

        internal void Delete(int id)
        {
            _repo.Delete(id);
        }


        internal List<Step> GetStepsByRecipe(int recipeId)
        {
            List<Step> steps = _repo.GetStepsByRecipe(recipeId);
            return steps;
        }

    }
}