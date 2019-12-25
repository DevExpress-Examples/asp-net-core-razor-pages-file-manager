using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace T846603.Pages {
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel {
        protected IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env) {
            _env = env;
        }

        public void OnGet() { }
        public IActionResult OnGetDocuments(FileSystemCommand command, string arguments) => ProcessFileApiRequest(command, arguments);

        public IActionResult OnPostDocuments(FileSystemCommand command, string arguments) => ProcessFileApiRequest(command, arguments);
        private IActionResult ProcessFileApiRequest(FileSystemCommand command, string arguments) {
            string documentsRoot = $"{_env.WebRootPath }\\Pictures";
            var config = new FileSystemConfiguration {
                Request = Request,
                FileSystemProvider = new DefaultFileProvider(documentsRoot),
                AllowDownload = true,
                AllowCopy = true,
                AllowCreate = true,
                AllowMove = true,
                AllowRemove = true,
                AllowRename = true,
                AllowUpload = true,
                UploadTempPath = $"{_env.ContentRootPath}\\UploadTemp"
            };
            var processor = new FileSystemCommandProcessor(config);
            var result = processor.Execute(command, arguments).GetClientCommandResult();
            return command == FileSystemCommand.Download ? (IActionResult)result : new JsonResult(result);
        }
    }
}
