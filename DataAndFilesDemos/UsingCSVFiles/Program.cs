using System;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper;

namespace csvFile_demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Reading and displaying a list of people from a CSV file
            using (var sr = new StreamReader("movies.csv"))
            using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var list = reader.GetRecords<Movie>().ToList();
                foreach (Movie m in list)
                {
                    Console.WriteLine($"{m.Title} is {m.ReleaseYear}");
                }
            }

            // Writing a list to a CSV file
            var more_movies = new Movie[] {
                new Movie { Title = "2001: A Space Odyssey", ReleaseYear = 1968 },
                new Movie { Title = "Dark Star", ReleaseYear = 1975 },
                new Movie { Title = "The Martian", ReleaseYear = 2015 }
            };

            using (var sw = new StreamWriter("updated_movies.csv"))
            using (var writer = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                writer.WriteRecords(more_movies);
            }

            // Reading records into dynamic class (NB: every property value will be a string!)
            Console.WriteLine();
            using (var sr = new StreamReader("movies.csv"))
            using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var list = reader.GetRecords<dynamic>().ToList();
                foreach (var m in list)
                {
                    Console.WriteLine($"{m.Title} is {m.ReleaseYear})");
                }
            }
        }
    }
}
