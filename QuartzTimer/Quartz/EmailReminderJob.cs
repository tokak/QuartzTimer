
using MailKit.Net.Smtp;
using MimeKit;
using Quartz;
using QuartzTimer.Context;
using QuartzTimer.Entities;

namespace QuartzTimer.Quartz
{
    [DisallowConcurrentExecution]
    public class EmailReminderJob : IJob
    {
        private readonly AppDbContext _context;

        public EmailReminderJob(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> ExecuteDb()
        {
            return GetStudentsWithNoDocument();
        }

     

        public async Task Execute(IJobExecutionContext context)
        {
            var request = await ExecuteDb();
            if (request != null)
            {
                foreach (var item in request)
                {
                    if (item.Document == null)
                    {
                       mailsend(item);
                        item.Document = "Mail Gönderildi";
                        _context.Students.Update(item);
                        _context.SaveChanges();
                    }

                }

            }
        }

        private static void mailsend(Student item)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Quartz Zamanlayıcı", "murattokak21@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", item.Email);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Test mesajıdır ";
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Quartz";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("mail bilginizi yazını", "şifrenizi yazınız");
            client.Send(mimeMessage);
            client.Disconnect(true);
            
        }

        private List<Student> GetStudentsWithNoDocument()
        {
            var request = _context.Students.Where(x => x.Document == null).ToList();
            if (request.Count > 0)
            {
                return request;
            }
            return null;
        }
    }
}
