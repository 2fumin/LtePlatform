using LtePlatform.Models;
using Microsoft.Owin.Security;

namespace LtePlatform
{
    public static class UserContextConfiguration
    {
        public static IndexViewModel CurrentUser { get; set; }

        public static string CurrentMessage { get; set; }
    }
}
