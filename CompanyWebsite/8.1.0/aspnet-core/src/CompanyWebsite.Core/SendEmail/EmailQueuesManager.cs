using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.UI;
using Abp.Zero.EntityFrameworkCore;
using CompanyWebsite.Applicants;
using CompanyWebsite.Authorization;
using CompanyWebsite.Careers;
using CompanyWebsite.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Abp.Net.Mail.EmailSettingNames;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;


namespace CompanyWebsite.SendEmail
{

    public class EmailQueuesManager : IDomainService
    {
        //BigDataContext
        private readonly IRepository<EmailQueues, int> _emailQueuesRepo;
        private readonly IRepository<Career, int> _careerRepo;
        //Utility
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ISettingManager _settingManager;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public EmailQueuesManager(
            IRepository<EmailQueues, int> emailQueuesRepo, IRepository<Career, int> careerRepo,
            IUnitOfWorkManager unitOfWorkManager, IWebHostEnvironment hostEnvironment, ISettingManager settingManager,
            IHttpContextAccessor httpContextAccessor
)
        {
            _emailQueuesRepo = emailQueuesRepo;
            _unitOfWorkManager = unitOfWorkManager;
            _hostEnvironment = hostEnvironment;
            _careerRepo = careerRepo;
            _settingManager = settingManager;
            _httpContextAccessor = httpContextAccessor;

        }


