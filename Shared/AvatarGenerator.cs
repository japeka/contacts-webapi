using System;
namespace ContactsWebApi.Shared
{
    public class AvatarGenerator
    {
        private string _avatarUrl = "http://svgavatars.com/style/svg/";
        private readonly string[,] _avatars = new string[,]
        {
            { "02", "04","06","12","18","20" }, { "03", "05","09","11","13","15" }
        };

        public AvatarGenerator() {}
        public string GetAvatarPicture(int gender)
        {
            var random = new Random();
            var randomNumber = random.Next(0, 6); 
            return _avatarUrl + _avatars[gender, randomNumber] + ".svg";
        }
    }
}
