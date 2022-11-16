using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json_demo
{
    public class PersonFilms
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public List<string> FavouriteFilms { get; set; }

        public PersonFilms()
        {
            FavouriteFilms = new List<string>();
        }

        public PersonFilms(string name, int age, List<String> favouriteFilms)
        {
            this.Name = name;
            this.Age = age;
            this.FavouriteFilms = favouriteFilms;
        }
    }
}
