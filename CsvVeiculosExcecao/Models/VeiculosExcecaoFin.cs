using System;

namespace CsvVeiculosExcecao.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class VeiculosExcecaoFin
    {
        /// <summary>
        /// 
        /// </summary>
        public string CodigoFipe { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DesModMarcVers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DataCadastro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UsuarioUltManutencao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DataUltManutencao { get; set; }
    }

    public enum TipoRelatorio
    {
       CSV
    }
}