using MixERP.Net.VCards;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Types;
using System;
using System.Collections.Generic;
using System.IO;

public class VcfHelper
{
    public static List<VCard> SunamoVCardsToVCards(List<SunamoVCard> vc)
    {
        List<VCard> result = new List<VCard>();

        List<Telephone> tele = new List<Telephone>();
        List<Email> mails = new List<Email>();

        foreach (var i in vc)
        {
            tele.Clear();
            mails.Clear();

            foreach (var i2 in i.Telephones)
            {
                tele.Add(new Telephone { Number = i2.Number, Preference = i2.Preference, Type = EnumHelperVcf.Parse<TelephoneType>( i2.Type) });
            }

            foreach (var i2 in i.Emails)
            {
                mails.Add(new Email { EmailAddress = i2.EmailAddress, Type = EnumHelperVcf.Parse<EmailType>( i2.Type), Preference = i2.Preference });
            }

            result.Add(new VCard { FirstName = i.FirstName, MiddleName = i.MiddleName, LastName = i.LastName, Telephones = tele, Emails = mails });
        }

        return result;
    }

    public static void Serialize(string file, List<SunamoVCard> vc)
    {
        var con = SunamoVCardsToVCards(vc);
        var d = MixERP.Net.VCards.Serializer.VCardCollectionSerializer.Serialize(con);

        File.WriteAllText(file, d);
    }

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
                l.Add(new SunamoEmail { EmailAddress = i.EmailAddress, Preference = i.Preference, Type = EnumHelperVcf.Parse<SunamoEmailType>( i.Type) });
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
                l.Add(new SunamoTelephone { Number = i.Number, Type = EnumHelperVcf.Parse<SunamoTelephoneType>( i.Type), Preference = i.Preference });
            }
        }

        return l;
    }
}
