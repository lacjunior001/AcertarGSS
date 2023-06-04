namespace AcertarGSS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcao = -1;
            while (opcao != 0)
            {
                opcao = -1;
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("Bem vindo ao Acertar GSS");
                Console.WriteLine("===========================================");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1- Apenas Gerar Relatório.");
                Console.WriteLine("2- Gerar Relatório e tratar processos.");
                Console.WriteLine("0- Para sair.");

                string digito = Console.ReadLine();
                if (!int.TryParse(digito, out opcao))
                {
                    Console.WriteLine("Digite uma opção válida.");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    case 1:
                        //var uploadDocumento = new Models.GerarRelatorio();
                        throw new Exception("Não implementado");
                        break;
                    case 2:
                        var tratarProcs = new Models.TratarProcessos();
                        tratarProcs.Executar();
                        opcao = -1;
                        break;
                    default:
                        Console.WriteLine("Digite uma opção válida.");
                        Console.WriteLine("Aperte enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}