using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.AutoMapper
{
    public class RequestHistoryMapper : Profile
    {
        public RequestHistoryMapper()
        {
            CreateMap<RequestHistoryModel, RequestHistory>();
            CreateMap<RequestHistory, RequestHistoryModel>();
        }
    }
}
