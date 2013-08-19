using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EHR.Infrastructure.Util
{
    public static class EmailUtil
    {
        public static void EnviarEmail(string assunto, string mensagem, List<MailAddress> destinatarios)
        {
            EnviarEmail(assunto, mensagem, destinatarios, true);
        }

        public static void EnviarEmail(string assunto, string mensagem, List<MailAddress> destinatarios, bool isBodyHtml)
        {
            var message = new MailMessage();

            destinatarios.ForEach(destinatario => message.To.Add(destinatario));

            message.Subject = assunto;
            message.IsBodyHtml = true;
            message.Body = mensagem;

            var smtp = new SmtpClient();

            smtp.Send(message);
        }

        public static void EnviarEmail(string assunto, string mensagem, string destinatario)
        {
            var destinatarios = new List<MailAddress> { new MailAddress(destinatario) };

            EnviarEmail(assunto, mensagem, destinatarios, true);
        }

        public static string CarregarMensagemHtml(string templateEmail, Dictionary<string, string> parametros)
        {
            var streamReaderHtml = File.OpenText(templateEmail);
            var textoHtml = new StringBuilder();

            try
            {
                string linha = streamReaderHtml.ReadLine();

                while (linha != null)
                {
                    textoHtml.Append(linha);

                    linha = streamReaderHtml.ReadLine();
                }

                textoHtml = parametros.Aggregate(textoHtml, (current, parametro) => current.Replace(parametro.Key, parametro.Value));
            }
            finally
            {
                streamReaderHtml.Close();
            }

            return textoHtml.ToString();
        }
    }
}