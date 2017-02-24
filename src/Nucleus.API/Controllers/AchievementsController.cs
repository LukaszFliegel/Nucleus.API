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
    public class AchievementsController: Controller
    {
        private IAchievementsRepository _repository;

        public AchievementsController(IAchievementsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetAchievements(bool includeCategory = false)
        {
            var achievemements = _repository.GetAchievements(includeCategory);

            var result = Mapper.Map<IEnumerable<AchievementDto>>(achievemements);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAchievement")]
        public IActionResult GetAchievement(int id)
        {
            var achievement = _repository.GetAchievement(id);

            if(achievement == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<AchievementDto>(achievement);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAchievement([FromBody] AchievementForCreationDto achievement)
        {
            if(achievement == null)
            {
                return BadRequest();
            }

            var achievementToCreate = Mapper.Map<Achievement>(achievement);

            // custom business validation here

            _repository.AddAchievement(achievementToCreate);

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

            var achievementEntity = _repository.GetAchievement(id);

            if(achievementEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(achievement, achievementEntity);

            if(!_repository.SaveChanges())
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

            var AchievementEntity = _repository.GetAchievement(id);

            if(AchievementEntity == null)
            {
                return NotFound();
            }

            var achievementToPatch = Mapper.Map<AchievementForUpdateDto>(AchievementEntity);

            patchDoc.ApplyTo(achievementToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // custom business validation here

            TryValidateModel(achievementToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(achievementToPatch, AchievementEntity);

            if(!_repository.SaveChanges())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
