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
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string DDD_primeiro { get; set; }
        public string DDD_segundo { get; set; }
        public string DDD_celular { get; set; }
        public string Telefone_primeiro { get; set; }
        public string Telefone_segundo { get; set; }
        public string Celular { get; set; }
        public string Pagamento_tipo { get; set; }
        public string Pagamento_meses { get; set; }
        public string Premio_total { get; set; }
        public string Operacao_cobertura { get; set; }
        public string NumeroContrato { get; set; }
        public string Cnpj { get; set; }
        public string Susep { get; set; }
        public string CodProd { get; set; }
        public string ViaCliente { get; set; }
        public string Data { get; set; }
        public string Prest { get; set; }


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
