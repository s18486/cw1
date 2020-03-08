using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {           
            if (args.Length == 0)
                throw new ArgumentNullException();
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(args[0]);
                if (response.IsSuccessStatusCode)
                {
                    var tmp = await response.Content.ReadAsStringAsync();
                    var reg = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                    var matches = reg.Matches(tmp);
                    if (matches.Count == 0)
                        throw new Exception("Nie znaleziono adresów email");
                    HashSet<string> set = new HashSet<string>();
                    foreach (var match in matches)                    
                        set.Add(match.ToString());
                    foreach(var st in set)
                        Console.WriteLine(st);
                }
                httpClient.Dispose();
            }
            catch(InvalidOperationException argex) {
                throw new ArgumentException();
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Bład w czasie pobierania strony");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
