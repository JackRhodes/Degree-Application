using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Degree_Application.Interfaces
{

    /* Using: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads for file upload  */

    public interface IFileUpload
    {
        string ContentType { get; }
        string ContentDisposition { get; }
        IHeaderDictionary Headers { get; }
        long Length { get; }
        string Name { get; }
        string FileName { get; }
        Stream OpenReadStream();
        void CopyTo(Stream target);
        Task CopyToAsync(Stream target /*, CancellationToken cancel*/);
    }
}
