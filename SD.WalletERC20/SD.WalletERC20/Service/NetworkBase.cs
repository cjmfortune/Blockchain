using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SD.WalletERC20.Service
{
    public class NetworkBase
    {
        public string contractAddress = null;
        public string owner = null;
        protected string abi = new Abi().GetERC20();
        protected Web3 web3 = null;
        
        protected void Connection()
        {
            if (this.web3 != null)
            {
                return;
            }

            this.web3 = new Web3();
            this.web3.TransactionManager.DefaultGasPrice = new BigInteger(0x4c4b40);
            this.web3.TransactionManager.DefaultGas = new BigInteger(0x4c4b40);
        }

        protected T GetFunction<T>(string functionName)
        {
            var contract = web3.Eth.GetContract(abi, contractAddress);
            var function = contract.GetFunction(functionName);

            var resoult = function.CallAsync<T>().Result;
            if (resoult == null)
            {
                return default(T);
            }

            return resoult;
        }

        protected T GetFunction<T>(string functionName, params object[] parameters)
        {
            if (string.IsNullOrEmpty(contractAddress))
            {
                return default(T);
            }

            var contract = web3.Eth.GetContract(abi, contractAddress);
            var function = contract.GetFunction(functionName);

            var resoult = function.CallAsync<T>(parameters).Result;
            if (resoult == null)
            {
                return default(T);
            }

            return resoult;
        }

        protected void SendFunction(string functionName)
        {
            try
            {
                var contract = web3.Eth.GetContract(abi, contractAddress);
                var function = contract.GetFunction(functionName);
                HexBigInteger gas = new HexBigInteger(210000);
                HexBigInteger value = new HexBigInteger(0);

                var result = function.SendTransactionAsync(this.owner, gas, value).Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void SendFunction(string functionName, params object[] parameters)
        {
            try
            {
                var contract = web3.Eth.GetContract(abi, contractAddress);
                var function = contract.GetFunction(functionName);
                HexBigInteger gas = new HexBigInteger(210000);
                HexBigInteger value = new HexBigInteger(0);

                var result = function.SendTransactionAsync(this.owner, gas, value, parameters).Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
