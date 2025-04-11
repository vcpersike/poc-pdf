using System.Diagnostics;
using System.Text.Json;
using EstudosEmPdf.Models;

namespace EstudosEmPdf.Services
{
    public class HtmlService
    {
        private readonly IWebHostEnvironment _environment;
        private List<HtmlViewModel> _pages;

        public HtmlService(IWebHostEnvironment environment)
        {
            _environment = environment;
           
        }

        private string TryGet(JsonElement element, string key)
        {
            return element.TryGetProperty(key, out var value) ? value.GetString() ?? "" : "";
        }


        public HtmlViewModel GetPageById(string id)
        {
            var jsonPath = Path.Combine(_environment.ContentRootPath, "Data", "mock-data.json");
            var jsonData = File.ReadAllText(jsonPath);
            var data = JsonSerializer.Deserialize<JsonElement>(jsonData);

            foreach (var page in data.GetProperty("pages").EnumerateArray())
            {
                if (TryGet(page, "id") == id)
                {
                    var beneficiarios = new List<Beneficiario>();
                    if (page.TryGetProperty("beneficiarios", out var benefArray))
                    {
                        foreach (var b in benefArray.EnumerateArray())
                        {
                            beneficiarios.Add(new Beneficiario
                            {
                                Nome = TryGet(b, "nome"),
                                Parentesco = TryGet(b, "parentesco"),
                                Descricao = TryGet(b, "descricao"),
                                Porcentagem = TryGet(b, "porcentagem")
                            });
                        }
                    }

                    return new HtmlViewModel
                    {
                        Title = TryGet(page, "title"),
                        ApoliceNumero = TryGet(page, "apoliceNumero"),
                        Corretora = TryGet(page, "corretora"),
                        CodigoSeguradora = TryGet(page, "codigoSeguradora"),
                        PropostaNumero = TryGet(page, "propostaNumero"),
                        NomeProponente = TryGet(page, "nomeProponente"),
                        CPF = TryGet(page, "cpf"),
                        DataNascimento = TryGet(page, "dataNascimento"),
                        Idade = TryGet(page, "idade"),
                        NumeroRg = TryGet(page, "numeroRg"),
                        DataExpedicao = TryGet(page, "dataExpedicao"),
                        OrgaoEmissor = TryGet(page, "orgaoEmissor"),
                        UF = TryGet(page, "uf"),
                        Sexo = TryGet(page, "sexo"),
                        EstadoCivil = TryGet(page, "estadoCivil"),
                        CapitalSegurado = TryGet(page, "capital_segurado"),
                        PremioMorteCausasNaturais = TryGet(page, "premio_morte_causas_naturais"),
                        PremioInvalidezPermanenteAcidente = TryGet(page, "premio_invalidez_permanente_acidente"),
                        DataProposta = TryGet(page, "dataProposta"),
                        Agencia = TryGet(page, "agencia"),
                        MatriculaDV = TryGet(page, "matricula_dv"),
                        Logradouro = TryGet(page, "logradouro"),
                        LogoPath = TryGet(page, "logoPath"),
                        Cep = TryGet(page, "cep"),
                        Bairro = TryGet(page, "bairro"),
                        Cidade = TryGet(page, "cidade"),
                        Email = TryGet(page, "email"),
                        DDD_primeiro = TryGet(page, "ddd_primeiro"),
                        Telefone_primeiro = TryGet(page, "telefone_primeiro"),
                        DDD_segundo = TryGet(page, "ddd_segundo"),
                        Telefone_segundo = TryGet(page, "telefone_segundo"),
                        DDD_celular = TryGet(page, "telefone_celular"),
                        Celular = TryGet(page, "telefone_celular"),
                        Pagamento_tipo = TryGet(page, "pagamento_tipo"),
                        Pagamento_meses = TryGet(page, "pagamento_meses"),
                        Premio_total = TryGet(page, "premio_total"),
                        Operacao_cobertura = TryGet(page, "operacao_cobertura"),
                        NumeroContrato = TryGet(page, "numeroContrato"),
                        Cnpj = TryGet(page, "cnpj"),
                        Susep = TryGet(page, "susep"),
                        CodProd = TryGet(page, "codProd"),
                        ViaCliente = TryGet(page, "viaCliente"),
                        Data = TryGet(page, "data"),
                        Prest = TryGet(page, "prest"),
                        Beneficiarios = beneficiarios
                    };
                }
            }

            return null;
        }


