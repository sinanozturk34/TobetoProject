﻿using AutoMapper;
using Business.Dtos.Requests.CourseCompanyRequests;
using Business.Dtos.Responses.CourseCompanyResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class CourseCompanyProfile : Profile
    {
        public CourseCompanyProfile()

        {
            CreateMap<CreateCourseCompanyRequest, CourseCompany>();
            CreateMap<CourseCompany, CreatedCourseCompanyResponse>();


            CreateMap<CourseCompany, GetListCourseCompanyResponse>()
                .ForMember(i => i.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ForMember(i => i.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ReverseMap();
            CreateMap<Paginate<CourseCompany>, Paginate<GetListCourseCompanyResponse>>();

            CreateMap<UpdateCourseCompanyRequest, CourseCompany>().ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<CourseCompany, UpdatedCourseCompanyResponse>();
        }
    }
}
