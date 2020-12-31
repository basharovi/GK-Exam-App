using GKExamApp.Models;

namespace GKExamApp
{
    public static class Utilities
    {
        public static User UserModel { get; private set; }

        public static void SetUserModel(User user)
        {
            UserModel = user;
        }



    }
}
