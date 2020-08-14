using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomSearch.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using CustomSearch.Data;

namespace CustomSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchContext _context;
        private readonly ILogger<HomeController> _logger;


        public HomeController(SearchContext context,ILogger<HomeController> logger)
        {
           
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GoogleAction(string search)
        {
            string cx = "**********************";
            string apiKey = "********************";
            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" + cx + "&q=" + search);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            List<Google> results = new List<Google>();
            foreach (var item in jsonData.items)
            {
               results.Add(  new Google()
                {
                    Title = item.title,
                    Link = item.link,
                    Snippet = item.snippet,
                });

            }

        
            _context.GoogleResults.AddRange(results);
            _context.SaveChanges();
            return View(results);
        }


        public ActionResult BingAction(string search)
        {
            
            string customConfig = "******************";
            string subKey = "*************************";
            string market = "en-US";
            var request = WebRequest.Create(@"https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?q=" + search + "&customconfig=" + customConfig + "&mkt=" + market);
            request.Headers.Add("Ocp-Apim-Subscription-Key", "f44a57a462784b19b408f981508dcb01");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            List<Bing> results = new List<Bing>();
            foreach (var item in jsonData.webPages.value)
            {
                results.Add(new Bing
                {
                    Title = item.name,
                    Link = item.id,
                    Snippet = item.snippet,
                });
            }

            _context.BingResults.AddRange(results);
            _context.SaveChanges();
            return View(results);
        }


        /*
         * Yandex result may have some errors, due to Yandex Search API don't access to use Custom search, set a limit even a new account with new API
         * */

        public ActionResult YandexAction(string searchQuery)
        {

            string key = "********************";
            string user = "*********************";
            string url = @"https://yandex.ru/search/xml?user={0}&key={1}&query={2}&l10n=ru&sortby=tm.order%3Dascending&filter=strict&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1";
            string completeUrl = String.Format(url, user, key, searchQuery);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(completeUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
            XDocument xmlResponse = XDocument.Load(xmlReader);

            List<Yandex> ret = new List<Yandex>();

            var groupQuery = from gr in xmlResponse.Elements().
                          Elements("response").
                          Elements("results").
                          Elements("grouping").
                          Elements("group")
                             select gr;

            List<Yandex> results = new List<Yandex>();

            for (int i = 0; i < groupQuery.Count(); i++)
            {
                results.Add(new Yandex
                {
                    Link = GetValue(groupQuery.ElementAt(i), "url"),
                    Title = GetValue(groupQuery.ElementAt(i), "title"),
                    Snippet = GetValue(groupQuery.ElementAt(i), "headline"),

                });
            }
            _context.YendexResults.AddRange(results);
            _context.SaveChanges();
            return View(results);
        }

        public static string GetValue(XElement group, string name)
        {
            try
            {
                return group.Element("doc").Element(name).Value;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
