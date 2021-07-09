﻿using GreenUp.Core.Business.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.EntityFrameworkCore.Data.Seed
{
    public class DbInitializer
    {
        public static void Initialize(GreenUpContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new Role[]
                {
                    new Role { Value = "User"},
                    new Role { Value = "Association"},
                    new Role { Value = "Company"},
                };
                foreach (var role in roles)
                {
                    context.Roles.Add(role);
                }
                context.SaveChanges();
            }
        }
    }
}