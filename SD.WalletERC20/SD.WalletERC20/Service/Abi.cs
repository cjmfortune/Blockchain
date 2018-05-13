using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.WalletERC20
{
    public class Abi
    {
        public string GetERC20()
        {
            string abiPath = "ERC20.abi";
            return System.IO.File.ReadAllText(abiPath);
            //return "[{\"constant\": true,\"inputs\": [{\"name\": \"_owner\",\"type\": \"address\"}],\"name\": \"balanceOf\",\"outputs\": [{\"name\": \"balance\",\"type\": \"uint256\"}],\"payable\": false,\"stateMutability\": \"view\",\"type\": \"function\"}]";
        }
    }
}
