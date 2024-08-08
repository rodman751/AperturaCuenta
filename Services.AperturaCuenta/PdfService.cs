using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using iText.Kernel.Pdf; // Clase de iText 7.x
using iText.Html2pdf; // Asegúrate de usar iText 7.x para HTML to PDF
using MailKit.Security;
using Interface.AperturaCuenta;


namespace Services.AperturaCuenta
{
    public class PdfService: IPdfService
    {
        public byte[] GeneratePdf(string htmlContent)
        {
            //using (var stream = new MemoryStream())
            //{
            //    Document document = new Document();
            //    PdfWriter.GetInstance(document, stream);
            //    document.Open();
            //    document.Add(new Paragraph(content));
            //    document.Close();
            //    return stream.ToArray();
            //}
            using (var stream = new MemoryStream())
            {
                var writerProperties = new WriterProperties();
                using (var pdfWriter = new PdfWriter(stream, writerProperties))
                {
                    using (var pdfDocument = new PdfDocument(pdfWriter))
                    {
                        var converterProperties = new ConverterProperties();
                        HtmlConverter.ConvertToPdf(htmlContent, pdfDocument, converterProperties);
                    }
                }
                return stream.ToArray();
            }
        }

        public async Task SendPdfByEmailAsync(string toEmail, string subject, string body, byte[] pdfContent)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Rodman", "jg38903@gmail.com")); // Nombre y dirección de correo electrónico del remitente
            message.To.Add(new MailboxAddress("Recipient Name", toEmail)); // Nombre y dirección de correo electrónico del destinatario
            message.Subject = subject;

            var builder = new BodyBuilder { TextBody = body };

            using (var stream = new MemoryStream(pdfContent))
            {
                builder.Attachments.Add("document.pdf", stream.ToArray(), new MimeKit.ContentType("application", "pdf")); // Especifica MimeKit.ContentType
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls); // Usa StartTls para seguridad
                await client.AuthenticateAsync("jg38903@gmail.com", "ollk yvqv nxlk ebwc"); // Proporciona tus credenciales de Gmail
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        public async Task<string> SendOtpByEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Rodman", "jg38903@gmail.com"));
            message.To.Add(new MailboxAddress("Recipient Name", toEmail)); 
            message.Subject = subject;

            // Generar un código OTP aleatorio de 6 dígitos
            var random = new Random();
            int otp = random.Next(100000, 999999);

            // Agregar el OTP al cuerpo del mensaje
            body += $"\n\nTu código OTP es: {otp}";

            var builder = new BodyBuilder { TextBody = body };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls); 
                await client.AuthenticateAsync("jg38903@gmail.com", "ollk yvqv nxlk ebwc"); 
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            // Retornar el OTP para que pueda ser almacenado
            return otp.ToString();
        }





    }
}
