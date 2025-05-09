namespace AgriEnergyConnect.Models
{
    public static class Global
    {
        public static bool UserLoggedIn { get; set; } = false;
        public static object? UserInfo { get; set; }
        public static bool IsEmployee { get; set; } = false;
    }
}
