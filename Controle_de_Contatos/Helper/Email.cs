using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Controle_de_Contatos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                // Recupere as configurações do appsettings.json
                var smtpConfig = _configuration.GetSection("SMTP");

                string host = smtpConfig["Host"];
                string nome = smtpConfig["Name"];
                string username = smtpConfig["UserName"];
                string senha = smtpConfig["Senha"];
                int porta = smtpConfig.GetValue<int>("Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                // Trate os erros ou registre mensagens de erro conforme necessário.
                return false;
            }
        }
    }
}




/*
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Controle_de_Contatos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                //começamos a recuperar todos os dados do appsettings.json
                string host = _configuration.GetSection("SMTP")["Host"];
                string nome = _configuration.GetSection("SMTP")["Name"];
                string username = _configuration.GetSection("SMTP")["UserName"];
                string senha = _configuration.GetSection("SMTP")["Senha"];
                int porta = _configuration.GetSection("SMTP").GetValue<int>("Porta");


                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };

                mail.To.Add(email); //para quem vamos enviar o email? Será o email do usuário que receberá essa senha
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //aqui fazemos de fato o envio do e-mail
                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);//credenciais do email do smtp
                    smtp.EnableSsl = true; //envio de e-mail seguro

                    smtp.Send(mail); //enviar o email, passando o email destino

                    return true;
                }

            }
            catch (System.Exception ex)
            {
                //gravar log de erro ao enviar e-mail
                return false;
            }
        }
    }
}
*/