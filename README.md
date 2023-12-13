<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T848278)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# File Manager for ASP.NET Core - How to use file manager in ASP.NET Core Razor Pages

This example demonstrates DevExtreme [FileManager](https://docs.devexpress.com/AspNetCore/401320/devextreme-based-controls/controls/file-manager) control bound to a default file sytem provider. The [page handler methods](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-3.1&tabs=visual-studio#multiple-handlers-per-page) work as backend. 

## Implementation Details

1. Add required libraries to your project. See the following topic for more details: [Configure a Visual Studio Project](https://docs.devexpress.com/AspNetCore/401026/devextreme-based-controls/get-started/configure-a-visual-studio-project).

2. Add the FileManager control to your Razor Page and specify the `Remote` provider.
    ```cs
    @(Html.DevExtreme().FileManager()
        .FileSystemProvider(provider =>
            provider.Remote()
            .Url(Url.Page("Index", "Documents")))
    ```

3. File Manager uses both `GET` and `POST` requests, so it is necessary to create handlers for both request types.

    ```cs
    public IActionResult OnGetDocuments(FileSystemCommand c, string a) => ProcessFileApiRequest(c, a);
    public IActionResult OnPostDocuments(FileSystemCommand c, string a) => ProcessFileApiRequest(c, a);
    ```
4. Process requests with [PhysicalFileSystemProvider](https://docs.devexpress.com/AspNetCore/DevExtreme.AspNet.Mvc.FileManagement.PhysicalFileSystemProvider) as demonstrated in the following demo: [Physical File System](https://demos.devexpress.com/ASPNetCore/Demo/FileManager/BindingToFileSystem/).
  
    All data operations except **Download** return JSON data. Return the result in the handler as follows:
    
    ```cs
    return command == FileSystemCommand.Download ? (IActionResult)result : new JsonResult(result);
    ```
5. Use the [IgnoreAntiforgeryTokenAttribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.ignoreantiforgerytokenattribute?view=aspnetcore-3.1) object to disable `AntiForgeryToken` on the Razor Page with File Manager.

## Files to Review

* [Index.cshtml](./CS/T846603/Pages/Index.cshtml)
* [Index.cshtml.cs](./CS/T846603/Pages/Index.cshtml.cs)
* [_Layout.cshtml](./CS/T846603/Pages/Shared/_Layout.cshtml)
