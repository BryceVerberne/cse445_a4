using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        // 1. Provide your GitHub URLs here:
        public static string xmlURL = "https://bryceverberne.com/cse445_a4/Hotels.xml";
        public static string xmlErrorURL = "https://bryceverberne.com/cse445_a4/HotelErrors.xml";
        public static string xsdURL = "https://bryceverberne.com/cse445_a4/Hotels.xsd";

        public static void Main(string[] args)
        {
            Console.WriteLine("Verifying Hotels.xml (should be OK) ...");
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            Console.WriteLine("\nVerifying HotelsErrors.xml (should show errors) ...");
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            Console.WriteLine("\nConverting Hotels.xml to JSON ...");
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                var schemas = new XmlSchemaSet();
                schemas.Add(null, xsdUrl);

                var settings = new XmlReaderSettings
                {
                    Schemas = schemas,
                    ValidationType = ValidationType.Schema
                };

                var errors = new List<string>();
                settings.ValidationEventHandler += (s, e) =>
                {
                    errors.Add($"Line {e.Exception.LineNumber}, Pos {e.Exception.LinePosition}: {e.Message}");
                };

                // Validate the XML
                using (var reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { /* read through doc */ }
                }

                return (errors.Count == 0) ? "No Error" 
                                           : string.Join(Environment.NewLine, errors);
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(xmlUrl);

                if (doc.FirstChild is XmlDeclaration)
                    doc.RemoveChild(doc.FirstChild);

                string json = JsonConvert.SerializeXmlNode(doc, Formatting.Indented);

                return json.Replace("\"@", "\"_");
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