        public string RenderHtml(string id)
        {
            var page = GetPageById(id);

            if (page == null)
            {
                return "<html><body><h1>Página não encontrada</h1></body></html>";
            }

            var templatePath = Path.Combine(_environment.ContentRootPath, "Data", "template.html");
            var template = File.ReadAllText(templatePath);
            var imagePath = Path.Combine(_environment.WebRootPath, "images", "caixa_vida_previdencia_2d_vertical.png");
            string base64Image = Convert.ToBase64String(File.ReadAllBytes(imagePath));
            string imageDataUri = $"data:image/png;base64,{base64Image}";

            var beneficiariosHtml = "";
            foreach (var b in page.Beneficiarios)
            {
                beneficiariosHtml += $"<tr>" +
                    $"<td>{b.Nome}</td>" +
                    $"<td>{b.Parentesco}</td>" +
                    $"<td>{b.Descricao}</td>" +
                    $"<td>{b.Porcentagem}%</td>" +
                    $"</tr>";
            }

            var renderedHtml = template
                .Replace("{{title}}", page.Title)
                .Replace("{{apoliceNumero}}", page.ApoliceNumero)
                .Replace("{{corretora}}", page.Corretora)
                .Replace("{{codigoSeguradora}}", page.CodigoSeguradora)
                .Replace("{{propostaNumero}}", page.PropostaNumero)
                .Replace("{{nomeProponente}}", page.NomeProponente)
                .Replace("{{cpf}}", page.CPF)
                .Replace("{{dataNascimento}}", page.DataNascimento)
                .Replace("{{idade}}", page.Idade)
                .Replace("{{numeroRg}}", page.NumeroRg)
                .Replace("{{dataExpedicao}}", page.DataExpedicao)
                .Replace("{{orgaoEmissor}}", page.OrgaoEmissor)
                .Replace("{{uf}}", page.UF)
                .Replace("{{sexo}}", page.Sexo)
                .Replace("{{estadoCivil}}", page.EstadoCivil)
                .Replace("{{capital_segurado}}", page.CapitalSegurado)
                .Replace("{{premio_morte_causas_naturais}}", page.PremioMorteCausasNaturais)
                .Replace("{{premio_invalidez_permanente_acidente}}", page.PremioInvalidezPermanenteAcidente)
                .Replace("{{dataProposta}}", page.DataProposta)
                .Replace("{{agencia}}", page.Agencia)
                .Replace("{{matricula_dv}}", page.MatriculaDV)
                .Replace("{{logradouro}}", page.Logradouro)
                .Replace("{{logoPath}}", imageDataUri)
                .Replace("{{cep}}", page.Cep)
                .Replace("{{bairro}}", page.Bairro)
                .Replace("{{cidade}}", page.Cidade)
                .Replace("{{email}}", page.Email)
                .Replace("{{ddd_primeiro}}", page.DDD_primeiro)
                .Replace("{{telefone_primeiro}}", page.Telefone_primeiro)
                .Replace("{{ddd_segundo}}", page.DDD_segundo)
                .Replace("{{telefone_segundo}}", page.Telefone_segundo)
                .Replace("{{ddd_celular}}", page.DDD_celular)
                .Replace("{{telefone_celular}}", page.Celular)
                .Replace("{{pagamento_tipo}}", page.Pagamento_tipo)
                .Replace("{{pagamento_meses}}", page.Pagamento_meses)
                .Replace("{{premio_total}}", page.Premio_total)
                .Replace("{{operacao_cobertura}}", page.Operacao_cobertura)
                .Replace("{{numeroContrato}}", page.NumeroContrato)
                .Replace("{{cnpj}}", page.Cnpj)
                .Replace("{{susep}}", page.Susep)
                .Replace("{{codProd}}", page.CodProd)
                .Replace("{{viaCliente}}", page.ViaCliente)
                .Replace("{{data}}", page.Data)
                .Replace("{{prest}}", page.Prest)
                .Replace("{{beneficiariosTable}}", beneficiariosHtml);


            return renderedHtml;
        }

        public List<HtmlViewModel> GetAllPages()
        {
            return _pages;
        }

        public string GetRawTemplate()
        {
            var templatePath = Path.Combine(_environment.WebRootPath, "templates", "template.html");
            return File.ReadAllText(templatePath);
        }

