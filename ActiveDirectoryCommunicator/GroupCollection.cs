using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace ActiveDirectoryCommunicator
{
    public class GroupCollection : PrincipalCollection
    {
        public GroupCollection()
            : base()
        {
        }

        public GroupCollection(List<Group> GroupList)
        {
            List = GroupList.Cast<MyPrincipal>().ToList();
        }

        public GroupCollection(List<GroupPrincipal> princList)
            : this()
        {
            foreach (GroupPrincipal principal in princList)
            {
                Add(new Group(principal));
            }
        }

        /// <summary>
        /// Gets all human Groups
        /// </summary>
        /// <returns></returns>
        public GroupCollection GetEmailGroups()
        {
            return new GroupCollection(List.Where(x => x.Email != null).Cast<Group>().ToList());
        }

        /// <summary>
        /// Gets all human Groups (safe from infinite recursion)
        /// </summary>
        /// <returns></returns>
        public static List<Principal> GetEmailGroups(List<Principal> principals)
        {
            List<Principal> distGroups = new List<Principal>();
            PropertyValueCollection email;
            foreach (Principal group in principals)
            {
                email = ((DirectoryEntry)group.GetUnderlyingObject()).Properties["mail"];
                if (email.Value != null)
                {
                    distGroups.Add(group);
                }
            }
            return distGroups;
        }
    }
}
