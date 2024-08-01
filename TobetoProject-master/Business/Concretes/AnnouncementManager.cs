﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.AnnouncementRequests;
using Business.Dtos.Responses.AnnouncementResponses;
using Business.Dtos.Responses.CategoryResponses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;

namespace Business.Concretes
{
    public class AnnouncementManager : IAnnouncementService
    {
        IAnnouncementDal _announcementDal;
        IMapper _mapper;

        public AnnouncementManager(IAnnouncementDal announcementDal, IMapper mapper)
        {
            _announcementDal = announcementDal;
            _mapper = mapper;
        }

        public async Task<CreatedAnnouncementResponse> AddAsync(CreateAnnouncementRequest createAnnouncementRequest)
        {
            Announcement announcement = _mapper.Map<Announcement>(createAnnouncementRequest);
            Announcement createdAnnouncement = await _announcementDal.AddAsync(announcement);
            CreatedAnnouncementResponse createdAnnouncementResponse = _mapper.Map<CreatedAnnouncementResponse>(createdAnnouncement);
            return createdAnnouncementResponse;
        }

        public async Task<Announcement> DeleteAsync(int id)
        {
            var data = await _announcementDal.GetAsync(i => i.Id == id);
            var result = await _announcementDal.DeleteAsync(data);
            return result;
        }

        public async Task<IPaginate<GetListAnnouncementResponse>> GetAllAsync(PageRequest pageRequest)
        {
            var data = await _announcementDal.GetListAsync(
                orderBy: a => a.OrderByDescending(b => b.CreatedDate),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
               );
            
            var result = _mapper.Map<Paginate<GetListAnnouncementResponse>>(data);
            return result;
        }

        public async Task<GetListAnnouncementResponse> GetById(int id)
        {
            var data = await _announcementDal.GetAsync(c => c.Id == id);
            var result = _mapper.Map<GetListAnnouncementResponse>(data);
            return result;
        }

        public async Task<UpdatedAnnouncementResponse> UpdateAsync(UpdateAnnouncementRequest updateAnnouncementRequest)
        {
            var data = await _announcementDal.GetAsync(i => i.Id == updateAnnouncementRequest.Id);
            _mapper.Map(updateAnnouncementRequest, data);
            data.UpdatedDate = DateTime.Now;
            await _announcementDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedAnnouncementResponse>(data);
            return result;
        }
    }
}
