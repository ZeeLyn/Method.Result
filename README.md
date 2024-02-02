# Method.Result
 
It returns an object indicating success or failure of an operation instead of throwing/using exceptions.

# Packages & Status

| Packages                         | NuGet                                                                                                                                               |Note|
| -------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------- |---|
| Method.Return.Results         | [![NuGet package](https://buildstats.info/nuget/Method.Return.Results )](https://www.nuget.org/packages/Method.Return.Results )             

```csharp
public Result<int> Create1(string name, string address)
{
    if (string.IsNullOrWhiteSpace(name))
    {
        return Result.Fail("Name is required!");
    }

    if (string.IsNullOrWhiteSpace(address))
    {
        return Result.Fail("Address is required!");
    }

    //.....
    return Result.Ok(123);
}

public Result<int> Create2(string name, string address)
{
    var result = Result.Fail(
        string.IsNullOrWhiteSpace(name) ? "Name is required!" : string.Empty,
        string.IsNullOrWhiteSpace(address) ? "Address is required!" : string.Empty
    );
    if (result.Failed)
        return result;
    //.....
    return Result.Ok(123);
}

public Result<int> Create3(string name, string address)
{
    var result = Result.Merge(
        Result.FailIf(string.IsNullOrWhiteSpace(name), "Name is required!"),
        Result.FailIf(string.IsNullOrWhiteSpace(address), "Address is required!")
    );
    if (result.Failed)
        return result;
    //.....
    return Result.Ok(123);
}
```