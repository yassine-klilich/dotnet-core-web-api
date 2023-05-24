namespace PracticeWebAPI.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string fromEmail = string.Empty;
        private readonly string toEmail = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            fromEmail = configuration["mailSetting:fromEmail"] ?? string.Empty;
            toEmail = configuration["mailSetting:toEmail"] ?? string.Empty;
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"################ {nameof(LocalMailService)} ################");
            Console.WriteLine("---------------- Subject ----------------");
            Console.WriteLine(subject);
            Console.WriteLine($"From : {fromEmail}");
            Console.WriteLine($"To   : {toEmail}");
            Console.WriteLine("---------------- Message ----------------");
            Console.WriteLine(message);
        }
    }
}
