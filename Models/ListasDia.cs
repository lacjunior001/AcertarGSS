using Hyland.Unity;

namespace AcertarGSS.Models
{
    internal class ListasDia
    {
        internal List<Hyland.Unity.Document> ProcessosAbertura { get; set; }
        internal List<Hyland.Unity.Document> ProcessosAguardandoCA { get; set; }
        internal List<Hyland.Unity.Document> ProcessosAguardandoIntegracaoDetranNet { get; set; }
        internal List<Hyland.Unity.Document> ProcessosArquivados { get; set; }
        internal List<Hyland.Unity.Document> ProcessosArquivadosEmLote { get; set; }
        internal List<Hyland.Unity.Document> ProcessosCancelado { get; set; }
        internal List<Hyland.Unity.Document> ProcessosCanceladoEmLote { get; set; }
        internal List<Hyland.Unity.Document> ProcessosConferencia { get; set; }
        internal List<Hyland.Unity.Document> ProcessosConferenciaDespachante { get; set; }
        internal List<Hyland.Unity.Document> ProcessosConferenciaRenave { get; set; }
        internal List<Hyland.Unity.Document> ProcessosEntrega { get; set; }
        internal List<Hyland.Unity.Document> ProcessosEnviandoCA { get; set; }
        internal List<Hyland.Unity.Document> ProcessosPendencia { get; set; }
        internal List<Hyland.Unity.Document> ProcessosPendenciaDespachante { get; set; }
        internal List<Hyland.Unity.Document> ProcessosPendenciaRenave { get; set; }
        internal List<Hyland.Unity.Document> ProcessosSemSituacao { get; set; }

        public ListasDia(DateTime execucaoAtual)
        {
            this.ProcessosAbertura = new List<Hyland.Unity.Document>();
            this.ProcessosAguardandoCA = new List<Hyland.Unity.Document>();
            this.ProcessosAguardandoIntegracaoDetranNet = new List<Hyland.Unity.Document>();
            this.ProcessosArquivados = new List<Hyland.Unity.Document>();
            this.ProcessosArquivadosEmLote = new List<Hyland.Unity.Document>();
            this.ProcessosCancelado = new List<Hyland.Unity.Document>();
            this.ProcessosCanceladoEmLote = new List<Hyland.Unity.Document>();
            this.ProcessosConferencia = new List<Hyland.Unity.Document>();
            this.ProcessosConferenciaDespachante = new List<Hyland.Unity.Document>();
            this.ProcessosConferenciaRenave = new List<Hyland.Unity.Document>();
            this.ProcessosEntrega = new List<Hyland.Unity.Document>();
            this.ProcessosEnviandoCA = new List<Hyland.Unity.Document>();
            this.ProcessosPendencia = new List<Hyland.Unity.Document>();
            this.ProcessosPendenciaDespachante = new List<Hyland.Unity.Document>();
            this.ProcessosPendenciaRenave = new List<Hyland.Unity.Document>();
            this.ProcessosSemSituacao = new List<Hyland.Unity.Document>();

            OnBase.ManutencaoConexao();
            var dq = OnBase.App.Core.CreateDocumentQuery();
            var docType = OnBase.App.Core.DocumentTypes.Find("GSS - Processo Gerenciar Solicitações de Serviço");
            dq.AddDocumentType(docType);
            dq.AddDateRange(execucaoAtual, execucaoAtual);
            long contador = dq.ExecuteCount();
            if (contador > 0)
            {
                var docs = dq.Execute(contador);
                SepararProcPorEstado(docs);
            }
        }


        private void SepararProcPorEstado(DocumentList docs)
        {
            var ktStatus = OnBase.App.Core.KeywordTypes.Find("Status Processo SS");

            foreach (var item in docs)
            {
                var kwR = item.KeywordRecords.Find(ktStatus);
                if (kwR == null)
                {
                    ProcessosSemSituacao.Add(item);
                }
                else
                {
                    var kwSituacao = kwR.Keywords.Find(ktStatus);
                    string kwSituacaoValor = kwSituacao.AlphaNumericValue;

                    switch (kwSituacaoValor)
                    {
                        case "ABERTURA":
                            this.ProcessosAbertura.Add(item);
                            break;
                        case "AGUARDANDO CA":
                            this.ProcessosAguardandoCA.Add(item);
                            break;
                        case "AGUARDANDO INTEGRAÇÃO DETRANNET":
                            this.ProcessosAguardandoIntegracaoDetranNet.Add(item);
                            break;
                        case "ARQUIVADO":
                            this.ProcessosArquivados.Add(item);
                            break;
                        case "ARQUIVADO EM LOTE":
                            this.ProcessosArquivadosEmLote.Add(item);
                            break;
                        case "CANCELADO":
                            this.ProcessosCancelado.Add(item);
                            break;
                        case "CANCELADO EM LOTE":
                            this.ProcessosCanceladoEmLote.Add(item);
                            break;
                        case "CONFERÊNCIA":
                            this.ProcessosConferencia.Add(item);
                            break;
                        case "EM CONFERÊNCIA":
                            this.ProcessosConferencia.Add(item);
                            break;
                        case "CONFERÊNCIA DESPACHANTE":
                            this.ProcessosConferenciaDespachante.Add(item);
                            break;
                        case "EM CONFERÊNCIA DESPACHANTE":
                            this.ProcessosConferenciaDespachante.Add(item);
                            break;
                        case "CONFERÊNCIA RENAVE":
                            this.ProcessosConferenciaRenave.Add(item);
                            break;
                        case "EM CONFERÊNCIA RENAVE":
                            this.ProcessosConferenciaRenave.Add(item);
                            break;
                        case "ENTREGA":
                            this.ProcessosEntrega.Add(item);
                            break;
                        case "ENVIANDO CA":
                            this.ProcessosEnviandoCA.Add(item);
                            break;
                        case "PENDÊNCIA":
                            this.ProcessosPendencia.Add(item);
                            break;
                        case "PENDÊNCIA DESPACHANTE":
                            this.ProcessosPendenciaDespachante.Add(item);
                            break;
                        case "PENDÊNCIA RENAVE":
                            this.ProcessosPendenciaRenave.Add(item);
                            break;
                        default:
                            throw new Exception("Situação do processo tem valor inesperado!!");
                    }
                }
            }
        }
    }
}