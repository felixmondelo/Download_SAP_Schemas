using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Download_SAP_Schemas
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "YOUR URL";
            string xsdPath = Path.GetTempPath();

            MainAsync(url, xsdPath).Wait();

            Console.WriteLine("Press RETURN to finish...");
            Console.ReadLine();
        }

        static async Task MainAsync(string url, string destinationPath)
        {            
            var httpClient = new HttpClient();
            
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            string responseText = await response.Content.ReadAsStringAsync();

            var convertedValue = JsonConvert.DeserializeObject<ListOfSchemas>(responseText);

            
            foreach (Schemas s in convertedValue.Schemas)
            {
                string name = s.Name;
                XmlDocument xmlSchema = new XmlDocument();

                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] clearString = Convert.FromBase64String(s.Content);
                    ms.Write(clearString, 0, clearString.Length);
                    ms.Position = 0;
                    xmlSchema.Load(ms);

                    string destinationFilename = string.Format("{0}{1}.xsd", destinationPath, name);                    
                    xmlSchema.Save(destinationFilename);
                }                
            }                        
        }
    }
}
