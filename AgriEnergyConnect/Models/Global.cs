namespace AgriEnergyConnect.Models
{
    /// <summary>
    /// Static Class: Holds user session information.
    /// </summary>
    public static class Global
    {
        public static bool UserLoggedIn { get; set; } = false;
        public static object? UserInfo { get; set; }
        public static bool IsEmployee { get; set; } = false;
    }
}
