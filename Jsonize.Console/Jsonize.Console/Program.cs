using Jsonize;
using Jsonize.Parser;
using Jsonize.Serializer;

var url = @"https://jackfinlay.com";
Console.WriteLine(await JsonizeTest(url));

static async Task<string> JsonizeTest(string url)
{
    using var client = new HttpClient();
    var response = await client.GetAsync(url);

    var html = await response.Content.ReadAsStringAsync();
    var parser = new JsonizeParser();
    var serializer = new JsonizeSerializer();
    var jsonizer = new Jsonizer(parser, serializer);

    return await jsonizer.ParseToStringAsync(html);
}