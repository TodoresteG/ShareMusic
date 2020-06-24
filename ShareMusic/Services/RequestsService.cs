using System;
using System.Collections.Generic;
using System.Linq;
using ShareMusic.Data;
using ShareMusic.Data.Entities;
using ShareMusic.Models.Requests;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly ShareMusicDbContext context;

        public RequestsService(ShareMusicDbContext context)
        {
            this.context = context;
        }

        public void Join(string groupId, string userName, string userId)
        {
            Request request = new Request
            {
                CreatedOn = DateTime.UtcNow,
                Name = $"{userName} - {DateTime.UtcNow}",
            };

            GroupRequest existingRequest = this.context.GroupRequests.FirstOrDefault(gr => gr.GroupId == groupId && gr.RequestId == request.Id);
            if (existingRequest != null)
            {
                return;
            }

            GroupRequest groupRequest = new GroupRequest
            {
                GroupId = groupId,
                RequestId = request.Id,
            };

            this.context.Requests.Add(request);
            this.context.GroupRequests.Add(groupRequest);
            this.context.SaveChanges();
        }

        public AllGroupRequestsViewModel ListAllReuqestsForGroup(string groupId)
        {
            List<GroupRequestViewModel> requests = this.context.GroupRequests
                .Where(gr => gr.GroupId == groupId)
                .Select(gr => new GroupRequestViewModel
                {
                    GroupId = gr.GroupId,
                    GroupName = gr.Group.Name,
                    Name = gr.Request.Name,
                    RequestId = gr.RequestId,
                }).ToList();

            return new AllGroupRequestsViewModel { Reuests = requests };
        }
    }
}
