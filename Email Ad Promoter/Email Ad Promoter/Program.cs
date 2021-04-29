using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace Email_Ad_Promoter
{
    class Program
    {
        public static int sent = 0;
        public static int errors = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("(1) Extract emails?");
            if(Console.ReadKey().KeyChar != '1')
            {

            Console.WriteLine("Start Sending Promotional Emails");
            Console.ReadKey();
            emailControl();

            }
            else
            {
                string ema = "";
                int counted = 0;
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\emails.txt"); ;

                    foreach (string line in lines)
                    {
                        counted++;
                        Console.Title = "Counted " + counted + " / " + lines.Count();
                        var emails = line.Replace(";",":").Split(':');
                        ema += emails[0];
                        ema += Environment.NewLine;
                        //Console.WriteLine(emails[0]);
                    }

                File.WriteAllText(Directory.GetCurrentDirectory() + @"\emails.txt", ema);
                Console.ReadLine();
            }
        }

        public static void emailControl()
        {
            Random rand = new Random();
            string email = "";
            string password = "";
            string emailtosend = "";
            string[] MYemails = { "contact.Koderz@gmail.com", "ProtectMeNow.V2@gmail.com", "nodontgo13345@gmail.com" };
            for (int i = 0; i < File.ReadAllLines(Directory.GetCurrentDirectory() + @"\emails.txt").Count(); i++)
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\emails.txt");
                int amount = lines.Count();

                emailtosend = lines[rand.Next(amount)];

                int random = rand.Next(MYemails.Count());

                email = MYemails[random];
                if(email == "nodontgo13345@gmail.com")
                {
                    password = "waterboss";
                }
                else if(email == "contact.Koderz@gmail.com")
                {
                    password = "Koderzfor2019";
                }
                else if(email == "ProtectMeNow.V2@gmail.com")
                {
                    password = "Shaen53642!?";
                }
                
                sent++;
                sendEmail(email, password, emailtosend);
                Task.Delay(500);
                Console.Title = "Sent : " + sent + "  |  Errors : " + errors;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Finished sending emails! Sent " + sent + " and there were " + errors + "!");
            Console.ReadLine();
        }

        public static void sendEmail(string email, string password, string emailToSend)
        {
            try
            {
                string subject = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Subject.txt");
                string body = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Body.txt");
                
                var cred = new NetworkCredential(email, password);
                var msg = new MailMessage(email, emailToSend, subject, body);
                var client = new SmtpClient("smtp.gmail.com",587);
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = cred;
                client.Send(msg);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sent " + emailToSend + " an email!");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("From " + email);

            }
            catch(Exception ex)
            {
                errors++;
                sent--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error sending message : " + ex.Message);
            }
        }
    }
}
