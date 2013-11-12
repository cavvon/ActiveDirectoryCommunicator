using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveDirectoryCommunicator
{
    public class User : MyPrincipal
    {
        public string Phone { get; private set; }
        public List<string> DistributionGroups { get; private set; }
        public List<string> SecurityGroups { get; private set; }

        public User()
        {
        }

        public User(string name, Guid guid, string loginName, string email, string phone)
            : base(name, guid, loginName, email)
        {
            Phone = phone;
        }

        public User(UserPrincipal userP) : this(userP, false)
        {
        }

        /// <summary>
        /// Simple allows for construction to skip querying for security and distribution groups
        /// which is much faster
        /// </summary>
        /// <param name="userP"></param>
        /// <param name="simple"></param>
        public User(UserPrincipal userP, bool simple)
        {
            Name = userP.DisplayName;
            Guid = userP.Guid;
            LoginName = userP.SamAccountName;
            Email = userP.EmailAddress;
            Phone = userP.VoiceTelephoneNumber;
            if (simple)
            {
                SecurityGroups = new List<string>() { "Must call User constructor with simple equal to false" };
                DistributionGroups = new List<string>() { "Must call User constructor with simple equal to false;" };
            }
            else
            {
                try
                {
                    SecurityGroups = PrincipalCollection.PrincipalListToNames(userP.GetAuthorizationGroups().Cast<Principal>().ToList());
                    DistributionGroups = PrincipalCollection.PrincipalListToNames(
                        GroupCollection.GetEmailGroups(userP.GetGroups().Cast<Principal>().ToList()));
                }
                catch (PrincipalOperationException)
                {
                    // User does not have privledges to view groups.
                    SecurityGroups = new List<string>() { "Authentication Error" };
                    DistributionGroups = new List<string>() { "Authentication Error" };
                }
            }
        }
    }
}
