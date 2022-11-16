using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace json_demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Writing and reading a strongly typed class
            PersonFilms personFilms = new PersonFilms
            {
                Name = "Andrew",
                Age = 50,
                FavouriteFilms = new List<string>() {
                    "Toy Story",
                    "Toy Story 2",
                    "Aliens"
                }
            };
            string pfs = JsonConvert.SerializeObject(personFilms, Formatting.Indented);
            Console.WriteLine(pfs);
            PersonFilms pfs2 = JsonConvert.DeserializeObject<PersonFilms>(pfs);
            Console.WriteLine(pfs2.Name);
            Console.WriteLine(pfs2.Age);
            pfs2.FavouriteFilms.ForEach(f => Console.WriteLine(f));

            // Writing an anonymous object to file
            var obj1 = new
            {
                name = "Maria",
                age = 39,
                favouriteFilms = new List<string>() {
                    "Love Story",
                    "Inception",
                    "It's a Wonderful Life"
                }
            };
            string s1 = JsonConvert.SerializeObject(obj1);
            string json1 = JsonConvert.SerializeObject(obj1, Formatting.Indented);
            File.WriteAllText("file1.json", json1);

            // Reading JSON into an anonymous, dynamic object then picking out elements
            string s2 = File.ReadAllText("file1.json");
            dynamic obj2 = JsonConvert.DeserializeObject(s2);
            Console.WriteLine(obj2.name);
            Console.WriteLine(obj2.age);
            Console.WriteLine(obj2.favouriteFilms);

            // Alternative way to read elements, if they are non-standard (include dots or hyphens)
            int age = obj2["age"];
            Console.WriteLine(age);

            // Use JsonProperty attributes to control the mapping of property
            // names to JSON names (name, age-in-years, favourite-films)
            // Note serialisation order specified in class via the JsonPropertyAttribute Order property
            PersonFilms2 pf2 = new PersonFilms2("Sharmi", 27, new List<string> { "Top Gun", "Die Hard", "ET" });
            s2 = JsonConvert.SerializeObject(pf2, Formatting.Indented);

            Console.WriteLine(s2);
            PersonFilms2 pd2 = JsonConvert.DeserializeObject<PersonFilms2>(s2);
            string name = pd2.Name;
            age = pd2.Age;
            List<string> favouriteFilms = pd2.FavouriteFilms;
            Console.WriteLine(name);
            Console.WriteLine(age);
            favouriteFilms.ForEach(f => Console.WriteLine(f));

            dynamic pdd = JsonConvert.DeserializeObject(s2);
            string namedd = pdd["name"];
            int ageInYearsdd = pdd["age-in-years"];
            List<string> favouriteFilmsdd = pdd["favourite-films"].ToObject<List<string>>();
            Console.WriteLine(namedd);
            Console.WriteLine(ageInYearsdd);
            favouriteFilmsdd.ForEach(f => Console.WriteLine(f));

            // Deserialization of unrecognised elements is ignored (not an error)
            //Console.WriteLine(obj2.agex);
            Console.WriteLine($"{(obj2.agex != null ? obj2.agex : string.Empty)}");
        }
    }
}
