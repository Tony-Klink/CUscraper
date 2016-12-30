using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace CUscraper
{
    /// <summary>
    /// Item class
    /// </summary>
    [DelimitedRecord("|")]
    public class shopItem
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }

        public shopItem()
        {
            Name = "";
            URL = "";
            Description = "";
            Image = "";
            Price = "";
        }

        public shopItem(string name, string url, string desc, string img, string price)
        {
            Name = name;
            URL = "https://www.computeruniverse.ru" + url; //append base url to link
            Description = desc;
            Image = img;
            Price = price;
        }
    }
}
