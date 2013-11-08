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

        public User(UserPrincipal userP)
        {
            Name = userP.DisplayName;
            Guid = userP.Guid;
            LoginName = userP.SamAccountName;
            Email = userP.EmailAddress;
            Phone = userP.VoiceTelephoneNumber;
            try
            {
                SecurityGroups = PrincipalCollection.PrincipalListToNames(userP.GetAuthorizationGroups().Cast<Principal>().ToList());
                DistributionGroups = PrincipalCollection.PrincipalListToNames(
                    GroupCollection.GetEmailGroups(userP.GetGroups().Cast<Principal>().ToList()));
            }
            catch (PrincipalOperationException e)
            {
                // User does not have privledges to view groups.
                SecurityGroups = new List<string>() { "Authentication Error" };
                DistributionGroups = new List<string>() { "Authentication Error" };
            }
        }
    }
}
