using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcertarGSS.Models
{
    internal class SituacaoEntrega
    {
        /// <summary>
        /// Verifica se os processos com situação entrega estão de acordo com a regra de negócio.
        /// </summary>
        /// <param name=""></param>
        /// <exception cref="NotImplementedException"></exception>
        internal static void VerificarCoformidade(List<Hyland.Unity.Document> documentos)
        {
            var foraConformidade = new List<Hyland.Unity.Document>();
            var ktNumDetranNet = OnBase.App.Core.KeywordTypes.Find("Número Processo DetranNet");
            var ktAnoDetranNet = OnBase.App.Core.KeywordTypes.Find("Ano Processo DetranNet");

            foreach (var doc in documentos)
            {
                var kwr = doc.KeywordRecords.Find(ktNumDetranNet);
                if (kwr == null)
                {
                    foraConformidade.Add(doc);
                    continue;
                }
                string numDetranNet = kwr.Keywords.Find(ktNumDetranNet).AlphaNumericValue;
                kwr = doc.KeywordRecords.Find(ktAnoDetranNet);
                if (kwr == null)
                {
                    foraConformidade.Add(doc);
                    continue;
                }
                string anoDetranNet = kwr.Keywords.Find(ktAnoDetranNet).AlphaNumericValue;

                var processoGSS = new ProcessoGSS(numDetranNet, anoDetranNet);
                
            }
        }
    }
}
