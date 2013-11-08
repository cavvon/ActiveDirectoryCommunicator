using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryCommunicator
{
    public class PrincipalCollection : IEnumerable<MyPrincipal>
    {
        public List<MyPrincipal> List { get; protected set; }

        public PrincipalCollection()
        {
            List = new List<MyPrincipal>();
        }

        public PrincipalCollection(List<MyPrincipal> list)
        {
            List = list;
        }

        public PrincipalCollection(List<Principal> princList)
            : this()
        {
            foreach (Principal principal in princList)
            {
                Add(new MyPrincipal(principal));
            }
        }

        public void Add(MyPrincipal principal)
        {
            List.Add(principal);
        }

        public static List<string> PrincipalListToNames(List<Principal> princList)
        {
            List<string> retList = new List<string>();
            foreach (Principal principal in princList)
            {
                retList.Add(principal.DisplayName);
            }
            return retList;
        }

        public IEnumerator<MyPrincipal> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
