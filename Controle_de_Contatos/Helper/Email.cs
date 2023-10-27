using System.Net;
using System.Net.Mail;

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
                string host = _configuration.GetValue<string>("SMTP:Host"); //o host pegamos de 'configuration' e passamos o nome exatamente como está lá no appsettings.json. ("SMTP:Host"), o SMTP é a chave principal e dentro do objeto temos a outra chave, o Host. Trás a informação de dentro de Host.
                string nome = _configuration.GetValue<string>("SMTP:Name");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

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
