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
                    Transaction transaction = new JavaScriptSerializer().Deserialize<Transaction>(json);
                    Console.WriteLine("Dados recebidos:");
                    Console.WriteLine(json);

                    //validar dados da transacao
                    Return returnObj = transaction.validate();

                    //transformar objeto de retorno em json
                    json = new JavaScriptSerializer().Serialize(returnObj);

                    //enviar json ao cliente
                    socket.Send(Encoding.UTF8.GetBytes(json));
                    Console.WriteLine("Retorno enviado:");
                    Console.WriteLine(json);

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
