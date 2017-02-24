using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nucleus.API.Models;
using Nucleus.API.Repositories;

namespace Nucleus.API.Controllers
{
    [Route("api/[controller]")]
    public class AchievementCategoriesController: Controller
    {
        private IAchievementCategoryRepository _repository;

        public AchievementCategoriesController(IAchievementCategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _repository.GetAchievementCategories();

            var result = Mapper.Map<IEnumerable<AchievementCategoryDto>>(categories);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _repository.GetAchievementCategory(id);

            if(category == null)
            {
                return BadRequest();
            }

            var categoryToReturn = Mapper.Map<AchievementCategoryDto>(category);

            return Ok(categoryToReturn);
        }
    }
}
