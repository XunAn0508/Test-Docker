using Abp.Application.Services;
using Abp.UI;
using CompanyWebsite.Careers.Dto;
using CompanyWebsite.Careers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyWebsite.Applicants.Dto;
using Microsoft.AspNetCore.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CompanyWebsite.Documents;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using CompanyWebsite.SendEmail;

namespace CompanyWebsite.Applicants
{
    [AllowAnonymous]
    public class ApplicantAppService : CompanyWebsiteAppServiceBase, IApplicationService // TanahMuAppServiceBase, 
    {
        private readonly ApplicantManager _applicantManager;
        private readonly DocumentManager _documentManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailQueuesManager _emailQueuesManager;

        public ApplicantAppService(
            ApplicantManager applicantManager,
            DocumentManager documentManager,
            IHttpContextAccessor httpContextAccessor,
            EmailQueuesManager emailQueuesManager
            )
        {
            _applicantManager = applicantManager;
            _documentManager = documentManager;
            _httpContextAccessor = httpContextAccessor;
            _emailQueuesManager = emailQueuesManager;
        }

        // GET
        [DontWrapResult]
        public async Task<List<ApplicantDto>> GetAllApplicant()
        {
            List<Applicant> applicants = await _applicantManager.GetAllApplicantAsync();

            var applicantDtos = ObjectMapper.Map<List<ApplicantDto>>(applicants);
            return new List<ApplicantDto>(applicantDtos);
        }

        [DontWrapResult]
        public async Task<ApplicantDto> GetApplicant(int applicantId)
        {

            Applicant applicant = await _applicantManager.GetApplicantAsync(applicantId);
            var applicantDto = ObjectMapper.Map<ApplicantDto>(applicant);
            return applicantDto;
        }

        // ADD
        [HttpPost]
        public async Task<ApplicantDto> AddApplicant()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var inputJson = httpContext.Request.Form["input"];
            var input = JsonConvert.DeserializeObject<ApplicantDto>(inputJson);

            var files = httpContext.Request.Form.Files;

            //Applicant a = ObjectMapper.Map<Applicant>(input);
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

            Applicant newApplicant = await _applicantManager.AddApplicantAsync(a);

            var newDocument = await _documentManager.CreateDocumentsAsync(newApplicant.Id, "Applicant", files);
            EmailQueues newEmailQueues = await _emailQueuesManager.AddCareerEmailQueuesAsync(a, "Applicant", files);

            return ObjectMapper.Map<ApplicantDto>(newApplicant);
        }

        [HttpPost]
        public async Task<ContactUsEmailQueuesDto> AddSendMessage()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var inputJson = httpContext.Request.Form["input"];
            var input = JsonConvert.DeserializeObject<ContactUsEmailQueuesDto>(inputJson);

            ContactUsEmailQueues contactUsEmailQueues = new ContactUsEmailQueues();

            contactUsEmailQueues.Name = input.Name;
            contactUsEmailQueues.Email = input.Email;
            contactUsEmailQueues.Subject = input.Subject;
            contactUsEmailQueues.Message = input.Message;

            EmailQueues newEmailQueues = await _emailQueuesManager.AddContactUsEmailQueuesAsync(contactUsEmailQueues);

            return ObjectMapper.Map<ContactUsEmailQueuesDto>(input);
        }

        // UPDATE
        public async Task<ApplicantDto> UpdateApplicant(int applicantId, ApplicantDto input)
        {
            Applicant a = ObjectMapper.Map<Applicant>(input);
            //Applicant a = new Applicant();
            if (a == null)
            {
                throw new UserFriendlyException("Applicant not found, may be already deleted?");
            }

            //a.CareerId = input.CareerId;
            //a.Gender = input.Gender;
            //a.Email = input.Email;
            //a.PhoneNo = input.PhoneNo;
            ////a.Resume = input.Resume;
            ////a.Transcript = input.Transcript;
            //a.Status = input.Status;
            ////a.StartDate = input.StartDate;
            ////a.EndDate = input.EndDate;
            //a.UpdatedAt = DateTime.Now;

            Applicant result = await _applicantManager.UpdateApplicantAsync(applicantId, a);
            ApplicantDto adto = ObjectMapper.Map<ApplicantDto>(result);
            return adto;
        }

        // DELETE
        public async Task DeleteApplicantSoft(int applicantId)
        {
            await _applicantManager.DeleteApplicantSoft(applicantId);
            return;
        }

        public async Task DeleteApplicantHard(int applicantId)
        {
            await _applicantManager.DeleteApplicantHard(applicantId);
            return;
        }
    }
}
