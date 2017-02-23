using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nucleus.API.Models;

namespace Nucleus.API
{
    public class AchievementsDataStore
    {
        public static AchievementsDataStore Current { get; } = new AchievementsDataStore();

        public List<AchievementDto> Achievements { get; set; }

        public AchievementsDataStore()
        {
            // init dummy data

            Achievements = new List<AchievementDto>()
            {
                new AchievementDto()
                {
                    Id = 1,
                    Name = "Test1",
                    Description = "Description of Test1 achiv"
                },
                new AchievementDto()
                {
                    Id = 2,
                    Name = "Test2",
                    Description = "Very hard to get"
                },
                new AchievementDto()
                {
                    Id = 3,
                    Name = "Test3",
                    Description = "Really cool achiv"
                }
            };
        }
    }
}
