using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareMusic.Data;
using ShareMusic.Models.Groups;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ShareMusicDbContext context;

        public GroupsService(ShareMusicDbContext context)
        {
            this.context = context;
        }

        public CreateGroupInputModel ListAllUsers()
        {
            List<SelectListItem> users = this.context
                .Users
                .Select(u => new SelectListItem(u.UserName, u.UserName))
                .ToList();

            return new CreateGroupInputModel { Users = users };
        }
    }
}
