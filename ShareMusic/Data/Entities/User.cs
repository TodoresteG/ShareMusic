﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Playlists = new HashSet<Playlist>();
            this.OwnedGroups = new HashSet<Group>();
            this.GroupUsers = new HashSet<GroupUser>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<Playlist> Playlists { get; set; }

        public ICollection<Group> OwnedGroups { get; set; } 

        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
