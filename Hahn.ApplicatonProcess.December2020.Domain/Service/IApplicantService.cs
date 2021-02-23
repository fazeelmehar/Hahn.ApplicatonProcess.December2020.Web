using Hahn.ApplicatonProcess.December2020.Domain.DomainModel;
using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Service
{
    public interface IApplicantService
    {
        Task<EntityResponseModel<ApplicantModel>> Insert(ApplicantModel model);
        Task<EntityResponseModel<ApplicantModel>> Update(ApplicantModel model);
        Task<EntityResponseModel<ApplicantModel>> Delete(int Id);
        Task<EntityResponseModel<ApplicantModel>> GetById(int Id);
        Task<EntityResponseListModel<ApplicantModel>> GetAll();
    }
}