        // ADD
        public async Task<EmailQueues> AddCareerEmailQueuesAsync(Applicant input, string refTable, IFormFileCollection files)
        {

            string AttachmentsPath = string.Join(";", files.Select(file =>
            {
                string folderName = $"Documents\\{refTable}";
                string assetPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot");
                string newPath = Path.Combine(assetPath, folderName);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                string uniqueFileName = $"{DateTime.Now.Ticks}_{FormatFileName(file.FileName)}";
                string fullPath = Path.Combine(newPath, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return fullPath;
            }));

            var alert = "";

            if (string.IsNullOrEmpty(AttachmentsPath))
            {
                alert = "";
            }
            else if (!AttachmentsPath.Contains(";"))
            {
                alert = "<p><b><span style=\"color: red;\">*</span></b>The applicant has attached a file for your review below.</p><br />";
            }
            else
            {
                alert = "<p><b><span style=\"color: red;\">*</span></b>The applicant has attached files for your review below.</p><br />";
            }

            //1.Web 2.Mobile 3.Intern
            var subject = await _careerRepo.GetAll().Where(x => x.Id == input.CareerId).FirstOrDefaultAsync();
            string FilePath = Path.Combine(_hostEnvironment.ContentRootPath + "\\App_Data", "EmailTemplate.html");
            StreamReader str = new StreamReader(FilePath);
            string contentTemplate = str.ReadToEnd();
            string content = "";
            string title = "";

            title += String.Format("Job Apply - {0}", subject.Title);

            content += String.Format("<p style=\"color: black;  font-weight:bolder;\">\r\n Dear 8DGE,\r\n</p>" +
                "<p>We are pleased to inform you that we have received a new job application for an <b>{0}</b>" +
                " position at our company.</p>\r\n<br /><p><b>Applicant Details:</b></p>\r\n" +
                " <ul>\r\n<li>Name: {1}</li>\r\n<li>Email Address: {2}</li>\r\n<li>Job Applied for: {3}</li>\r\n</ul>" +
                "{4}" +
                "<p style=\"text-decoration:underline\">The applicant's message includes the following:</p>\r\n" +
                "<p style=\"white-space: pre-line; text-align: justify;\">\r\n{5}\r\n</p>"
                , subject.Title, input.Name, input.Email, subject.Title, alert, input.Message);


            contentTemplate = contentTemplate.Replace("<!--EnterTitle-->", title).Replace("<!--EnterContent-->", content);

            var DefaultFromAddress = _settingManager.GetSettingValue("Abp.Net.Mail.DefaultFromAddress");
            var DefaultFromDisplayName = _settingManager.GetSettingValue("Abp.Net.Mail.DefaultFromDisplayName");
            var Host = _settingManager.GetSettingValue("Abp.Net.Mail.Smtp.Host");
            var UserName = _settingManager.GetSettingValue("Abp.Net.Mail.Smtp.UserName");
            var Password = _settingManager.GetSettingValue("Abp.Net.Mail.Smtp.Password");
            var Port = int.Parse(_settingManager.GetSettingValue("Abp.Net.Mail.Smtp.Port"));
            var RecipientEmail = _settingManager.GetSettingValue("RecipientEmailAddresses");


            EmailQueues a = new EmailQueues();
            a.DefaultFromDisplayName = DefaultFromDisplayName;
            a.DefaultFromAddress = DefaultFromAddress;
            //a.RecipientEmailAddresses = RecipientEmail;
            //a.RecipientEmailAddresses = "chu.tan@8dge.com.my";
            a.RecipientEmailAddresses = RecipientEmail;


            a.Subject = "Job Application: " + subject.Title + " - Inquiry from " + input.Name;
            a.Body = contentTemplate;
            a.SmtpHost = Host;
            a.SmtpUserName = UserName;
            a.SmtpPassword = Password;
            a.SmtpPort = Port;
            a.AttachmentPaths = AttachmentsPath;



            EmailQueues newEmailQueues = await _emailQueuesRepo.InsertAsync(a);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            return newEmailQueues;
            //return newApplicant;
        }
        public async Task<EmailQueues> XXX(ContactUsEmailQueues input)
        {

            string FilePath = Path.Combine(_hostEnvironment.ContentRootPath + "\\App_Data", "EmailTemplate.html");
            StreamReader str = new StreamReader(FilePath);
            string contentTemplate = str.ReadToEnd();
            string content = "";
            string title = "";

            title += String.Format("New Contact Us Inquiry From - {0}", input.Name);
            content += String.Format(" <p>\r\n<b>Name :</b> {0}</p> <p>\r\n<b>Email :</b> {1}\r\n</p> " +
                "<p>\r\n<b>Subject : </b>{2}\r\n</p> <p style=\"white-space: pre-line; text-align: justify;\">\r\n{3}\r\n</p>"
                , input.Name, input.Email, input.Subject, input.Message);
            contentTemplate = contentTemplate.Replace("<!--EnterTitle-->", title).Replace("<!--EnterContent-->", content);

            var DefaultFromAddress = _settingManager.GetSettingValue("Abp.Net.Mail.DefaultFromAddress");
            var DefaultFromDisplayName = _settingManager.GetSettingValue("Abp.Net.Mail.DefaultFromDisplayName");
            var Host = _settingManager.GetSettingValue("Abp.Net.Mail.Smtp.Host");
            var UserName = _settingManager.GetSettingValue("Abp.Net.Mail.Smtp.UserName");
            var Password = _settingManager.GetSettingValue("Abp.Net.Mail.Smtp.Password");
            var Port = int.Parse(_settingManager.GetSettingValue("Abp.Net.Mail.Smtp.Port"));
            var RecipientEmail = _settingManager.GetSettingValue("RecipientEmailAddresses");



            EmailQueues a = new EmailQueues();
            a.DefaultFromDisplayName = DefaultFromDisplayName;
            a.DefaultFromAddress = DefaultFromAddress;
            //a.RecipientEmailAddresses = RecipientEmail;
            //a.RecipientEmailAddresses = "chu.tan@8dge.com.my";
            a.RecipientEmailAddresses = RecipientEmail;


            a.Subject = title;
            a.Body = contentTemplate;
            a.SmtpHost = Host;
            a.SmtpUserName = UserName;
            a.SmtpPassword = Password;
            a.SmtpPort = Port;
            a.AttachmentPaths = null;



            EmailQueues newEmailQueues = await _emailQueuesRepo.InsertAsync(a);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            return newEmailQueues;
            //return newApplicant;
        }

        public static (string fullPath, string shortPath) SaveDocument(IWebHostEnvironment host, string folderName, string filename)
        {
            if (host == null || host.ContentRootPath == null)
            {
                throw new Exception("Host or content root path is null.");
            }
            string assetPath = host.ContentRootPath + "\\wwwroot";
            string newPath = Path.Combine(assetPath, folderName);

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            string uniqueFileName = $"{DateTime.Now.Ticks}_{FormatFileName(filename)}";
            string fullPath = Path.Combine(newPath, uniqueFileName);
            string shortPath = Path.Combine(folderName, uniqueFileName);

            return (fullPath, shortPath);
        }
        public static string FormatFileName(string filename)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            // Add the degree symbol to the list of valid characters
            regexSearch = regexSearch.Replace("\\", "").Replace("]", "").Insert(0, "°");
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));

            filename = r.Replace(filename, "_");
            filename = Regex.Replace(filename.Trim(), "[^A-Za-z0-9-.° ]+", "");
            filename = Regex.Replace(filename, @"\s+", " ").Trim();

            return filename;
        }

        public static string GetDomain(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
    }
}
