namespace AcertarGSS.Models
{
    internal abstract class AtoGSS
    {
        internal long DocumentHandle { get; set; }
        internal DateTime DataRecebimento { get; set; }
        internal CapaGss Capa { get; set; }
    }
}