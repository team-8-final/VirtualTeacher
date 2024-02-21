namespace VirtualTeacher.Helpers
{
    public class AvatarHelper
    {
        const string avatarDir = "wwwroot/images/avatars";


        public string GetAvatar(string username)
        {
            bool userHasAvatar = Directory.EnumerateFiles(avatarDir, $"{username}*").Any();

            string userAvatar = "avatar-default.jpg";

            if (userHasAvatar)
            {
                var directory = new DirectoryInfo(@"wwwroot/images/avatars");
                var fileInfo = directory.GetFiles("*" + username + "*.*");
                userAvatar = fileInfo[0].Name;
            }

            return userAvatar;
        }
    }
}
