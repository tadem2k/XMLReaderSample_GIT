using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Net;
using System.Threading.Tasks;

namespace XMLReaderSample_GIT
{
    class DocumentReader
    {
        public XmlDocument xmlDoc = new XmlDocument();
        private XmlReader xmlDocReader;
        public string urlAddress { get; set; }

        public DocumentReader(string url)
        {
            urlAddress = (url);
            if (this.urlAddress.Length > 0)
            {
                try
                {
                    using (xmlDocReader = XmlReader.Create(this.urlAddress))
                    {
                        try
                        {
                            xmlDoc.Load(xmlDocReader);
                            Console.WriteLine("Document Loaded succefully");
                        }
                        catch (XmlSchemaValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                catch (WebException we)
                {
                    Console.WriteLine($"WebException: {we.Message}");
                }
            }
            else
            {
                Console.WriteLine("No URL Entered, check your address");
            }
        }

        public void FED_RSS_Parser()
        {
            XmlNodeList newsList = xmlDoc.SelectNodes("/item");

            Console.WriteLine("Parsing FED RSS Feed");
            Console.WriteLine("NodeListCount {0}", newsList.Count);
            foreach (XmlNode node in newsList)
            {
                Console.WriteLine($"Title: {node.SelectSingleNode("title").InnerText}");
                Console.WriteLine($"Message: {node.SelectSingleNode("description").InnerText}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            //Console

            DocumentReader docReader = new DocumentReader("https://www.federalreserve.gov/feeds/press_bcreg.xml");
            docReader.FED_RSS_Parser();
            //if (string.f)
                /*
                     <system.net>
                        <defaultProxy>
                          <proxy 
                            usesystemdefault="False"
                            proxyaddress="179.107.16.205:8080"
                          />
                        </defaultProxy>
                      </system.net>

                 * /

                /*
                XmlDocument xmlDoc = new XmlDocument();
                string nodeName = "";

                //http://www.w3schools.com/xml/note.xml
                //http://rss.nytimes.com/services/xml/rss/nyt/World.xml
                //http://feeds.reuters.com/reuters/businessNews?format=xml

                using (XmlReader myReader = XmlReader.Create("http://rss.nytimes.com/services/xml/rss/nyt/World.xml"))
                {
                    while (myReader.Read())
                    {
                        switch (myReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    nodeName = myReader.Name;
                                    //Console.WriteLine($"Node Name: {myReader.Name}");
                                    //Console.WriteLine($"Node Type: {myReader.NodeType.ToString()}");
                                    //Console.WriteLine($"Node Value: {myReader.Value}");
                                    //Console.WriteLine($"-================================================-");
                                    break;
                                }
                            case XmlNodeType.Text:
                                {
                                    Console.WriteLine($"Node Name: {nodeName}");
                                    Console.WriteLine($"Node Value: {myReader.Value}");
                                    Console.WriteLine($"-================================================-");
                                    break;
                                }
                        }
                    }


                }
                */
                Console.ReadLine();

        }
    }
}
