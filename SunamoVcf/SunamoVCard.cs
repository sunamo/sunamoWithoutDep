using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SunamoVCard
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<SunamoTelephone> Telephones { get; set; }
    public IEnumerable<SunamoEmail> Emails { get; set; }

    public bool wrapTelInQm = false;



    public string TelephonesToString()
    {
        var tel = string.Empty;
        if (Telephones != null)
        {
            var tels = Telephones.Select(t => t.Number);
            if (wrapTelInQm)
            {
                tels = CA.ChangeContent(new ChangeContentArgs { }, tels.ToList(), SH.WrapWithQm);
            }

            tel = SH.Join(tels, ",");
        }
        return tel;
    }

    public string EmailsToString()
    {
        var mail = string.Empty;
        if (Emails != null)
        {
            mail = SH.Join(Emails.Select(t => t.EmailAddress), ",");
        }
        return mail;
    }

    public override string ToString()
    {
        var fn = EmptyIfNull(FirstName);
        var mn = EmptyIfNull(MiddleName);
        var ln = EmptyIfNull(LastName);

        var tel = TelephonesToString();
        var mail = EmailsToString();

        return $"{fn} {mn} {ln} {tel} {mail}";
    }

    

    private object EmptyIfNull(string firstName)
    {
        if (firstName == null)
        {
            return string.Empty;
        }
        return firstName;
    }
}