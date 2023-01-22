# JsonizeUpdate
Convert HTML to JSON with this .NET package. Update to Jsonize project.

### Version 3.1.0
#### CancellationTokens
From version 3.1.0, you can now pass a `CancellationToken`
to allow cancellation of the parsing methods.
This is an optional parameter, so will not break any existing code.

#### Streams
From version 3.1.0 you can now directly pass in a `Stream` object for your HTML to be parsed.
The methods accepting a `Stream` are overloads on the same method names as before.
There's no real performance increase, but you'll no longer have to process the `Stream` to a `string` yourself first!

### Deprecation Note:
Version `3.0.0` introduced a major performance regression that could make the run time many hundred times worse
(compared to version 1.0.9)!. 
Please upgrade to version `3.1.0` as `3.0.0` will be deprecated once the new package is pushed to NuGet.
You will also get some nice extra methods for working with `Stream` objects.

## Try it

Get the NuGet packages: 

| **Package**                                                                                | **Build Status**                                                                     | **NuGet Version**                                                                           |
|:-------------------------------------------------------------------------------------------|:-------------------------------------------------------------------------------------|:--------------------------------------------------------------------------------------------|
| [Jsonize](https://www.nuget.org/packages/Jsonize/)                                         | ![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg) | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize)                      |
| [Jsonize.Abstractions](https://www.nuget.org/packages/Jsonize.Abstractions/)               | ![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg) | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Abstractions)         |
| [Jsonize.Parser](https://www.nuget.org/packages/Jsonize.Parser/)                           | ![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg) | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Parser)               |
| [Jsonize.Serializer](https://www.nuget.org/packages/Jsonize.Serializer/)                   | ![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg) | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Serializer)           |
| [Jsonize.Serializer.Json.Net](https://www.nuget.org/packages/Jsonize.Serializer.Json.Net/) | ![.NET Core](https://github.com/JackWFinlay/jsonize/workflows/.NET%20Core/badge.svg) | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jsonize.Serializer.Json.Net)  |


## Usage

An example to get the site "https://jackfinlay.com" as a JSON string:

```C#
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
```

Alternatively, get the response as a `JsonizeNode`:

```C#
return jsonizer.ParseToJsonizeNodeAsync();
```

You can control the output with a `JsonizeParserConfiguration` object, which is passed as a parameter to the constructor of the `IJsonizeParser` of choice:

```C#
JsonizeParserConfiguration parserConfiguration = new JsonizeParserConfiguration()
{
    NullValueHandling = NullValueHandling.Ignore,
    EmptyTextNodeHandling = EmptyTextNodeHandling.Ignore,
    TextTrimHandling = TextTrimHandling.Trim,
    ClassAttributeHandling = ClassAttributeHandling.Array
}

JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
{
    Parser = new JsonizeParser(parserConfiguration),
    Serializer = new JsonizeSerializer()
};

Jsonizer jsonizer = new Jsonizer(jsonizeConfiguration);
```

Results are in the form:
```JSON
{
    "nodeType":"Node type e.g. Document, Element, or Comment",
    "tag":"If node is Element this will display the tag e.g p, h1 ,div etc.",
    "text":"If node is of type Text, this will display the text in that node.",
    "attr":{
                "name":"value",
                "class": []
            },
    "children":[
                {
                    "nodeType":"Node type e.g. Document, Element, or Comment",
                    "tag":"If node is Element this will display the tag e.g p, h1 ,div etc.",
                    "text":"If node is of type Text, this will display the text in that node.",
                    "child": []
                }
            ]
}
```

## License
MIT

See [license.md](license.md) for details.
