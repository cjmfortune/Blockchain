using Nethereum.Web3;
using SD.WalletERC20.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SD.WalletERC20.Helper;

namespace SD.WalletERC20
{
    public class EtherumNetwork: NetworkBase
    {       
        public bool Connect(string login, string password)
        {
            this.Connection();
            var unlockAccountResoult = web3.Personal.UnlockAccount.SendRequestAsync(login, password, 120).Result;

            web3.TransactionManager.DefaultGasPrice = new BigInteger(500);
            web3.TransactionManager.DefaultGas = new BigInteger(1000);
            this.owner = login;
            return unlockAccountResoult;
        }

        public string CreateAccount(string password)
        {
            this.Connection();
            var login = this.web3.Personal.NewAccount.SendRequestAsync(password).Result;
            var unlockAccountResoultNotOWner = web3.Personal.UnlockAccount.SendRequestAsync(login, password, 120).Result;
            return login;
        }

        public decimal GetTokenBalance(string wallet)
        {
            this.Connection();
            var value = GetFunction<BigInteger>("balanceOf", wallet);
            return (decimal)value.WeiToEth();
        }

        public decimal GetEthBalance(string wallet)
        {
            var value = web3.Eth.GetBalance.SendRequestAsync(wallet).Result;
            return (decimal)value.Value.WeiToEth();
        }
    }
}
