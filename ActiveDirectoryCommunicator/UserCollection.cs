using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryCommunicator
{
    public class UserCollection : PrincipalCollection
    {
        public UserCollection()
            : base()
        {
        }

        public UserCollection(List<User> userList)
        {
            List = userList.Cast<MyPrincipal>().ToList();
        }

        public UserCollection(List<UserPrincipal> princList)
            : this()
        {
            foreach (UserPrincipal principal in princList)
            {
                Add(new User(principal));
            }
        }

        /// <summary>
        /// Gets all human users
        /// </summary>
        /// <returns></returns>
        public UserCollection GetHumanUsers()
        {
            UserCollection retUsers = new UserCollection();
            foreach (User user in List)
            {
                if (user.DistributionGroups.Contains(Properties.Settings.Default.HumanUsersGroup))
                {
                    retUsers.Add(user);
                }
            }
            return retUsers;
        }
    }
}
