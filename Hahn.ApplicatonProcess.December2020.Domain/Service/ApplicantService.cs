using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Domain.DomainModel;
using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using Hahn.ApplicatonProcess.December2020.Domain.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Service
{
    public class ApplicantService : IApplicantService
    {
        private readonly IMapper Mapper;
        private readonly DataContext DataContext;
        public ApplicantService(IMapper _Mapper, DataContext _DataContext)
        {
            DataContext = _DataContext;
            Mapper = _Mapper;
        }
        public async Task<EntityResponseModel<ApplicantModel>> Insert(ApplicantModel model)
        {
            var response = new EntityResponseModel<ApplicantModel>();
            try
            { 
                var Validate = CheckValidation(model);
                if (!Validate.ReturnStatus)
                    return Validate;

                var applicant = Mapper.Map<Data.Entities.Applicant>(model);
                await DataContext.Applicant.AddAsync(applicant);
                DataContext.SaveChanges();
                response.Data = Mapper.Map<ApplicantModel>(applicant);
            }
            catch (Exception ex)
            {
                response.ReturnMessage.Add(ex.Message);
                response.ReturnStatus = false;
            }
            return response;
        }
        public async Task<EntityResponseModel<ApplicantModel>> GetById(int Id)
        {
            EntityResponseModel<ApplicantModel> response = new EntityResponseModel<ApplicantModel>();
            try
            {
                var applicant = await DataContext.Applicant
                    .Where(s => s.ID == Id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                response.Data = Mapper.Map<ApplicantModel>(applicant);
            }
            catch (Exception ex)
            {
                response.ReturnMessage.Add(ex.Message);
                response.ReturnStatus = false;
            }
            return response;
        }
        public async Task<EntityResponseModel<ApplicantModel>> Update(ApplicantModel model)
        {
            var response = new EntityResponseModel<ApplicantModel>();
            try
            {
                var Validate = CheckValidation(model);
                if (!Validate.ReturnStatus)
                    return Validate;

                if (!DataContext.Applicant.Any(s => s.ID == model.ID))
                    throw new Exception("Record not found");

                var applicant = Mapper.Map<Hahn.ApplicatonProcess.December2020.Data.Entities.Applicant>(model);
                applicant.ID = model.ID;

                DataContext.Applicant.Update(applicant);
                await DataContext.SaveChangesAsync();
                response.Data = Mapper.Map<ApplicantModel>(applicant);
            }
            catch (Exception ex)
            {
                response.ReturnMessage.Add(ex.Message);
                response.ReturnStatus = false;
            }
            return response;
        }
        public async Task<EntityResponseModel<ApplicantModel>> Delete(int Id)
        {
            var response = new EntityResponseModel<ApplicantModel>();
            try
            {
                var applicant = await DataContext.Applicant
                    .Where(s => s.ID == Id)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (applicant == null)
                    throw new Exception("Record not found");

                DataContext.Applicant.Remove(applicant);
                DataContext.SaveChanges();
                response.ReturnMessage.Add("Deleted Succesfully");
                response.Data = Mapper.Map<ApplicantModel>(applicant);
            }
            catch (Exception ex)
            {
                response.ReturnMessage.Add(ex.Message);
                response.ReturnStatus = false;
            }
            return response;
        }

        private EntityResponseModel<ApplicantModel> CheckValidation(ApplicantModel model)
        {
            var validate = new CheckApplicationValidation();
            var validateResult = validate.Validate(model);
            if (!validateResult.IsValid)
            {
                var errors = new List<string>();
                foreach (var validateResultError in validateResult.Errors)
                {
                    errors.Add(validateResultError.ErrorMessage);
                }
                return new EntityResponseModel<ApplicantModel>
                {
                    ReturnStatus = false,
                    Errors = errors
                };
            }
            else
                return new EntityResponseModel<ApplicantModel>();
        }

        public async Task<EntityResponseListModel<ApplicantModel>> GetAll()
        {
            EntityResponseListModel<ApplicantModel> response = new EntityResponseListModel<ApplicantModel>();
            try
            {
                var applicant = await DataContext.Applicant
                    .AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);
                response.Data = Mapper.Map<List<ApplicantModel>>(applicant);
            }
            catch (Exception ex)
            {
                response.ReturnMessage.Add(ex.Message);
                response.ReturnStatus = false;
            }
            return response;
        }
    }
}
