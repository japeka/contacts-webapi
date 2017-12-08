using System;
namespace ContactsWebApi.Shared
{
    public class SharedClass
    {
        private string avatarUrl = "http://svgavatars.com/style/svg/";
        private readonly string[,] avatars = new string[,]
        {
            { "02", "04","06","12","18","20" }, { "03", "05","09","11","13","15" }
        };

        public SharedClass() {}
        public string GetAvatarPicture(int Gender)
        {
            var random = new Random();
            var randomNumber = random.Next(0, 6); 
            return avatarUrl + avatars[Gender, randomNumber] + ".svg";
        }
    }
}
