namespace SCN
{
    public static class User
    {
        public static string FIO { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string PhoneNumber { get; set; }
        public static string Address { get; set; }
        public static int IsAdmin { get; set; } = 0;
    }
}