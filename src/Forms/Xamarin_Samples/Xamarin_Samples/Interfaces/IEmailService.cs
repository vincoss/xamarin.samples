using System.Collections.Generic;

namespace Xamarin_Samples.Interfaces
{
    public interface IEmailService
    {
        void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody);
    }
}
