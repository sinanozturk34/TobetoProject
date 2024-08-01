﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.CountryRequests;
using Business.Dtos.Requests.CourseCategoryRequests;
using Business.Dtos.Responses.CountryResponses;
using Business.Dtos.Responses.CourseCategoryResponses;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class CourseCategoryManager : ICourseCategoryService
    {
        ICourseCategoryDal _courseCategoryDal;
        IMapper _mapper;

        public CourseCategoryManager(ICourseCategoryDal courseCategoryDal, IMapper mapper)
        {
            _courseCategoryDal = courseCategoryDal;
            _mapper = mapper;
        }

        public async Task<CreatedCourseCategoryResponse> AddAsync(CreateCourseCategoryRequest createCourseCategoryRequest)
        {
            CourseCategory courseCategory = _mapper.Map<CourseCategory>(createCourseCategoryRequest);
            CourseCategory createdCourseCategory = await _courseCategoryDal.AddAsync(courseCategory);
            CreatedCourseCategoryResponse createdCourseResponse = _mapper.Map<CreatedCourseCategoryResponse>(createdCourseCategory);
            return createdCourseResponse;
        }

        public async Task<CourseCategory> DeleteAsync(int id)
        {
            var data = await _courseCategoryDal.GetAsync(i => i.Id == id);
            var result = await _courseCategoryDal.DeleteAsync(data);
            return result;
        }

        public async Task<IPaginate<GetListCourseCategoryResponse>> GetAllAsync(PageRequest pageRequest)
        {
            var data = await _courseCategoryDal.GetListAsync(
                 include: ca => ca
                .Include(c => c.Course)
                .Include(cc => cc.Category),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
               );
            var result = _mapper.Map<Paginate<GetListCourseCategoryResponse>>(data);
            return result;
        }

        public async Task<IPaginate<GetListCourseCategoryResponse>> GetListByCategoryId(int categoryId, PageRequest pageRequest)
        {
            var data = await _courseCategoryDal.GetListAsync(
                predicate: ca => ca.CategoryId == categoryId,
                 include: ca => ca
                .Include(c => c.Course)
                .Include(cc => cc.Category),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
               );
            var result = _mapper.Map<Paginate<GetListCourseCategoryResponse>>(data);
            return result;
        }

        public async Task<IPaginate<GetListCourseCategoryResponse>> GetListByCourseId(int courseId, PageRequest pageRequest)
        {
            var data = await _courseCategoryDal.GetListAsync(
                predicate: ca => ca.CourseId == courseId,
                 include: ca => ca
                .Include(c => c.Course)
                .Include(cc => cc.Category),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
               );
            var result = _mapper.Map<Paginate<GetListCourseCategoryResponse>>(data);
            return result;
        }

        public async Task<GetListCourseCategoryResponse> GetById(int id)
        {
            var data = await _courseCategoryDal.GetAsync(
                c => c.Id == id,
                include: ca => ca
                .Include(c => c.Course)
                .Include(cc => cc.Category)
                );
            var result = _mapper.Map<GetListCourseCategoryResponse>(data);
            return result;
        }

        

        public async Task<UpdatedCourseCategoryResponse> UpdateAsync(UpdateCourseCategoryRequest updateCourseCategoryRequest)
        {
            var data = await _courseCategoryDal.GetAsync(i => i.Id == updateCourseCategoryRequest.Id);
            _mapper.Map(updateCourseCategoryRequest, data);
            data.UpdatedDate = DateTime.Now;
            await _courseCategoryDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedCourseCategoryResponse>(data);
            return result;
        }
    }
}
