using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class LinkToProfessionalViewModel
    {
        public User CareRecipient { get; set; }
        public List<User> ProfessionalList { get; set; }
        public User Professional { get; set; }

        public LinkToProfessionalViewModel(User careRecipient, List<User> professionalList)
        {
            CareRecipient = careRecipient;
            ProfessionalList = professionalList;
        }

        public LinkToProfessionalViewModel(User careRecipient, List<User> professionalList, User professional)
        {
            CareRecipient = careRecipient;
            ProfessionalList = professionalList;
            Professional = professional;
        }

        public LinkToProfessionalViewModel()
        {
            
        }
    }
}
