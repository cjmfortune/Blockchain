using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SD.WalletERC20.Helper
{
    public static class EthereumExtension
    {
        public static decimal WeiToEth(this BigInteger value)
        {
            BigInteger devider = new BigInteger(1000000000000);
            var val =BigInteger.Divide(value, devider);
            decimal valDecimal = (decimal)val / 1000000;
            return valDecimal;
        }

        public static string EthToText(this decimal value)
        {
            var val = value.ToString("### ### ##0.00# ### ###");
            return val;
        }
    }
}
