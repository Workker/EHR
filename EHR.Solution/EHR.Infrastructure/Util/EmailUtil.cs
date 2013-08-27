using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System;

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
            message.From = new MailAddress(ConfigurationManager.AppSettings["Remetente"]);
            message.IsBodyHtml = true;
            message.Body = mensagem;

            var smtpClient = new SmtpClient
                                 {
                                     Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortaSMTP"]),
                                     Host = Convert.ToString(ConfigurationManager.AppSettings["HostSMTP"]),
                                     Credentials = new NetworkCredential(
                                         Convert.ToString(ConfigurationManager.AppSettings["Usuario"]),
                                         Convert.ToString(ConfigurationManager.AppSettings["Senha"]))
                                 };

            smtpClient.Send(message);
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