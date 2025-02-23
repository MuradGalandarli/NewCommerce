﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Abstractions.Storage.Local
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string path);
        bool HasFile(string pathOrContainerName,string fileName);
    
    }
}
