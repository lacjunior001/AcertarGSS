namespace AcertarGSS.Models
{
    internal class CapaGss
    {
        internal long DocumentHandle { get; set; }
        internal DateTime Recebimento { get; set; }
        internal string NumDetranNet { get; set; }
        internal string AnoDetranNet { get; set; }
        internal string StatusProcSS { get; set; }
        internal string RespostaAuditoriaAutomatizada { get; set; }
        internal List<AtoGSS> AtosProcesso { get; set; }

        public CapaGss()
        {

        }
    }
}