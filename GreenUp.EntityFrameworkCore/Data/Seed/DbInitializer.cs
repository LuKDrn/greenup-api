using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Orders.Models;
using GreenUp.Core.Business.Tags.Models;
using System.Linq;

namespace GreenUp.EntityFrameworkCore.Data.Seed
{
    public class DbInitializer
    {
        public static void Initialize(GreenUpContext context)
        {
            if (!context.Statuses.Any())
            {
                var statuses = new Status[]
                {
                    new Status { Value = "Hidden" },
                    new Status { Value = "Closed" },
                    new Status { Value = "Open" },
                    new Status { Value = "In progress" },
                    new Status { Value = "Completed" },
                    new Status { Value = "Cancelled" },
                };
                foreach (var status in statuses)
                {
                    context.Statuses.Add(status);
                }
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                var tags = new Tag[]
                {
                    new Tag{ Name = "Ecologie" },
                    new Tag{ Name = "Mission" },
                    new Tag{ Name = "Environnement" },
                    new Tag{ Name = "Earth" },
                    new Tag{ Name = "Garbage" },
                    new Tag{ Name = "Pollution" },
                };
                foreach (var tag in tags)
                {
                    context.Tags.Add(tag);
                }
                context.SaveChanges();
            }

            if (!context.Steps.Any())
            {
                var steps = new Step[]
                {
                    new Step{ Value = "Completed"},
                    new Step{ Value = "Received"},
                    new Step{ Value = "Sent"},
                    new Step{ Value = "In process"},
                    new Step{ Value = "Passed"},
                };
                foreach (var step in steps)
                {
                    context.Steps.Add(step);
                }
                context.SaveChanges();
            }
        }
    }
}
