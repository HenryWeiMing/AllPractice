namespace AllPractice.Models
{
    public class AppSetting
    {
        public URL URL { get; set; }
    }

    public class URL
    {
        public string DB { get; set; }
        public string Redis { get; set; }
        public string Npgsql { get; set; }
    }
}
