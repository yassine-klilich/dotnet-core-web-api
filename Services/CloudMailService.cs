namespace PracticeWebAPI.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string fromEmail = string.Empty;
        private readonly string toEmail = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            fromEmail = configuration["mailSetting:fromEmail"] ?? string.Empty;
            toEmail = configuration["mailSetting:toEmail"] ?? string.Empty;
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"################ {nameof(CloudMailService)} ################");
            Console.WriteLine("---------------- Subject ----------------");
            Console.WriteLine(subject);
            Console.WriteLine($"From : {fromEmail}");
            Console.WriteLine($"To   : {toEmail}");
            Console.WriteLine("---------------- Message ----------------");
            Console.WriteLine(message);
        }
    }
}
