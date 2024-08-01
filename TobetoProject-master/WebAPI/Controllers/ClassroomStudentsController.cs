﻿using Business.Abstracts;
using Business.Dtos.Requests.ClassroomStudentRequests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ClassroomStudentsController : ControllerBase
{
    IClassroomStudentService _classroomStudentService;

    public ClassroomStudentsController(IClassroomStudentService classroomStudentService)
    {
        _classroomStudentService = classroomStudentService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateClassroomStudentRequest createClassroomStudentRequest)
    {
        var result = await _classroomStudentService.AddAsync(createClassroomStudentRequest);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var result = await _classroomStudentService.GetAllAsync(pageRequest);
        return Ok(result);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateClassroomStudentRequest updateClassroomStudentRequest)
    {
        var result = await _classroomStudentService.UpdateAsync(updateClassroomStudentRequest);
        return Ok(result);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        var result = await _classroomStudentService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var result = await _classroomStudentService.GetById(id);
        return Ok(result);
    }

    [HttpGet("GetListByStudentId")]
    public async Task<IActionResult> GetListByStudentId(int studentId,[FromQuery] PageRequest pageRequest)
    {
        var result = await _classroomStudentService.GetListByStudentId(studentId,pageRequest);
        return Ok(result);
    }

    [HttpGet("GetListByClassroomGroupId")]
    public async Task<IActionResult> GetListByClassroomGroupId(int classroomGroupId, [FromQuery] PageRequest pageRequest)
    {
        var result = await _classroomStudentService.GetListByClassroomGroupId(classroomGroupId, pageRequest);
        return Ok(result);
    }
}
