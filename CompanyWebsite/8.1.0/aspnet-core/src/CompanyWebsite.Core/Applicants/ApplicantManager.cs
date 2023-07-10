using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using CompanyWebsite.Careers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebsite.Applicants
{

    public class ApplicantManager : IDomainService
    {
        //BigDataContext
        private readonly IRepository<Applicant, int> _applicantRepo;

        //Utility
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ApplicantManager(
            IRepository<Applicant, int> applicantRepo,
            IUnitOfWorkManager unitOfWorkManager
            ) 
        {
            _applicantRepo = applicantRepo;
            _unitOfWorkManager = unitOfWorkManager;
        }

        // GET
        public async Task<List<Applicant>> GetAllApplicantAsync()
        {

            var applicants = await _applicantRepo.GetAll().Where(a => a.DeletedAt == null).ToListAsync();

            return applicants;
        }

        public async Task<Applicant> GetApplicantAsync(int applicantId)
        {

            var applicant = await _applicantRepo.GetAll().Where(a => a.Id == applicantId && a.DeletedAt == null).FirstOrDefaultAsync();

            if (applicant == null)
            {
                throw new UserFriendlyException("Career not found or already deleted.");
            }

            return applicant;
        }

        // ADD
        public async Task<Applicant> AddApplicantAsync(Applicant input)
        {
            Applicant a = new Applicant();
            a.CareerId = input.CareerId;
            a.Name = input.Name;
            a.Gender = input.Gender;
            a.Email = input.Email;
            a.PhoneNo = input.PhoneNo;
            a.Message = input.Message;
            //a.Resume = input.Resume;
            //a.Transcript = input.Transcript;
            //a.Status = input.Status;
            //a.StartDate = input.StartDate;
            //a.EndDate = input.EndDate;
            a.CreatedAt = DateTime.Now;

            Applicant newApplicant = await _applicantRepo.InsertAsync(a);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            // Retrieve the generated ID
            //int generatedId = newApplicant.Id;

            return newApplicant;
        }

        // UPDATE
        public async Task<Applicant> UpdateApplicantAsync(int applicantId, Applicant input)
        {
            Applicant a = await this.GetApplicantAsync(applicantId);
            if (a == null)
            {
                throw new UserFriendlyException("Applicant not found, may be already deleted?");
            }

            a.CareerId = input.CareerId;
            a.Gender = input.Gender;
            a.Email = input.Email;
            a.PhoneNo = input.PhoneNo;
            a.Message = input.Message;
            //a.Resume = input.Resume;
            //a.Transcript = input.Transcript;
            a.Status = input.Status;
            //a.StartDate = input.StartDate;
            //a.EndDate = input.EndDate;
            a.UpdatedAt = DateTime.Now;

            Applicant result = await _applicantRepo.UpdateAsync(a);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return result;
        }

        //public async Task ReactivateApplicant(int applicantId)
        //{
        //    Applicant a = await this.GetApplicantAsync(applicantId);
        //    if (a == null)
        //    {
        //        throw new UserFriendlyException("Applicant not found, may be already deleted?");
        //    }

        //    a.DeletedAt = null; // Set DeletedAt to null to reactivate the applicant

        //    await _applicantRepo.UpdateAsync(a); // Update the applicant in the repository
        //    await _unitOfWorkManager.Current.SaveChangesAsync();

        //    return;
        //}


        // SOFT DELETE
        public async Task DeleteApplicantSoft(int applicantId)
        {
            Applicant a = await this.GetApplicantAsync(applicantId);
            if (a == null)
            {
                throw new UserFriendlyException("Applicant not found, may be already deleted?");
            }
            // Set the DeletedAt property to the current DateTime
            a.DeletedAt = DateTime.Now;

            // Mark the entity as modified to trigger an update in the database
            _applicantRepo.UpdateAsync(a);

            // Save the changes
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return;
        }

        // HARD DELETE
        public async Task DeleteApplicantHard(int applicantId)
        {
            Applicant a = await this.GetApplicantAsync(applicantId);
            if (a == null)
            {
                throw new UserFriendlyException("Applicant not found, may be already deleted?");
            }
            a.DeletedAt = DateTime.Now;
            await _applicantRepo.DeleteAsync(a);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return;
        }
    }
}
