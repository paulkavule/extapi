

using System.Security.Cryptography;

using System.Text;
using static System.Net.Mime.MediaTypeNames;

string Hash(string input)
{
    var enc = Encoding.GetEncoding(0);

    byte[] buffer = enc.GetBytes(input);
    var sha1 = SHA1.Create();
    var hash = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");
    return hash;
}
var nonce = "120003023";
var password = "67Dg!46n";
var username = "MinistryofEnergy@TPI";
var datetime = DateTime.Now.ToString("yyyy-MM-dd");
var bytes = Encoding.UTF8.GetBytes(nonce + datetime + Hash(password));
var digest = Convert.ToBase64String(bytes);

var request = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:fac=\"http://facade.server.pilatus.thirdparty.tidis.muehlbauer.de/\""
+"xmlns:wsse= \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">"
+"<soapenv:Header>"
+ "<wsse:UsernameToken>"
+ "<wsse:Username>"+username+"</wsse:Username>"
+ "<wsse:Password Type=\"PasswordDigest\">"+digest+"</wsse:Password>"
+ "<wsse:Nonce>aYdz/Rbe/laaPKl1qPdaPQ==</wsse:Nonce>"
+ "<wsse:Created>2018-04-06T19:32:31.543+03:00</wsse:Created>"
+ "</wsse:UsernameToken>"
+ "</soapenv:Header>"
+ "<soapenv::Body>"
+ "<getPerson xmlns=\"http://facade.server.pilatus.thirdparty.tidis.muehlbauer.de/\">"
+ "<request>"
+ "<nationalId>CM93012102CR7F</nationalId>"
+ "</request>"
+ "</getPerson>"
+ "</soapenv:Body>"
+"</soapenv:Envelope>";
try
{
    var client = new HttpClient();
    var content = new StringContent(request, System.Text.Encoding.UTF8, "text/xml");
    var postResp = await client.PostAsync("http://196.0.118.1:8080/pilatusp2-tpi2-ws/ThirdPartyInterfaceNewWS", content);
    if (postResp.IsSuccessStatusCode)
        Console.WriteLine("Error: " + postResp.StatusCode);
    var resultStr = await postResp.Content.ReadAsStringAsync();
    Console.WriteLine("Hurray: "+ resultStr);
}
catch (Exception ex)
{
    Console.WriteLine("Exception: "+ex.Message);
}
