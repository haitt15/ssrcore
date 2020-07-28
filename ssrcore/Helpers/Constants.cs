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
            public const int SORT_NAME_ASC = 1;
            public const int SORT_NAME_DES = 2;
        }

        public struct KeyRedis
        {
            public const string SERVICES = "Services";
        }
        public struct GoogleSheet
        {
            public const string SHEET_IN_PROGRESS = "1rQDO2xqPhMyyuEh3G6qbdRibbRFGZl5LeE4km94znTU";
            public const string SHEET_FINISHED = "17p3zi7ha2S0TH7LAn-wtiZCEguY8oUeM5uikdNRGSxI";
            public const string SHEET_REJECTED = "1MG8ZU9AE4Cu6P85Oc4QS83SKWf5ESq7RwubQnkbSaQo";
            public const string SHEET_REQUEST_SERVICE = "11G2M1iKzud1fonZjUlfYS8-rE0lXLmQgBP49rQSbi_Y";
        }
    }
}
