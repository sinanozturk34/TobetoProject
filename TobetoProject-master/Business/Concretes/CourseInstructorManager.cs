﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.ClassroomInstructorRequests;
using Business.Dtos.Responses.ClassroomInstructorResponses;
using Business.Dtos.Responses.CourseCompanyResponses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes;

public class CourseInstructorManager : ICourseInstructorService
{
    ICourseInstructorDal _courseInstructorDal;
    IMapper _mapper;

    public CourseInstructorManager(ICourseInstructorDal courseInstructorDal, IMapper mapper)
    {
        _courseInstructorDal = courseInstructorDal;
        _mapper = mapper;
    }

    public async Task<CreatedCourseInstructorResponse> AddAsync(CreateCourseInstructorRequest createCourseInstructorRequest)
    {
        CourseInstructor courseInstructor = _mapper.Map<CourseInstructor>(createCourseInstructorRequest);
        CourseInstructor createdCourseInstructor = await _courseInstructorDal.AddAsync(courseInstructor);
        CreatedCourseInstructorResponse createdCourseInstructorResponse = _mapper.Map<CreatedCourseInstructorResponse>(createdCourseInstructor);
        return createdCourseInstructorResponse;
    }

    public async Task<CourseInstructor> DeleteAsync(int Id)
    {
        var data = await _courseInstructorDal.GetAsync(i => i.Id == Id);
        var result = await _courseInstructorDal.DeleteAsync(data);
        return result;
    }

    public async Task<IPaginate<GetListCourseInstructorResponse>> GetAllAsync(PageRequest pageRequest)
    {
        var data = await _courseInstructorDal.GetListAsync(
            include: ci => ci.Include(cl => cl.Instructor).ThenInclude(u => u.User).Include(i => i.Course),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize
            );
        var result = _mapper.Map<Paginate<GetListCourseInstructorResponse>>(data);
        return result;
    }

    public async Task<IPaginate<GetListCourseInstructorResponse>> GetListByCourseId(int courseId, PageRequest pageRequest)
    {
        var data = await _courseInstructorDal.GetListAsync(
            include: ci => ci.Include(cl => cl.Instructor).ThenInclude(u => u.User).Include(i => i.Course),
            predicate: ci => ci.CourseId == courseId,
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize
            );
        var result = _mapper.Map<Paginate<GetListCourseInstructorResponse>>(data);
        return result;
    }

    public async Task<IPaginate<GetListCourseInstructorResponse>> GetListByInstructorId(int instructorId, PageRequest pageRequest)
    {
        var data = await _courseInstructorDal.GetListAsync(
            include: ci => ci.Include(cl => cl.Instructor).ThenInclude(u => u.User).Include(i => i.Course),
            predicate: ci => ci.InstructorId == instructorId,
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize
            );
        var result = _mapper.Map<Paginate<GetListCourseInstructorResponse>>(data);
        return result;
    }




    public async Task<GetListCourseInstructorResponse> GetById(int id)
    {
        var data = await _courseInstructorDal.GetAsync(
            c => c.Id == id,
            include: ci => ci.Include(cl => cl.Instructor).ThenInclude(u => u.User).Include(i => i.Course)
            );
        var result = _mapper.Map<GetListCourseInstructorResponse>(data);
        return result;
    }



    public async Task<UpdatedCourseInstructorResponse> UpdateAsync(UpdateCourseInstructorRequest updateCourseInstructorRequest)
    {
        var data = await _courseInstructorDal.GetAsync(i => i.Id == updateCourseInstructorRequest.Id);
        _mapper.Map(updateCourseInstructorRequest, data);
        data.UpdatedDate = DateTime.Now;
        await _courseInstructorDal.UpdateAsync(data);
        var result = _mapper.Map<UpdatedCourseInstructorResponse>(data);
        return result;

    }


}

