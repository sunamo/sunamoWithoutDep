using MixERP.Net.VCards;
using MixERP.Net.VCards.Models;
using System;
using System.Collections.Generic;

public class VcfHelper
{
    public static List<SunamoVCard> Parse(string path)
    {
        IEnumerable<VCard> vcards = MixERP.Net.VCards.Deserializer.Deserialize(path);
        List<SunamoVCard> vc = new List<SunamoVCard>();
        foreach (var item in vcards)
        {
            SunamoVCard v = new SunamoVCard();
            v.Emails = Emails( item.Emails);
            v.Telephones = Telephones( item.Telephones);
            v.FirstName = item.FirstName;
            v.MiddleName = item.MiddleName;
            v.LastName = item.LastName;

            vc.Add(v);
        }

        return vc;
    }

    private static IEnumerable<SunamoEmail> Emails(IEnumerable<Email> emails)
    {
        List<SunamoEmail> l = new List<SunamoEmail>();
        if (emails != null)
        {
            foreach (var i in emails)
            {
                l.Add(new SunamoEmail { EmailAddress = i.EmailAddress, Preference = i.Preference, Type = i.Type.ToString() });
            }
        }

        return l;
    }

    

    public static IEnumerable<SunamoTelephone> Telephones(IEnumerable<Telephone> telephones)
    {
        List<SunamoTelephone> l = new List<SunamoTelephone>();
        if (telephones != null)
        {
            foreach (var i in telephones)
            {
                l.Add(new SunamoTelephone { Number = i.Number, Type = i.Type.ToString(), Preference = i.Preference });
            }
        }

        return l;
    }
}

