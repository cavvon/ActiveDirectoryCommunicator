using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryCommunicator
{
    public class MyPrincipal
    {
        public string Name { get; protected set; }
        public Guid? Guid { get; protected set; }
        public string LoginName { get; protected set; }
        public string Email { get; protected set; }

        public MyPrincipal()
        {

        }

        public MyPrincipal(string name, Guid guid, string loginName, string email)
        {
            Name = name;
            Guid = guid;
            LoginName = loginName;
            Email = email;
        }

        public MyPrincipal(Principal principal)
        {
            Name = principal.DisplayName;
            Guid = principal.Guid;
            LoginName = principal.SamAccountName;
            Email = null;
        }
    }
}
