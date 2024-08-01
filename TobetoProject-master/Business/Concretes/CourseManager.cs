﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.CourseRequests;
using Business.Dtos.Responses.ClassroomInstructorResponses;
using Business.Dtos.Responses.CourseResponses;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Perfromance;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes
{
    public class CourseManager : ICourseService
    {
        ICourseDal _courseDal;
        IMapper _mapper;

        public CourseManager(ICourseDal courseDal, IMapper mapper)
        {
            _courseDal = courseDal;
            _mapper = mapper;
        }


        public async Task<CreatedCourseResponse> AddAsync(CreateCourseRequest createCourseRequest)
        {
            Course course = _mapper.Map<Course>(createCourseRequest);
            Course createdCourse = await _courseDal.AddAsync(course);
            CreatedCourseResponse createdCourseResponse = _mapper.Map<CreatedCourseResponse>(createdCourse);
            return createdCourseResponse;
        }

        public async Task<Course> DeleteAsync(int id)
        {
            var data = await _courseDal.GetAsync(i => i.Id == id);
            var result = await _courseDal.DeleteAsync(data);
            return result;
        }

        //[PerformanceAspect(5)] AspectInterceptorSelector'de genel olarak aktif edebiliriz.
        [CacheAspect]
        public async Task<IPaginate<GetListCourseResponse>> GetAllAsync(PageRequest pageRequest)
        {
            var data = await _courseDal.GetListAsync(
                include: p=> p.Include(st=> st.CourseSubType)
                .Include(st => st.Image),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
               );
            var result = _mapper.Map<Paginate<GetListCourseResponse>>(data);
            return result;
        }

        public async Task<GetListCourseResponse> GetById(int id)
        {
            var data = await _courseDal.GetAsync(
                c => c.Id == id,
                include: p => p.Include(st => st.CourseSubType)
                );
            var result = _mapper.Map<GetListCourseResponse>(data);
            return result;
        }

        public async Task<UpdatedCourseResponse> UpdateAsync(UpdateCourseRequest updateCourseRequest)
        {
            var data = await _courseDal.GetAsync(i => i.Id == updateCourseRequest.Id);
            _mapper.Map(updateCourseRequest, data);
            data.UpdatedDate = DateTime.Now;
            await _courseDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedCourseResponse>(data);
            return result;
        }

    }
}
