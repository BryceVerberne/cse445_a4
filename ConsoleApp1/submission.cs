// © 2025 Bryce Verberne – CSE 445 Assignment 4
// Sharing or re‑using this code without attribution violates ASU academic‑integrity policy.

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        // List of GitHub URLS 
        public static string xmlURL = "https://bryceverberne.com/cse445_a4/Hotels.xml";
        public static string xmlErrorURL = "https://bryceverberne.com/cse445_a4/HotelErrors.xml";
        public static string xsdURL = "https://bryceverberne.com/cse445_a4/Hotels.xsd";

        public static void Main(string[] args)
        {
            // Verify a valid XML file (Hotels.xml)
            Console.WriteLine("Verifying Hotels.xml (should be OK) ...");
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            // Verify an intentionally invalid XML file (HotelErrors.xml)
            Console.WriteLine("\nVerifying HotelsErrors.xml (should show errors) ...");
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            // Convert both Hotels.xml to JSON
            Console.WriteLine("\nConverting Hotels.xml to JSON ...");
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Verify XML files against the Hotel.xsd schema
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            var errors = new List<string>();

            try
            {
                // Load the schema
                var schemas = new XmlSchemaSet();
                schemas.Add(null, xsdUrl);

                // Configure reader
                var settings = new XmlReaderSettings
                {
                    Schemas = schemas,
                    ValidationType = ValidationType.Schema,
                    ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
                };

                // Catch schema errors
                settings.ValidationEventHandler += (s, e) =>
                {
                    errors.Add(
                        $"Schema {e.Severity}: Line {e.Exception.LineNumber}, " +
                        $"Pos {e.Exception.LinePosition}: {e.Message}");
                };

                // Read through the document
                using (var reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) {}
                }
            }
            catch (XmlException xe) // Catch well-formedness errors
            {
                errors.Add(
                    $"XML well‑formedness error: Line {xe.LineNumber}, " +
                    $"Pos {xe.LinePosition}: {xe.Message}");
            }
            catch (Exception ex) // Catch every other error
            {
                errors.Add($"Exception: {ex.Message}");
            }

            // Print out all errors if present
            return errors.Count == 0
                ? "No Error"
                : string.Join(Environment.NewLine, errors);
        }

        // Converts an XML document to a formatted JSON string
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                // Load XML content from the GitHub URL
                var doc = new XmlDocument();
                doc.Load(xmlUrl);

                // Cleanup the JSON output
                if (doc.FirstChild is XmlDeclaration dec) doc.RemoveChild(dec);
                if (doc.DocumentElement != null)
                {
                    doc.DocumentElement.RemoveAttribute("xmlns:xsi");
                    doc.DocumentElement.RemoveAttribute("xsi:noNamespaceSchemaLocation");
                }

                // Format & return the JSON string
                string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
                return json.Replace("\"@", "\"");
            }
            catch (Exception ex) // Catch runtime exceptions
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
