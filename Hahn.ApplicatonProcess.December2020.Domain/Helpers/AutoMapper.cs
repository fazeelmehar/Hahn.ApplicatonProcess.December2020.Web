using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ApplicantModel, Data.Entities.Applicant>();
            CreateMap<Data.Entities.Applicant, ApplicantModel>();
        }
    }
}
