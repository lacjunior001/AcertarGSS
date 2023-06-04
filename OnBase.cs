using Hyland.Unity;
using static System.Formats.Asn1.AsnWriter;
using System.Text;
using Microsoft.Extensions.Configuration;
using Hyland.Unity.PhysicalRecords;

namespace AcertarGSS
{
    /// <summary>
    /// Classe que Gerencia a Conexão com OnBase.
    /// </summary>
    internal static class OnBase
    {
        /// <summary>
        /// App do OnBase.
        /// </summary>
        internal static Hyland.Unity.Application? App { get; set; }

        private static readonly string _servidor;
        private static readonly string _dataSource;
        private static string _usuario;
        private static string _senha;
        private static DateTime _ultimaUtilizacao;

        /// <summary>
        /// Abre Conexão se estiver fechada.
        /// </summary>
        internal static void ManutencaoConexao()
        {
            try
            {
                if (ConexaoAberta())
                {
                    _ultimaUtilizacao = DateTime.Now;
                }
                else
                {
                    Conectar();
                    _ultimaUtilizacao = DateTime.Now;
                }
            }
            catch (Exception e)
            {
                throw new Exception("OnBase. Manutenção da Conexão. ", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool ConexaoAberta()
        {
            bool aberta = true;
            try
            {
                if (App == null)
                {
                    aberta = false;
                }
                else
                {
                    if (App.IsConnected)
                    {
                        if (App.Ping())
                        {
                            aberta = true;
                        }
                        else
                        {
                            aberta = false;
                        }
                    }
                    else
                    {
                        aberta = false;
                    }
                }
            }
            catch (Exception e)
            {
                aberta = false;
            }

            return aberta;
        }

        /// <summary>
        /// Finaliza Conexão com OnBase.
        /// </summary>
        internal static void Finalizar()
        {
            //conexão só poderá ser finalizada se estiver 20 minutos parada.
            var validade = _ultimaUtilizacao.AddMinutes(18);
            if (validade.CompareTo(DateTime.Now) <= 0)
            {
                if (App != null)
                {
                    if (App.IsConnected)
                    {
                        App.Disconnect();
                    }

                    App.Dispose();
                }
            }
        }

        /// <summary>
        /// Abre conexão com OnBase. Se não estiver aberta.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void Conectar()
        {
            bool disconectado = true;

            while (disconectado)
            {
                Console.Clear();

                try
                {
                    if (string.IsNullOrWhiteSpace(_usuario))
                    {
                        Console.WriteLine("Digite o UserName do OnBase: ");
                        _usuario = Console.ReadLine();
                    }

                    if (string.IsNullOrWhiteSpace(_senha))
                    {
                        Console.WriteLine("Digite a Senha do OnBase: ");
                        string senha = "";
                        ConsoleKeyInfo key;

                        do
                        {
                            key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Backspace)
                            {
                                if (senha.Length > 0)
                                {
                                    senha = senha.Substring(0, senha.Length - 1);
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                    Console.Write(" ");
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                }
                            }
                            else if (key.Key != ConsoleKey.Enter)
                            {
                                senha += key.KeyChar;
                                Console.Write("*");
                            }

                        } while (key.Key != ConsoleKey.Enter);
                        _senha = senha;
                    }

                    var authProps = Application
                    .CreateOnBaseAuthenticationProperties(_servidor, _usuario, _senha, _dataSource);
                    App = Application.Connect(authProps);
                    Console.Clear();
                    disconectado = false;
                }
                catch (Exception e)
                {
                    disconectado = true;
                    Console.WriteLine("Erro ao conectar com OnBase:");
                    Console.WriteLine(e.ToString());
                    Console.ReadLine();
                }
            }
        }

        static OnBase()
        {
            _ultimaUtilizacao = DateTime.MinValue;
            var configurationManager = new Microsoft.Extensions.Configuration.ConfigurationManager();
            configurationManager
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            try
            {
                _servidor = configurationManager["Servidor"];
                _dataSource = configurationManager["DataSource"];
            }
            catch (Exception e)
            {

                throw new Exception("Serviços. OnBase. Construtor. ", e);
            }
        }
    }
}