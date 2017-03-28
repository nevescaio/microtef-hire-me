using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using StoneClassLibrary;
using static StoneClassLibrary.StoneMethods;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Globalization;

namespace Servidor
{
    public class Program
    {

        static void Main(string[] args)
        {
            try
            {
                //iniciar servidor no IP local, porta 8001
                IPAddress ip = GetLocalIPAddress();
                TcpListener listener = new TcpListener(ip, 8001);
                listener.Start();

                Console.WriteLine("Servidor LEGAL rodando em " + listener.LocalEndpoint);

                List<TransactionResult> transactions = new List<TransactionResult>();

                while (true)
                {
                    Console.WriteLine("Aguardando conexão do cliente");

                    //criar conexao com o cliente
                    Socket socket = listener.AcceptSocket();
                    Console.WriteLine("Conectado a " + socket.RemoteEndPoint);

                    //receber dados da transacao do cliente
                    byte[] receivedBytes = new byte[1000];
                    int k = socket.Receive(receivedBytes);
                    Array.Resize(ref receivedBytes, k);  
                        
                    //transformar json em objeto            
                    string json = Encoding.UTF8.GetString(receivedBytes);
                    Request request = new JavaScriptSerializer().Deserialize<Request>(json);
                    Console.WriteLine("Dados recebidos:");
                    Console.WriteLine(json);



                    switch (request.url) {

                        case "/transaction":
                            //validar dados da transacao
                            Transaction transaction = request.dataObject;
                            Return returnObj = transaction.validate();

                            //adicionar a lista de transacoes
                            if (returnObj.success)
                            {
                                CultureInfo ci = new CultureInfo("pt-BR");
                                TransactionResult transactionResult = new TransactionResult
                                {
                                    date = DateTime.Now.ToString(ci),
                                    cardholderName = transaction.card.cardholderName,
                                    number = transaction.card.number,
                                    amount = transaction.amount < 0 ? "" : transaction.amount.ToString("C", ci),
                                    type = transaction.type,
                                    instalments = transaction.number < 0 ? 0 : transaction.number,
                                    result = returnObj.message
                                };
                                transactions.Add(transactionResult);
                            }

                            //transformar objeto de retorno em json
                            json = new JavaScriptSerializer().Serialize(returnObj);

                            //enviar json ao cliente
                            socket.Send(Encoding.UTF8.GetBytes(json));
                            Console.WriteLine("Retorno enviado:");
                            Console.WriteLine(json + "\n");
                            break;



                        case "/transactionsList":
                            //transformar objeto de retorno em json
                            json = new JavaScriptSerializer().Serialize(transactions);

                            //enviar json ao cliente
                            socket.Send(Encoding.UTF8.GetBytes(json));
                            Console.WriteLine("Retorno enviado:");
                            Console.WriteLine(json + "\n");
                            break;
                    };

                    

                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
