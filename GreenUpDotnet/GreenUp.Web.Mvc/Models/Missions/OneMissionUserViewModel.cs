using GreenUp.Web.Mvc.Models.Adresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Models.Missions
{
    public class OneMissionUserViewModel
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public OneAdressViewModel Adress { get; set; }
        public string DateInscription { get; set; }
    }
}
