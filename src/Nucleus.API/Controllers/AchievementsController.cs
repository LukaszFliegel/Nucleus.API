using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nucleus.API.Controllers
{
    [Route("api/[controller]")]
    public class AchievementsController: Controller
    {
        [HttpGet()]
        public IActionResult GetAchievements()
        {
            return Ok(AchievementsDataStore.Current.Achievements);
        }

        [HttpGet("{id}")]
        public IActionResult GetAchievement(int id)
        {
            var achivement = AchievementsDataStore.Current.Achievements.FirstOrDefault(p => p.Id == id);

            if (achivement == null)
            {
                return NotFound();
            }

            return Ok(achivement);
        }
    }
}
