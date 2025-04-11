namespace EstudosEmPdf.Models
{
    public class HtmlViewModel
    {
        public string Title { get; set; }
        public string ApoliceNumero { get; set; }
        public string Corretora { get; set; }
        public string CodigoSeguradora { get; set; }
        public string PropostaNumero { get; set; }
        public string NomeProponente { get; set; }
        public string CPF { get; set; }
        public string DataNascimento { get; set; }
        public string Idade { get; set; }
        public string NumeroRg { get; set; }
        public string DataExpedicao { get; set; }
        public string OrgaoEmissor { get; set; }
        public string UF { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string CapitalSegurado { get; set; }
        public string PremioMorteCausasNaturais { get; set; }
        public string PremioInvalidezPermanenteAcidente { get; set; }
        public string DataProposta { get; set; }
        public string Agencia { get; set; }
        public string MatriculaDV { get; set; }
        public string Logradouro { get; set; }
        public string LogoPath { get; set; }

        public List<Beneficiario> Beneficiarios { get; set; }

    }

    public class Beneficiario
    {
        public string Nome { get; set; }
        public string Parentesco { get; set; }
        public string Descricao { get; set; }
        public string Porcentagem { get; set; }
    }

}
