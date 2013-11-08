using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using ActiveDirectoryCommunicator.Properties;

namespace ActiveDirectoryCommunicator
{
    public class ActiveDirectoryComm : IDisposable
    {
        protected PrincipalContext ctx;
        private bool _disposed = false;

        public ActiveDirectoryComm()
        {
            ctx = new PrincipalContext(ContextType.Domain, Settings.Default.Domain, Settings.Default.Container);
        }

        public ActiveDirectoryComm(string domain, string container)
        {
            ctx = new PrincipalContext(ContextType.Domain, domain, container);
        }

        /// <summary>
        /// Get all users in active directory
        /// </summary>
        /// <returns></returns>
        public UserCollection GetAllUsers()
        {
            return new UserCollection(GetAllUsersRaw());
        }

        public GroupCollection GetAllGroups()
        {
            return new GroupCollection(GetAllGroupsRaw());
        }

        public User GetUser(string username)
        {
            using (UserPrincipal usp = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
            {
                return (new User(usp));
            }
        }

        /// <summary>
        /// Retreives principals from AD based on the given prototype
        /// </summary>
        /// <param name="prototype"></param>
        /// <returns></returns>
        private List<Principal> GetPrincipals(Principal prototype)
        {
            using (PrincipalSearcher ps = new PrincipalSearcher(prototype))
            {
                return ps.FindAll().ToList();
            }
        }

        /// <summary>
        /// Gets all the users in Active Directory as principal objects
        /// </summary>
        /// <returns></returns>
        private List<UserPrincipal> GetAllUsersRaw()
        {
            List<UserPrincipal> retUsers = new List<UserPrincipal>();

            using (UserPrincipal userPrincipal = new UserPrincipal(ctx))
            {
                userPrincipal.Name = "*";
                retUsers = GetPrincipals(userPrincipal).Cast<UserPrincipal>().ToList();
            }
            return retUsers;
        }

        private List<GroupPrincipal> GetAllGroupsRaw()
        {
            List<GroupPrincipal> retGroups = new List<GroupPrincipal>();

            using (GroupPrincipal groupPrincipal = new GroupPrincipal(ctx))
            {
                groupPrincipal.Name = "*";
                retGroups = GetPrincipals(groupPrincipal).Cast<GroupPrincipal>().ToList();
            }
            return retGroups;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
