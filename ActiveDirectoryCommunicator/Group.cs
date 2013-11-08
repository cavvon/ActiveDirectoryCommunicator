using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace ActiveDirectoryCommunicator
{
    public class Group : MyPrincipal
    {
        public Group()
        {}

        public Group(GroupPrincipal groupP)
        {
            Email = ((DirectoryEntry)groupP.GetUnderlyingObject()).Properties["mail"].Value as string;
        }
    }
}
