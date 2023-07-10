using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyWebsite.Careers
{
    public class CareerManager : IDomainService
    {
        //BigDataContext
        private readonly IRepository<Career, int> _careerRepo;

        //Utility
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CareerManager(
            IRepository<Career, int> careerRepo,
            IUnitOfWorkManager unitOfWorkManager
            ) 
        {
            _careerRepo = careerRepo;
            _unitOfWorkManager = unitOfWorkManager;
        }

        // GET
        public async Task<List<Career>> GetAllCareerAsync()
        {
            var careers = await _careerRepo.GetAll().Where(c => c.DeletedAt == null).ToListAsync();

            return careers;
        }


        public async Task<Career> GetCareerAsync(int careerId)
        {

            var career = await _careerRepo.GetAll().Where(c => c.Id == careerId && c.DeletedAt == null).FirstOrDefaultAsync();

            if (career == null)
            {
                throw new UserFriendlyException("Career not found or already deleted.");
            }

            return career;
        }

        // ADD
        public async Task<Career> AddCareerAsync(Career input)
        {
            Career c = new Career();
            c.Title = input.Title;
            c.Duration = input.Duration;
            c.Description = input.Description;
            c.Qualifications = input.Qualifications;
            c.Responsibilities = input.Responsibilities;
            c.CreatedAt = DateTime.Now;

            Career newCareer = await _careerRepo.InsertAsync(c);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return newCareer;
        }

        // UPDATE
        public async Task<Career> UpdateCareerAsync(int careerId, Career input)
        {
            Career c = await this.GetCareerAsync(careerId);
            if (c == null)
            {
                throw new UserFriendlyException("Career not found, may be already deleted?");
            }

            c.Title = input.Title;
            c.Duration = input.Duration;
            c.Description = input.Description;
            c.Qualifications = input.Qualifications;
            c.Responsibilities = input.Responsibilities;
            c.UpdatedAt = DateTime.Now;

            Career result = await _careerRepo.UpdateAsync(c);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return result;
        }

        public async Task ReactivateCareer(int careerId)
        {
            Career c = await this.GetCareerAsync(careerId);
            if (c == null)
            {
                throw new UserFriendlyException("Career not found, may be already deleted?");
            }

            c.DeletedAt = null; // Set DeletedAt to null to reactivate the career

            await _careerRepo.UpdateAsync(c); // Update the career in the repository
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return;
        }


        // SOFT DELETE
        public async Task DeleteCareerSoft(int careerId)
        {
            Career c = await this.GetCareerAsync(careerId);
            if (c == null)
            {
                throw new UserFriendlyException("Career not found, may be already deleted?");
            }
            // Set the DeletedAt property to the current DateTime
            c.DeletedAt = DateTime.Now;

            // Mark the entity as modified to trigger an update in the database
            _careerRepo.UpdateAsync(c);

            // Save the changes
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return;
        }

        // HARD DELETE
        public async Task DeleteCareerHard(int careerId)
        {
            Career c = await this.GetCareerAsync(careerId);
            if (c == null)
            {
                throw new UserFriendlyException("Career not found, may be already deleted?");
            }
            c.DeletedAt = DateTime.Now;
            await _careerRepo.DeleteAsync(c);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return;
        }
    }
}
