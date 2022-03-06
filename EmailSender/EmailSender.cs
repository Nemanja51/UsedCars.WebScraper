using System.Net;
using System.Net.Mail;
using System.Text;

namespace EmailSender
{
    public static class EmailSender
    {
        public static void SendEmail(int textForBody)
        {
            string subj = "Difference in car count for your search";
            string body = BuildBody(textForBody);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("email", "password"),
                EnableSsl = true,
            };

            smtpClient.Send("email", "recipient", subj, body);
        }

        private static string BuildBody(int textForBody)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear user,");
            sb.AppendLine();
            sb.AppendLine($"There is difference in your search. Difference now is: {textForBody}!");
            sb.AppendLine();
            sb.AppendLine("We will keep youu updated!");
            sb.AppendLine();
            sb.AppendLine("Sincerelly,");
            sb.AppendLine("Your WebScraper");

            return sb.ToString();
        }
    }
}
