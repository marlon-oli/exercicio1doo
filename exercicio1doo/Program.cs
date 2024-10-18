using System.Xml;
using Newtonsoft.Json;

namespace exercicio1doo
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlAlunos = "http://localhost:3000/alunos";
            string urlDisciplinas = "http://localhost:3000/disciplinas";
            string urlMatriculas = "http://localhost:3000/matriculas";
            string alunosPathXml = "alunos.xml";
            string disciplinasPathXml = "disciplinas.xml";
            string matriculasPathXml = "matriculas.xml";
            
            var alunos = GetAlunos(urlAlunos);
            var disciplinas = GetDisciplinas(urlDisciplinas);
            var matriculas = GetMatriculas(urlMatriculas);

            XmlWriterSettings settings = new XmlWriterSettings();
            
            
            
            // Exibir os dados
            Console.WriteLine("Alunos:");
            using (XmlWriter writer = XmlWriter.Create(alunosPathXml, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartDocument();
                writer.WriteStartElement("alunos");
                foreach (var aluno in alunos)
                {
                    writer.WriteAttributes();
                }
            }

            Console.WriteLine("\nDisciplinas:");
            foreach (var disciplina in disciplinas)
            {
                Console.WriteLine($"ID: {disciplina.Id}, Nome: {disciplina.Nome}, Descrição: {disciplina.Descricao}");
            }
            
            Console.WriteLine("\nMatriculas:");
            foreach (var matricula in matriculas)
            {
                Console.WriteLine($"ID: {matricula.Id}, IDAluno: {matricula.AlunoId}, IDDisciplina: {matricula.DisciplinaId}");
            }
            
            //escrever em um xml
            
        }
        
        
        static List<Aluno> GetAlunos(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(url).Result; // Usando .Result para tornar a chamada síncrona
                return JsonConvert.DeserializeObject<List<Aluno>>(response);
            }
        }

        static List<Disciplina> GetDisciplinas(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(url).Result; // Usando .Result para tornar a chamada síncrona
                return JsonConvert.DeserializeObject<List<Disciplina>>(response);
            }
        }

        static List<Matricula> GetMatriculas(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<List<Matricula>>(response);
            }
        }
    }

    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
    }

    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class Matricula
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
    }
}