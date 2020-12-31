using GKExamApp.Models;
using GKExamApp.UI;

namespace GKExamApp
{
    public static class Utilities
    {
        public static User UserModel { get; private set; }

        public static void SetUserModel(User user)
        {
            UserModel = user;
        }

        public static void BackToDashboard()
        {
            if (UserModel.Role.Equals("Admin"))
            {
                var admin = new AdminDashboard();
                admin.Show();
            }
            else
            {
                var user = new UserDashboard();
                user.Show();
            }
        }

    }
}
