namespace GKExamApp
{
    public static class Utilities
    {
        public static string UserRole { get; private set; }

        public static void SetUserRole(string userRole)
        {
            UserRole = userRole;
        }

    }
}
