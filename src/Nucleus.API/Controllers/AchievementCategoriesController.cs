using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nucleus.API.Entities;
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

        [HttpGet()]
        public IActionResult GetCategories()
        {
            var categories = _repository.GetAll();

            var result = Mapper.Map<IEnumerable<AchievementCategoryDto>>(categories);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var category = _repository.GetOne(id);

            if(category == null)
            {
                return BadRequest();
            }

            var categoryToReturn = Mapper.Map<AchievementCategoryDto>(category);

            return Ok(categoryToReturn);
        }

        [HttpPost()]
        public IActionResult CreateCategory([FromBody] AchievementCategoryForCreationDto category)
        {
            if(category == null)
            {
                return BadRequest();
            }

            var categoryToAdd = Mapper.Map<AchievementCategory>(category);

            // custom business validation here

            _repository.Add(categoryToAdd);

            if(!_repository.SaveChanges())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetCategory", new { id = categoryToAdd.Id }, categoryToAdd);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] AchievementCategoryForUpdateDto category)
        {
            if(category == null)
            {
                return BadRequest();
            }

            var categoryEntity = _repository.GetOne(id);

            if(categoryEntity == null)
            {
                return NotFound();
            }

            Mapper.Map<AchievementCategoryForUpdateDto, AchievementCategory>(category, categoryEntity);

            TryValidateModel(categoryEntity);

            // custom logic validation here

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.SaveChanges())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateCategory(int id, [FromBody] JsonPatchDocument<AchievementCategoryForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var categoryEntity = _repository.GetOne(id);

            if(categoryEntity == null)
            {
                return NotFound();
            }

            var categoryToUpdate = Mapper.Map<AchievementCategoryForUpdateDto>(categoryEntity);

            patchDoc.ApplyTo(categoryToUpdate, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map<AchievementCategoryForUpdateDto, AchievementCategory>(categoryToUpdate, categoryEntity);

            TryValidateModel(categoryEntity);

            // custom validation here

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_repository.SaveChanges())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
