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
using Nucleus.API.Validators;

namespace Nucleus.API.Controllers
{
    [Route("api/[controller]")]
    public class AchievementsController: Controller
    {
        private IAchievementsRepository _repository;

        public AchievementsController(IAchievementsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetAchievements([FromQuery] bool includeCategory = false)
        {
            var achievemements = _repository.GetAll(includeCategory);

            var result = Mapper.Map<IEnumerable<AchievementDto>>(achievemements);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAchievement")]
        public IActionResult GetAchievement(int id)
        {
            var achievement = _repository.GetOne(id);

            if(achievement == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<AchievementDto>(achievement);

            return Ok(result);
        }

        [HttpPost()]
        public IActionResult CreateAchievement([FromBody] AchievementForCreationDto achievement)
        {
            if(achievement == null)
            {
                return BadRequest();
            }

            var achievementToCreate = Mapper.Map<Achievement>(achievement);

            TryValidateModel(achievementToCreate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(achievementToCreate);

            if(!_repository.SaveChanges())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetAchievement", new { id = achievementToCreate.Id }, achievementToCreate);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAchievement(int id, [FromBody] AchievementForUpdateDto achievement)
        {
            if(achievement == null)
            {
                return BadRequest();
            }

            var achievementEntity = _repository.GetOne(id);

            if(achievementEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(achievement, achievementEntity);

            TryValidateModel(achievementEntity);

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
        public IActionResult PartiallyUpdateAchievement(int id, [FromBody] JsonPatchDocument<AchievementForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var AchievementEntity = _repository.GetOne(id);

            if(AchievementEntity == null)
            {
                return NotFound();
            }

            var achievementToPatch = Mapper.Map<AchievementForUpdateDto>(AchievementEntity);

            patchDoc.ApplyTo(achievementToPatch, ModelState);

            // check if patch was properly applied (it doesn't checks if model is correct!)
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(achievementToPatch, AchievementEntity);

            TryValidateModel(AchievementEntity);

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
    }
}
