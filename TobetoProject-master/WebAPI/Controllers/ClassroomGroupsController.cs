﻿using Business.Abstracts;
using Business.Dtos.Requests.ClassroomGroupRequests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ClassroomGroupsController : ControllerBase
{
    IClassroomGroupService _classroomGroupService;

    public ClassroomGroupsController(IClassroomGroupService classroomGroupService)
    {
        _classroomGroupService = classroomGroupService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateClassroomGroupRequest createClassroomGroupRequest)
    {
        var result = await _classroomGroupService.AddAsync(createClassroomGroupRequest);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var result = await _classroomGroupService.GetAllAsync(pageRequest);
        return Ok(result);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateClassroomGroupRequest updateClassroomGroupRequest)
    {
        var result = await _classroomGroupService.UpdateAsync(updateClassroomGroupRequest);
        return Ok(result);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        var result = await _classroomGroupService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var result = await _classroomGroupService.GetById(id);
        return Ok(result);
    }

    [HttpGet("GetListByGroupId")]
    public async Task<IActionResult> GetListByGroupId(int groupId,[FromQuery] PageRequest pageRequest)
    {
        var result = await _classroomGroupService.GetListByGroupId(groupId,pageRequest);
        return Ok(result);
    }

    [HttpGet("GetListByClassroomId")]
    public async Task<IActionResult> GetListByClassroomId(int classroomId, [FromQuery] PageRequest pageRequest)
    {
        var result = await _classroomGroupService.GetListByClassroomId(classroomId, pageRequest);
        return Ok(result);
    }


}
