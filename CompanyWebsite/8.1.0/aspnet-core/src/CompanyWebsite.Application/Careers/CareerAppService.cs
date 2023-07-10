using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Abp.Web.Models;
using AutoMapper.Internal.Mappers;
using CompanyWebsite.Careers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyWebsite.Careers
{
    [AllowAnonymous]
    public class CareerAppService : CompanyWebsiteAppServiceBase, IApplicationService
    {
        private readonly CareerManager _careerManager;

        public CareerAppService(
            CareerManager careerManager
            )
        {
            _careerManager = careerManager;
        }

        // GET
        [DontWrapResult]
        public async Task<List<CareerDto>> GetAllCareer()
        {
            List<Career> careers = await _careerManager.GetAllCareerAsync();

            var careerDtos = ObjectMapper.Map<List<CareerDto>>(careers);
            return new List<CareerDto>(careerDtos);
        }

        [DontWrapResult]
        public async Task<CareerDto> GetCareer(int careerId)
        {

            Career career = await _careerManager.GetCareerAsync(careerId);
            var careerDto = ObjectMapper.Map<CareerDto>(career);
            return careerDto;
        }

        // ADD
        public async Task<CareerDto> AddCareer(CareerDto input)
        {
            Career c = new Career();
            c.Title = input.Title;
            c.Duration = input.Duration;
            c.Description = input.Description;
            c.Qualifications = input.Qualifications;
            c.Responsibilities = input.Responsibilities;
            c.CreatedAt = DateTime.Now;

            Career newCareer = await _careerManager.AddCareerAsync(c);

            return ObjectMapper.Map<CareerDto>(newCareer);
        }

        // UPDATE
        public async Task<CareerDto> UpdateCareer(int careerId, CareerDto input)
        {
            Career c = new Career();
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

            Career result = await _careerManager.UpdateCareerAsync(careerId, c);
            CareerDto cdto = ObjectMapper.Map<CareerDto>(result);
            return cdto;
        }

        public async Task ReactivateCareer(int careerId)
        {
            await _careerManager.ReactivateCareer(careerId);
            return;
        }

        // DELETE
        public async Task DeleteCareerSoft(int groupId)
        {
            await _careerManager.DeleteCareerSoft(groupId);
            return;
        }

        public async Task DeleteCareerHard(int groupId)
        {
            await _careerManager.DeleteCareerHard(groupId);
            return;
        }
    }
}
