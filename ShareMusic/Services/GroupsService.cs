using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareMusic.Data;
using ShareMusic.Data.Entities;
using ShareMusic.Models.Groups;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ShareMusicDbContext context;
        private readonly UserManager<User> userManager;

        public GroupsService(ShareMusicDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public void CreateGroup(CreateGroupInputModel inputModel)
        {
            Group group = new Group
            {
                Name = inputModel.Name,
                CreatedOn = DateTime.UtcNow,
                OwnerId = inputModel.OwnerId,
            };

            List<GroupUser> groupUsers = inputModel
                .SelectedUsers
                .Select(u => new GroupUser
                {
                    UserId = this.userManager.Users.FirstOrDefault(x => x.Email == u).Id,
                })
                .ToList();

            group.Users = groupUsers;

            this.context.Groups.Add(group);
            this.context.SaveChanges();
        }

        public GroupsListViewModel ListAllGroupsForUser(string userId)
        {
            GroupsListViewModel groupsList = new GroupsListViewModel
            {
                GroupsForUser = this.context.GroupUsers
                    .Where(gu => gu.UserId == userId)
                    .Select(gu => new GroupUsersListViewModel
                    {
                        Id = gu.GroupId,
                        Name = gu.Group.Name,
                    }).ToList(),
                OwnedGroups = this.context.Groups
                    .Where(g => g.OwnerId == userId)
                    .Select(g => new GroupsOwnedByUserViewModel
                    {
                        Id = g.Id,
                        Name = g.Name,
                    }).ToList(),
            };

            return groupsList;
        }

        public CreateGroupInputModel ListAllUsers()
        {
            List<string> users = this.context
                .Users
                .Select(u => u.UserName)
                .ToList();

            return new CreateGroupInputModel { MultiSelectUsers = new MultiSelectList(users) };
        }
    }
}
