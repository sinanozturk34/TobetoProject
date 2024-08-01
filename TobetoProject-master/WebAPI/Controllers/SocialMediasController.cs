﻿using Business.Abstracts;
using Business.Dtos.Requests.SocialMediaRequests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class SocialMediasController : ControllerBase
    {
        ISocialMediaService _socialMediaService;

        public SocialMediasController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateSocialMediaRequest createSocialMediaRequest)
        {
            var result = await _socialMediaService.AddAsync(createSocialMediaRequest);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var result = await _socialMediaService.GetAllAsync(pageRequest);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateSocialMediaRequest updateSocialMediaRequest)
        {
            var result = await _socialMediaService.UpdateAsync(updateSocialMediaRequest);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _socialMediaService.DeleteAsync(id);
            return Ok(result);
        }
        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _socialMediaService.GetById(id);
            return Ok(result);
        }
    }
}
