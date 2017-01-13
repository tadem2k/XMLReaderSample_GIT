using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Net;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace XMLReaderSample_GIT
{
    class DocumentReader
    {
        public XmlDocument xmlDoc = new XmlDocument();
        private XmlReader xmlDocReader;
        private XmlNamespaceManager xmlNSMgr;
        //private static NameTable NameTbl = new NameTable();
        public string urlAddress { get; set; }

        public DocumentReader(string url)
        {
            /*
            xmlNSMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlNSMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            xmlNSMgr.AddNamespace("ns", "http://purl.org/rss/1.0/");
            */

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
                            // load all namespaces
                            xmlNSMgr = GetNameSpaceManager(xmlDoc);
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
        /*
         * 
         * Loading Namespaces from the file
         * 
         */
        private XmlNamespaceManager GetNameSpaceManager(XmlDocument xDoc)
        {
            XmlNamespaceManager nsm = new XmlNamespaceManager(xDoc.NameTable);
            XPathNavigator RootNode = xDoc.CreateNavigator();
            RootNode.MoveToFollowing(XPathNodeType.Element);
            IDictionary<string, string> NameSpaces = RootNode.GetNamespacesInScope(XmlNamespaceScope.All);

            foreach (KeyValuePair<string, string> kvp in NameSpaces)
            {
                if (kvp.Key.Length == 0)
                {
                    nsm.AddNamespace("ns", kvp.Value);
                }
                else
                {
                    nsm.AddNamespace(kvp.Key, kvp.Value);
                }
            }

            return nsm;
        }

        public void FED_RSS_Parser()
        {
            //XmlNodeList newsList = xmlDoc.DocumentElement.SelectNodes("/rdf:RDF/ns:item", xmlNSMgr);
            XmlNodeList newsList = xmlDoc.DocumentElement.SelectNodes("/rdf:RDF/ns:item", xmlNSMgr);
            
            //Console.WriteLine("doc: {0}", xmlDoc.OuterXml);
            Console.WriteLine("Parsing FED RSS Feed");
            Console.WriteLine("NodeListCount {0}", newsList.Count);
            foreach (XmlNode node in newsList)
            {
                Console.WriteLine("NODE: " + node.InnerXml);
                Console.WriteLine("preffix: " + node.Name.ToString());
                Console.WriteLine("______________________________________________");
                Console.WriteLine("FirstNodeURI: " + node.SelectSingleNode("/ns:item/ns:title", xmlNSMgr).InnerText);
                //Console.WriteLine("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_");
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
