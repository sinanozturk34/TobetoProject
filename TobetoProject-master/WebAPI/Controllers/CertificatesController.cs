﻿using Business.Abstracts;
using Business.Dtos.Requests.CertificateRequests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CertificatesController : ControllerBase
    {
        ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateCertificateRequest createCertificateRequest)
        {
            var result = await _certificateService.AddAsync(createCertificateRequest);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var result = await _certificateService.GetAllAsync(pageRequest);
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _certificateService.GetById(id);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCertificateRequest updateCertificateRequest)
        {
            var result = await _certificateService.UpdateAsync(updateCertificateRequest);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _certificateService.DeleteAsync(id);
            return Ok(result);
        }

    }
}
