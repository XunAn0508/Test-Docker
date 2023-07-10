using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using CompanyWebsite.Applicants;
using CompanyWebsite.Careers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompanyWebsite.Documents
{
    public class DocumentManager : IDomainService
    {
        //BigDataContext
        private readonly IRepository<Document, int> _documentRepository;

        //Utility
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DocumentManager(
            IRepository<Document, int> documentRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IWebHostEnvironment hostEnvironment
            )
        {
            _documentRepository = documentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<bool> CreateDocumentsAsync(int refId, string refTable, IFormFileCollection files)
        {
            

            foreach (var file in files)
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
                    await file.CopyToAsync(stream);
                }

                Document document = new Document()
                {
                    Title = file.FileName,
                    FileName = uniqueFileName,
                    RefId = refId,
                    RefTable = refTable,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _documentRepository.InsertAsync(document);
            }

            await _unitOfWorkManager.Current.SaveChangesAsync();

            return true;
        }

        private string FormatFileName(string fileName)
        {
            // Implement your logic to format the file name if needed
            return fileName;
        }

        // GET 


        // ADD
        //public async Task<Document> AddDocumentAsync(Document input)
        //{

        //}

        // UPDATE


        // DELETE

    }
}
