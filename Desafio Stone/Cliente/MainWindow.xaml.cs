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
using System.IO;
using System.Net;
using System.Net.Sockets;
using StoneClassLibrary;
using static StoneClassLibrary.StoneMethods;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            TcpClient client = new TcpClient();

            try
            {
                //conectar ao servidor no IP local, porta 8001
                client.Connect(GetLocalIPAddress().ToString(), 8001);
                Stream stream = client.GetStream();

                //criar objeto de transacao com os dados informados na tela
                Transaction transaction = new Transaction
                {
                    amount = String.IsNullOrEmpty(amount.Text) ? -1 : Decimal.Parse(amount.Text),
                    type = type.Text,
                    card = new Card
                    {
                        cardholderName = cardholderName.Text,
                        number = cardNumber.Text,
                        expirationDate = expirationDate.Text,
                        cardBrand = cardBrand.Text,
                        password = password.Password,
                        type = cardType.Text,
                        hasPassword = (bool)hasPassword.IsChecked
                    },
                    number = number.SelectedIndex
                };

                //transformar objeto em json
                var json = new JavaScriptSerializer().Serialize(transaction);

                //transmitir json ao servidor
                byte[] sentBytes = Encoding.UTF8.GetBytes(json);
                stream.Write(sentBytes, 0, sentBytes.Length);

                //receber retorno do servidor
                byte[] receivedBytes = new byte[100];
                int k = stream.Read(receivedBytes, 0, 100);
                Array.Resize(ref receivedBytes, k);

                //transformar json em objeto
                json = Encoding.UTF8.GetString(receivedBytes);
                Return returnObj = new JavaScriptSerializer().Deserialize<Return>(json);

                //exibir o retorno do servidor
                MessageBox.Show(returnObj.message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                client.Close();
            }
        }

        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //quando selecionar "credito", habilitar escolha de parcelas
            if (type.SelectedIndex == 0)
            {
                number.IsEnabled = true;
                number.SelectedIndex = 0;
            }
            else
            {
                number.IsEnabled = false;
                number.SelectedIndex = -1;
            }
        }

        private void cardType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //quando selecionar "tarja magnetica", habilitar hasPassword
            if (cardType.SelectedIndex == 1)
            {
                hasPassword.IsEnabled = true;
            }
            else
            {
                hasPassword.IsEnabled = false;
                hasPassword.IsChecked = false;
            }
        }

        private void hasPassword_Click(object sender, RoutedEventArgs e)
        {
            //quando selecionar hasPassword, desabilitar campo password
            if (hasPassword.IsChecked == true)
            {
                password.IsEnabled = false;
            }
            else
            {
                password.IsEnabled = true;
            }
        }

        private void cardNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //pertmitir apenas numeros
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //permitir apenas numeros
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //permitir apenas numeros, '.' e ','
            Regex regex = new Regex("[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void expirationDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //permitir apenas numeros, '/'
            Regex regex = new Regex("[^0-9/]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
