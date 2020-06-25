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

        public void ApproveRequest(string groupId, string requestId, string userName)
        {
            Request request = this.context.Requests.FirstOrDefault(r => r.Id == requestId);
            GroupRequest groupRequest = this.context.GroupRequests.FirstOrDefault(gr => gr.GroupId == groupId && gr.RequestId == requestId);

            if (request == null || groupRequest == null)
            {
                return;
            }

            User userToJoin = this.context.Users.FirstOrDefault(u => u.UserName == userName) as User;
            GroupUser groupUser = new GroupUser
            {
                CreatedOn = DateTime.UtcNow,
                GroupId = groupId,
                User = userToJoin,
            };

            this.context.GroupUsers.Add(groupUser);
            this.context.GroupRequests.Remove(groupRequest);
            this.context.Requests.Remove(request);
            this.context.SaveChanges();
        }

        public void DeclineRequest(string groupId, string requestId, string userName)
        {
            Request request = this.context.Requests.FirstOrDefault(r => r.Id == requestId);
            GroupRequest groupRequest = this.context.GroupRequests.FirstOrDefault(gr => gr.GroupId == groupId && gr.RequestId == requestId);

            if (request == null || groupRequest == null)
            {
                return;
            }

            this.context.Requests.Remove(request);
            this.context.GroupRequests.Remove(groupRequest);
            this.context.SaveChanges();
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
                    Name = gr.Request.Name,
                    RequestId = gr.RequestId,
                    UserName = gr.Request.Name.Split(" - ", StringSplitOptions.RemoveEmptyEntries)[0],
                }).ToList();

            string groupName = this.context.Groups
                .Where(g => g.Id == groupId)
                .Select(g => g.Name)
                .FirstOrDefault();

            return new AllGroupRequestsViewModel
            {
                GroupId = groupId,
                GroupName = groupName,
                Requests = requests,
            };
        }
    }
}
