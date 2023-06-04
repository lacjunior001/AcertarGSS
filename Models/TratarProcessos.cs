using Models;

namespace AcertarGSS.Models
{
    internal class TratarProcessos
    {
        internal DateTime DataInicio { get; set; }
        internal DateTime DataFim { get; set; }
        internal DateTime Atual { get; set; }
        internal DateTime DoisAnosAntes { get; set; }

        internal TratarProcessos()
        {
            DataInicio = DateTime.MinValue;
            DataFim = DateTime.MinValue;
            Atual = DateTime.Now.Date;
            DoisAnosAntes = Atual.AddYears(-2);
        }

        internal void Executar()
        {
            bool inicioValido = false;
            bool fimValido = false;

            while (!(inicioValido && fimValido))
            {
                Console.Clear();
                this.DataInicio = DateTime.MinValue;
                this.DataFim = DateTime.MinValue;
                inicioValido = false;
                fimValido = false;
                Console.WriteLine("===========================================");
                Console.WriteLine("Bem vindo ao Acertar GSS");
                Console.WriteLine("===========================================");
                Console.WriteLine("Opção de Tratar Processos");
                Console.WriteLine("===========================================");
                Console.WriteLine("A range de datas que será buscada é a de abertura do processo no OnBase.");
                Console.WriteLine("Insira o período:");
                Console.WriteLine("Ex: Data Inicio: 01/02/2023");
                Console.WriteLine("Ex: Data Fim: 12/03/2023");
                Console.WriteLine("===========================================");
                Console.WriteLine("Digite a data de início:");
                string dataInicioString = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dataInicioString))
                {
                    Console.WriteLine("Valor Nulo");
                    Console.WriteLine("Aperte enter para continuar.");
                    Console.ReadLine();
                    continue;
                }
                else if (DateTime.TryParse(dataInicioString, out DateTime dataInicio))
                {
                    this.DataInicio = dataInicio;
                    if (this.DataInicio < this.DoisAnosAntes)
                    {
                        Console.WriteLine("Data inicial da busca é de mais de 2 anos atrás.");
                        Console.WriteLine("Aperte enter para continuar.");
                        Console.ReadLine();
                        continue;
                    }
                    else
                    {
                        inicioValido = true;
                    }
                }
                else
                {
                    dataInicioString = dataInicioString.Trim().ToLower();
                    if ("hoje".Equals(dataInicioString))
                    {
                        this.DataInicio = this.Atual;
                        inicioValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Não é um formato válido de data.");
                        Console.WriteLine("Aperte enter para continuar.");
                        Console.ReadLine();
                        continue;
                    }
                }

                Console.WriteLine("Digite a data de fim:");
                string dataFimString = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dataFimString))
                {
                    Console.WriteLine("Valor Nulo");
                    Console.WriteLine("Aperte enter para continuar.");
                    Console.ReadLine();
                    continue;
                }
                else if (DateTime.TryParse(dataFimString, out DateTime dataFim))
                {
                    this.DataFim = dataFim;
                    if (this.DataFim < this.DataInicio)
                    {
                        Console.WriteLine("Data fim não pode ser menor que a data inicio.");
                        Console.WriteLine("Aperte enter para continuar.");
                        Console.ReadLine();
                        continue;
                    }
                    else
                    {
                        fimValido = true;
                    }
                }
                else
                {
                    dataFimString = dataFimString.Trim().ToLower();
                    if ("hoje".Equals(dataFimString))
                    {
                        this.DataFim = this.Atual;
                        fimValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Não é um formato válido de data.");
                        Console.WriteLine("Aperte enter para continuar.");
                        Console.ReadLine();
                        continue;
                    }
                }
            }

            DateTime execucaoAtual = this.DataInicio;

            while (execucaoAtual <= this.DataFim)
            {
                var processos = new ListasDia(execucaoAtual);
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("Bem vindo ao Acertar GSS");
                Console.WriteLine("===========================================");
                Console.WriteLine("Opção de Tratar Processos");
                Console.WriteLine("===========================================");
                Console.WriteLine($"Período Escolhido {this.DataInicio} --> {this.DataFim}");
                Console.WriteLine("===========================================");
                Console.WriteLine($"Consultando data {execucaoAtual}");
                Console.WriteLine("===========================================");
                Console.WriteLine("Processos Encontrados: ");
                Console.WriteLine($"ABERTURA: {processos.ProcessosAbertura.Count} ");
                Console.WriteLine($"AGUARDANDO CA: {processos.ProcessosAguardandoCA.Count} ");
                Console.WriteLine($"AGUARDANDO INTEGRAÇÃO DETRANNET: {processos.ProcessosAguardandoIntegracaoDetranNet.Count} ");
                Console.WriteLine($"ARQUIVADO: {processos.ProcessosArquivados.Count} ");
                Console.WriteLine($"ARQUIVADO EM LOTE: {processos.ProcessosArquivadosEmLote.Count} ");
                Console.WriteLine($"CANCELADO: {processos.ProcessosCancelado.Count} ");
                Console.WriteLine($"CANCELADO EM LOTE: {processos.ProcessosCanceladoEmLote.Count} ");
                Console.WriteLine($"CONFERÊNCIA: {processos.ProcessosConferencia.Count} ");
                Console.WriteLine($"CONFERÊNCIA DESPACHANTE: {processos.ProcessosConferenciaDespachante.Count} ");
                Console.WriteLine($"CONFERÊNCIA RENAVE: {processos.ProcessosConferenciaRenave.Count} ");
                Console.WriteLine($"ENTREGA: {processos.ProcessosEntrega.Count} ");
                Console.WriteLine($"ENVIANDO CA: {processos.ProcessosEnviandoCA.Count} ");
                Console.WriteLine($"PENDÊNCIA: {processos.ProcessosEntrega.Count} ");
                Console.WriteLine($"PENDÊNCIA DESPACHANTE: {processos.ProcessosPendenciaDespachante.Count} ");
                Console.WriteLine($"PENDÊNCIA RENAVE: {processos.ProcessosPendenciaRenave.Count} ");


                Console.ReadLine();
                string parada = " ";
            }
        }
    }
}