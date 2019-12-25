# File Manager - How to use it in ASP.NET Core Razor Pages

This example shows DevExtreme FileManager, which is bound to a default file sytem provider. The [page handler methods](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-3.1&tabs=visual-studio#multiple-handlers-per-page) work as backend here. 

*Files to look at*:

* [Index.cshtml](./CS/T846603/Pages/Index.cshtml)
* [Index.cshtml.cs](./CS/T846603/Pages/Index.cshtml.cs)
* [_Layout.cshtml](./CS/T846603/Pages/Shared/_Layout.cshtml)

> **Note** The project targets .NET Core 3.0. To run the project in Visual Studio 2017, change the target framework in the project settings.

## Implementation:

1) Add required libraries to your project:(https://docs.devexpress.com/AspNetCore/401026/devextreme-based-controls/get-started/configure-a-visual-studio-project)

2) Add File Manager control to your Razor Page and use the Remote provider.
```cs
@(Html.DevExtreme().FileManager()
    .FileProvider(provider =>
        provider.Remote()
        .Url(Url.Page("Index", "Documents")))
```

3) File Manager uses both GET and POST requests, so it's necessary to create handlers for both request types:
```cs
public IActionResult OnGetDocuments(FileSystemCommand c, string a) => ProcessFileApiRequest(c, a);
public IActionResult OnPostDocuments(FileSystemCommand c, string a) => ProcessFileApiRequest(c, a);
```
4) Process requests with IFileProvider like in the [Physical File System](https://demos.devexpress.com/ASPNetCore/Demo/FileManager/BindingToFileSystem/) demo. Since all data operations except Download return JSON data, return the result in the handler as follows:

```cs
 return command == FileSystemCommand.Download ? (IActionResult)result : new JsonResult(result);
```
5) Disable AntiForgeryToken in the Razor Page with File Manager via [IgnoreAntiforgeryTokenAttribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.ignoreantiforgerytokenattribute?view=aspnetcore-3.1). 


