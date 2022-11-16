using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json_demo
{
    internal class PersonFilms2
    {
        [JsonProperty(PropertyName = "name", Order = 2)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age-in-years", Order = 1)]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "favourite-films", Order = 3)]
        public List<string> FavouriteFilms { get; set; }

        public PersonFilms2()
        {
            FavouriteFilms = new List<string>();
        }

        public PersonFilms2(string name, int age, List<String> favouriteFilms)
        {
            this.Name = name;
            this.Age = age;
            this.FavouriteFilms = favouriteFilms;
        }
    }
}
