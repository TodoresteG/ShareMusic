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

        public void AddUsers(UsersListViewComponentViewModel inputModel, string groupId)
        {
            List<string> usersInGroup = this.context.GroupUsers
                .Where(gu => gu.GroupId == groupId)
                .Select(gu => gu.User.UserName).ToList();

            IEnumerable<string> userIds = inputModel.SelectedUsers
                .Except(usersInGroup)
                .Select(u => this.userManager.Users.FirstOrDefault(x => x.Email == u).Id);

            List<GroupUser> groupUsers = new List<GroupUser>();

            foreach (var id in userIds)
            {
                GroupUser groupUser = new GroupUser
                {
                    GroupId = groupId,
                    UserId = id,
                    CreatedOn = DateTime.UtcNow,
                };

                groupUsers.Add(groupUser);
            }

            this.context.GroupUsers.AddRange(groupUsers);
            this.context.SaveChanges();
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

        public GroupDetailsViewModel GetGroupDetails(string groupId)
        {
            GroupDetailsViewModel groupDetails = this.context.Groups
                .Where(g => g.Id == groupId)
                .Select(g => new GroupDetailsViewModel
                {
                    Name = g.Name,
                    OwnerName = g.Owner.UserName,
                    GroupUserNames = this.context.GroupUsers
                        .Where(gu => gu.GroupId == groupId)
                        .Select(gu => gu.User.UserName).ToList(),
                }).FirstOrDefault();

            return groupDetails;
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

        public MultiSelectList ListAllUsers()
        {
            List<string> users = this.context
                .Users
                .Select(u => u.UserName)
                .ToList();

            return new MultiSelectList(users);
        }

        public GroupsSearchResultViewModel SearchGroups(string groupName)
        {
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrWhiteSpace(groupName))
            {
                return new GroupsSearchResultViewModel { Count = 0, SearchResults = new List<GroupsSearchResultListViewModel>(), };
            }

            List<GroupsSearchResultListViewModel> searchResults = this.context.Groups
                    .Where(g => g.Name.Contains(groupName))
                    .Select(g => new GroupsSearchResultListViewModel
                    {
                        Id = g.Id,
                        Name = g.Name,
                    }).ToList();

            GroupsSearchResultViewModel groups = new GroupsSearchResultViewModel
            {
                SearchResults = searchResults,
                Count = searchResults.Count,
            };

            return groups;
        }
    }
}
