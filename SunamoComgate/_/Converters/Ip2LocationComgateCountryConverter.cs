using SunamoComgate;
using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THsoftware.ComGate.Core.Domain.Enums;

public class Ip2LocationComgateCountryConverter
{
    public static Country ToComgate(string i)
    {
        var sk = Ip2LocationsCountries.sk;
        var cz = Ip2LocationsCountries.cz;
        var pl = Ip2LocationsCountries.pl;

        if (CA.IsEqualToAnyElement<string>(i, sk, cz, pl))
        {
            return (Country)Enum.Parse(typeof(Country), i);
        }
        return Country.ALL;
    }
}