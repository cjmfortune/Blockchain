using SD.WalletERC20.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SD.WalletERC20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EtherumNetwork eth = null;
        public MainWindow()
        {
            InitializeComponent();
            eth = new EtherumNetwork();
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            eth.contractAddress = this.Contract.Text;
            var balance = eth.GetTokenBalance(this.Wallet.Text);
            this.BalanceToken.Text = balance.EthToText();

            var balanceETH = eth.GetEthBalance(this.Wallet.Text);
            this.BalanceEth.Text = balanceETH.EthToText();
        }
    }
}