        public string RenderHtmlWithExternalCss(string id)
        {
            var page = GetPageById(id);
            if (page == null)
            {
                return "<html><body><h1>Página não encontrada</h1></body></html>";
            }

            var templatePath = Path.Combine(_environment.ContentRootPath, "Data", "template.html");
            var template = File.ReadAllText(templatePath);

            var imagePath = Path.Combine(_environment.WebRootPath, "images", "caixa_vida_previdencia_2d_vertical.png");
            string base64Image = Convert.ToBase64String(File.ReadAllBytes(imagePath));
            string imageDataUri = $"data:image/png;base64,{base64Image}";

            var beneficiariosHtml = "";
            foreach (var b in page.Beneficiarios)
            {
                beneficiariosHtml += $"<tr>" +
                    $"<td>{b.Nome}</td>" +
                    $"<td>{b.Parentesco}</td>" +
                    $"<td>{b.Descricao}</td>" +
                    $"<td>{b.Porcentagem}%</td>" +
                    $"</tr>";
            }

            var renderedHtml = template
                .Replace("{{title}}", page.Title)
                .Replace("{{apoliceNumero}}", page.ApoliceNumero)
                .Replace("{{corretora}}", page.Corretora)
                .Replace("{{codigoSeguradora}}", page.CodigoSeguradora)
                .Replace("{{propostaNumero}}", page.PropostaNumero)
                .Replace("{{nomeProponente}}", page.NomeProponente)
                .Replace("{{cpf}}", page.CPF)
                .Replace("{{dataNascimento}}", page.DataNascimento)
                .Replace("{{idade}}", page.Idade)
                .Replace("{{numeroRg}}", page.NumeroRg)
                .Replace("{{dataExpedicao}}", page.DataExpedicao)
                .Replace("{{orgaoEmissor}}", page.OrgaoEmissor)
                .Replace("{{uf}}", page.UF)
                .Replace("{{sexo}}", page.Sexo)
                .Replace("{{estadoCivil}}", page.EstadoCivil)
                .Replace("{{capital_segurado}}", page.CapitalSegurado)
                .Replace("{{premio_morte_causas_naturais}}", page.PremioMorteCausasNaturais)
                .Replace("{{premio_invalidez_permanente_acidente}}", page.PremioInvalidezPermanenteAcidente)
                .Replace("{{dataProposta}}", page.DataProposta)
                .Replace("{{agencia}}", page.Agencia)
                .Replace("{{matricula_dv}}", page.MatriculaDV)
                .Replace("{{logradouro}}", page.Logradouro)
                .Replace("{{logoPath}}", imageDataUri)
                .Replace("{{cep}}", page.Cep)
                .Replace("{{bairro}}", page.Bairro)
                .Replace("{{cidade}}", page.Cidade)
                .Replace("{{email}}", page.Email)
                .Replace("{{ddd_primeiro}}", page.DDD_primeiro)
                .Replace("{{telefone_primeiro}}", page.Telefone_primeiro)
                .Replace("{{ddd_segundo}}", page.DDD_segundo)
                .Replace("{{ddd_segundo}}", page.DDD_segundo)
                .Replace("{{telefone_segundo}}", page.Telefone_segundo)
                .Replace("{{ddd_celular}}", page.DDD_celular)
                .Replace("{{telefone_celular}}", page.Celular)
                .Replace("{{pagamento_tipo}}", page.Pagamento_tipo)
                .Replace("{{pagamento_meses}}", page.Pagamento_meses)
                .Replace("{{premio_total}}", page.Premio_total)
                .Replace("{{operacao_cobertura}}", page.Operacao_cobertura)
                .Replace("{{numeroContrato}}", page.NumeroContrato)
                .Replace("{{cnpj}}", page.Cnpj)
                .Replace("{{susep}}", page.Susep)
                .Replace("{{codProd}}", page.CodProd)
                .Replace("{{viaCliente}}", page.ViaCliente)
                .Replace("{{data}}", page.Data)
                .Replace("{{prest}}", page.Prest)
                .Replace("{{beneficiariosTable}}", beneficiariosHtml);

            var cssPath = Path.Combine(_environment.WebRootPath, "css", "styles.css");
            var cssContent = File.ReadAllText(cssPath);

            var htmlWithInlineCss = $"<style>{cssContent}</style>{renderedHtml}";

            return htmlWithInlineCss;
        }

        public void GerarPdfViaNode(string htmlContent)
        {
            var htmlPath = Path.Combine(_environment.WebRootPath, "output", "entrada.html");
            var scriptPath = Path.Combine(_environment.ContentRootPath, "PdfScripts", "gerar-pdf.js");

            Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
            File.WriteAllText(htmlPath, htmlContent);

            var processInfo = new ProcessStartInfo
            {
                FileName = "node",
                Arguments = $"\"{scriptPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processInfo);

            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("STDOUT:\n" + stdout);
                Console.WriteLine("STDERR:\n" + stderr);
                throw new Exception("Erro ao gerar PDF: " + stderr);
            }

            Console.WriteLine("PDF gerado com sucesso:\n" + stdout);
        }


    }
}
