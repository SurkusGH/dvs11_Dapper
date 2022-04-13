using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace dvs11_lecture_Dapper
{
    internal class Program
    {
        public static string connString = $"Server=localhost;Database=dvs01_lecture_Intro;Trusted_Connection=True";
        static void Main(string[] args)
        {
            // (1) Sukurkite servisą, kuris enkapsuliuos SQL’o funkcionalumą nuo kliento, bet viduj savęs
            //     execute’ins Dapper metodus komunikuoti su duomenų baze.
            // (2) Naudokite praeitos paskaitos užduoties SP.
            // (3) Pabandykite C# metodus padaryti Generic.
            // (4) Pagalvokite, ką galima pernaudoti? O kas kartosis?

            // 5.Išrinkite visus duomenis apie darbuotojus, kurie dirba C# skyriuje.
            ExecuteSol_0105("C#");

            // 7.Išrinkite visus duomenis apie darbuotojus, kurių gimimo data -1986-09-19
            ExecuteSol_0107("1986-09-19");

            // 8.Išrinkite darbuotojų vardus, kurių pavardės yra Sabutis
            ExecuteSol_0108("Sabutis");

            // 9.Išrinkite duomenis(vardą ir pavardę) apie programuotojus iš Java skyriaus
            ExecuteSol_0109("JAVA");

            // 16.Suskaičiuokite, kiek įmonėje dirba Testuotojų.
            ExecuteSol_0116("Testuotojas");

        }

        /// <summary>
        /// Išrenka darbuotojus pagal jų priklausymą skyriuje;
        /// Galimos parinktys : "C#". "JAVA", "Testavimo"
        /// </summary>
        /// <param string ="input"></param>
        public static void ExecuteSol_0105(string input)
        {
            
            using var connection = new SqlConnection(connString);
            connection.Open();
            var sql = "EXECUTE [Proc_Sol_0105] @Lang;";
            var values = new { Lang = input };
            var data = connection.Query<DataType>(sql, values);

            Console.WriteLine($"{input} skyriaus darbuotojai: ");
            foreach (var row in data) Console.WriteLine($"* { row.AsmensKodas} - " +
                                                          $"{ row.Vardas} - " +
                                                          $"{ row.Pavarde} - " +
                                                          $"{ row.DirbaNuo} - " +
                                                          $"{ row.GimimoMetai} - " +
                                                          $"{ row.Skyrius_Pavadinimas} - " +
                                                          $"{ row.Projektas_ID}");
            Console.WriteLine("---");
        }

        /// <summary>
        /// Išrenka darbuotojus pagal jų gimimo datą;
        /// </summary>
        /// <param string ="input"></param>
        public static void ExecuteSol_0107(string input)
        {

            using var connection = new SqlConnection(connString);
            connection.Open();
            var sql = "EXECUTE [Proc_Sol_0107] @date;";
            var values = new { date = input };
            var data = connection.Query<DataType>(sql, values);

            Console.WriteLine($"{input} gimę darbuotojai: ");
            foreach (var row in data) Console.WriteLine($"* { row.AsmensKodas} - " +
                                                          $"{ row.Vardas} - " +
                                                          $"{ row.Pavarde} - " +
                                                          $"{ row.DirbaNuo} - " +
                                                          $"{ row.GimimoMetai} - " +
                                                          $"{ row.Skyrius_Pavadinimas} - " +
                                                          $"{ row.Projektas_ID}");
            Console.WriteLine("---");
        }

        /// <summary>
        /// Randa vardą pagal pavardę
        /// </summary>
        /// <param String="input"></param>
        public static void ExecuteSol_0108(string input)
        {

            using var connection = new SqlConnection(connString);
            connection.Open();
            var sql = "EXECUTE [Proc_Sol_0108] @LastName;";
            var values = new { LastName = input };
            var data = connection.Query<DataType_Name>(sql, values);

            Console.WriteLine($"Darbuotojo pavarde {input} vardas yra: ");
            foreach (var row in data) Console.WriteLine($"* { row.Vardas}");    
            Console.WriteLine("---");
        }

        /// <summary>
        /// Išrenka darbuotojus pagal jų priklausymą skyriuje;
        /// Galimos parinktys : "C#". "JAVA", "Testavimo"
        /// Grąžina vardą ir pavardę
        /// </summary>
        /// <param string="input"></param>
        public static void ExecuteSol_0109(string input)
        {

            using var connection = new SqlConnection(connString);
            connection.Open();
            var sql = "EXECUTE [Proc_Sol_0105] @Lang;";
            var values = new { Lang = input };
            var data = connection.Query<DataType_NameLastName>(sql, values);

            Console.WriteLine($"{input} skyriaus darbuotojai: ");
            foreach (var row in data) Console.WriteLine($"* { row.Vardas} -  { row.Pavarde}");                   
            Console.WriteLine("---");
        }

        /// <summary>
        /// Suskaičiuoja darbuotojus skyriuje;
        /// Galimos parinktys: "Projektų vadovas", "Programuotojas" ir "Testuotojas"
        /// </summary>
        /// <param string="input"></param>
        public static void ExecuteSol_0116(string input)
        {
            using var connection = new SqlConnection(connString);
            connection.Open();
            var sql = "SELECT COUNT(ALL [PAREIGOS]) FROM[dvs01_lecture_Intro].[dbo].[DARBUOTOJAS] WHERE[PAREIGOS] = @Occupation";
            var values = new { Occupation = input };
            var data = connection.QuerySingle<int>(sql, values); // <-- QuerySingle

            Console.WriteLine($"{input} skyriaus darbuotojų skaičius:");
            Console.WriteLine(data);
            Console.WriteLine("---");
        }
    }
}
