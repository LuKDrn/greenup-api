using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Models.Associations
{
    public class AssociationsViewModel
    {
        public ICollection<OneAssociationViewModel> Associations { get; set; }
    }
}
