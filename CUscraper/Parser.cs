using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CUscraper
{
    class Parser
    {
        List<string> names;
        List<string> links;
        List<string> descriptions;
        List<string> image_links;
        List<string> prices;
        List<shopItem> shopItems;

        public Parser()
        {
            
        }

        /// <summary>
        /// Get list of shopItems; returns List<shopItem>
        /// </summary>
        /// <param name="page">number of pages to parse</param>
        /// <param name="itemgroupid">group id</param>
        public async Task<List<shopItem>> GetItemsFromPage(int page, int itemgroupid)
        {
            HtmlWeb web = new HtmlWeb();
            shopItems = new List<shopItem>();
            string baseUrl = "";

            for (int j = 1; j < page+1; j++)
            {
                //Reinitialize lists on every iteration
                names = new List<string>();
                links = new List<string>();
                descriptions = new List<string>();
                image_links = new List<string>();
                prices = new List<string>();
                
                baseUrl = "https://www.computeruniverse.ru/groups/p" + j.ToString() + "/" + itemgroupid.ToString();
                var doc = await Task.Factory.StartNew(() => web.Load(baseUrl));

                //Parse document for Names
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*//div//div//h2//a//span"))
                {                    
                    names.Add(link.InnerText);
                }

                //Parse document for item Links
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*//div//div//h2//a"))
                {
                    links.Add(link.Attributes["href"].Value);
                }

                //Parse document for item Description
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*[contains(@class,'props')]"))
                {
                    descriptions.Add(link.InnerText);
                }

                //Parse document for item's images links
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*//div[1]/div/a/img/@data-original"))
                {
                    image_links.Add(link.Attributes["data-original"].Value);
                }

                //Parse document for item price
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*//div[3]/div/div/font[1]"))
                {
                    prices.Add(link.InnerText.TrimEnd("&nbsp;&euro;".ToCharArray()));
                }

                //Fill shopItems list with parsed data
                for (int i = 0; i < names.Count; i++)
                {
                    shopItems.Add(new shopItem(names[i], links[i], descriptions[i], image_links[i], prices[i]));
                }
            }
            return shopItems;
        }       
    }
}
