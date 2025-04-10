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

        public HtmlViewModel GetPageById(string id)
        {
            var jsonPath = Path.Combine(_environment.ContentRootPath, "Data", "mock-data.json");
            var jsonData = File.ReadAllText(jsonPath);
            var data = JsonSerializer.Deserialize<JsonElement>(jsonData);

            foreach (var page in data.GetProperty("pages").EnumerateArray())
            {
                if (page.GetProperty("id").GetString() == id)
                {
                    var beneficiarios = new List<Beneficiario>();
                    if (page.TryGetProperty("beneficiarios", out var benefArray))
                    {
                        foreach (var b in benefArray.EnumerateArray())
                        {
                            beneficiarios.Add(new Beneficiario
                            {
                                Nome = b.GetProperty("nome").GetString(),
                                Parentesco = b.GetProperty("parentesco").GetString(),
                                Descricao = b.GetProperty("descricao").GetString(),
                                Porcentagem = b.GetProperty("porcentagem").GetString()
                            });
                        }
                    }

                    return new HtmlViewModel
                    {
                        Title = page.GetProperty("title").GetString(),
                        ApoliceNumero = page.GetProperty("apoliceNumero").GetString(),
                        Corretora = page.GetProperty("corretora").GetString(),
                        CodigoSeguradora = page.GetProperty("codigoSeguradora").GetString(),
                        PropostaNumero = page.GetProperty("propostaNumero").GetString(),
                        NomeProponente = page.GetProperty("nomeProponente").GetString(),
                        CPF = page.GetProperty("cpf").GetString(),
                        DataNascimento = page.GetProperty("dataNascimento").GetString(),
                        Idade = page.GetProperty("idade").GetString(),
                        NumeroRg = page.GetProperty("numeroRg").GetString(),
                        DataExpedicao = page.GetProperty("dataExpedicao").GetString(),
                        OrgaoEmissor = page.GetProperty("orgaoEmissor").GetString(),
                        UF = page.GetProperty("uf").GetString(),
                        Sexo = page.GetProperty("sexo").GetString(),
                        EstadoCivil = page.GetProperty("estadoCivil").GetString(),
                        CapitalSegurado = page.GetProperty("capital_segurado").GetString(),
                        PremioMorteCausasNaturais = page.GetProperty("premio_morte_causas_naturais").GetString(),
                        PremioInvalidezPermanenteAcidente = page.GetProperty("premio_invalidez_permanente_acidente").GetString(),
                        DataProposta = page.GetProperty("dataProposta").GetString(),
                        Agencia = page.GetProperty("agencia").GetString(),
                        MatriculaDV = page.GetProperty("matricula_dv").GetString(),
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
