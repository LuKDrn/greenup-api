using Abp.Domain.Entities;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Companies.Models;
using System.Collections.Generic;

namespace GreenUp.Core.Business.Users.Models
{
    public class Role : Entity
    {
        public string Value { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Association> Assocations { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}