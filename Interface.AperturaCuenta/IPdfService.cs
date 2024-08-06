using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.AperturaCuenta
{
    public  interface IPdfService
    {
        byte[] GeneratePdf(string content);
        Task SendPdfByEmailAsync(string toEmail, string subject, string body, byte[] pdfContent);
        Task<string> SendOtpByEmailAsync(string toEmail, string subject, string body);
    }
}
