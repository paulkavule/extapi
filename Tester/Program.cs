
var request = "<soapenv:Envelopexmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:fac=\"http://facade.server.pilatus.thirdparty.tidis.muehlbauer.de/\""
+"xmlns:wsse= \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">"
+"<soapenv:Header>"
+ "<wsse:UsernameToken>"
+ "<wsse:Username>Test@ROOT</wsse:Username>"
+ "<wsse:Password Type=\"PasswordDigest\">ZMvfJzFWcWkrWGd10gz7wYVY/js=</wsse:Password>"
+"<wsse:Nonce>aYdz/Rbe/laaPKl1qPdaPQ==</wsse:Nonce>"
+"<wsse:Created>2018-04-06T19:32:31.543+03:00</wsse:Created>"
+"</wsse:UsernameToken>"
+"</soapenv:Header>"
+"<soapenv::Body>"
+"<!–- SOAP Body data -->"
+"</soapenv:Body>"
+"</soapenv:Envelope>";
try
{
    var client = new HttpClient();
    var content = new StringContent(request, System.Text.Encoding.UTF8, "text/xml");
    var postResp = await client.PostAsync("", content);
    if (postResp.IsSuccessStatusCode)
        Console.WriteLine("Error: " + postResp.StatusCode);
    var resultStr = await postResp.Content.ReadAsStringAsync();
    Console.WriteLine("Hurray: "+ resultStr);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
