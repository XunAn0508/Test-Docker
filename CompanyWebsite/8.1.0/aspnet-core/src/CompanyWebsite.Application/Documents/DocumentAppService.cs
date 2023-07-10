using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Abp.Web.Models;
using AutoMapper.Internal.Mappers;
using CompanyWebsite.Careers;
using CompanyWebsite.Careers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyWebsite.Documents
{
    [AllowAnonymous]
    public class DocumentAppService : CompanyWebsiteAppServiceBase, IApplicationService
    {
        private readonly DocumentManager _DocumentManager;

        public DocumentAppService(
            DocumentManager DocumentManager
            )
        {
            _DocumentManager = DocumentManager;
        }

        // GET
        //[DontWrapResult]


        // ADD


        // UPDATE


        // DELETE

    }
}
