using FirebaseAdmin.Messaging;
using ssrcore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssrcore.Helpers
{
    public class Utils
    {


        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string GetUserNo(string email, string displayName)
        {
            int lastIndex = displayName.LastIndexOf("(");  //lastIndexOf(" ");
            string fullname = displayName.Substring(0, lastIndex).Trim();
            string[] arr = fullname.Split(" ");
            string username = arr[arr.Length - 1];
            int lengthPre = username.Length - 1 + arr.Length;
            email = email.Substring(lengthPre);
            int indexEmail = email.LastIndexOf("@fpt.edu.vn");
            string result = email.Substring(0, indexEmail);
            return result.ToUpper();
        }

        public static async Task<string> PushNotificationAsync (string fcmToken, string title, string body)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title =title,
                    Body = body
                },

                Token = fcmToken
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            return result;
        }
    }
}
