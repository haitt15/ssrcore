using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Helpers
{
    public class Constants
    {
        public struct Admin
        {
            public const string ADMIN = "Admin";
        }

        public struct Users
        {
            public const string PASSWORD = "ThisIsMyPassword";
        }

        public struct Roles
        {
            public const string ROLE_STAFF = "role01";
            public const string ROLE_STUDENT = "role02";
        }

        public struct SortBy
        {
            public const int SORT_NAME_ASC = 0;
            public const int SORT_NAME_DES = 1;
        }

        public struct KeyRedis
        {
            public const string SERVICES = "Services";
        }
    }
}
