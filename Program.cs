using HtmlAgilityPack;
using System.Net;
using System.Xml.Linq;

class Program
{
    static async Task Main()
    {

        string proxyAddress = "http://localhost";
        string targetUrl = "testAd.html";
        // Create an HttpClientHandler with the proxy settings
        HttpClientHandler handler = new HttpClientHandler
        {
            //Proxy = new WebProxy(proxyAddress),
            UseProxy = true,
        };
        HtmlDocument doc = new HtmlDocument();
        doc.Load(Path.Combine(Directory.GetCurrentDirectory(), targetUrl));
        // Create an HttpClient with the custom handler
        using (HttpClient client = new HttpClient(handler))
        {
            // Set the User-Agent header to mimic a web browser
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
            try
            {
                // Make the HTTP request
                //HttpResponseMessage response = await client.GetAsync(targetUrl);
                // Check if the request was successful (status code 200)
                //if (response.IsSuccessStatusCode)
                //{
                    //// Read the content of the response
                    //string htmlContent = await response.Content.ReadAsStringAsync();
                    //// Process the HTML content as needed
                 
                    string xpathExpression = "//dt"; ;
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpathExpression);
                    // Extract and print the data
                    if (nodes != null)
                    {
                        foreach (HtmlNode node in nodes)
                        {
                            string dtText = node.InnerText.Trim();
                            Console.Write($"{dtText}: ");
                            // Navigate to the corresponding <dd> element
                            HtmlNode ddNode = node.SelectSingleNode("following-sibling::dd[1]");
                            if (ddNode != null)
                            {
                                string ddText = ddNode.InnerText.Trim();
                                Console.WriteLine(ddText);
                            }
                            else
                            {
                                Console.WriteLine("No corresponding <dd> element found.");
                            }

                            //Console.WriteLine(node.InnerText.Trim());
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                //}
                //else
                //{
                //    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}

    
  