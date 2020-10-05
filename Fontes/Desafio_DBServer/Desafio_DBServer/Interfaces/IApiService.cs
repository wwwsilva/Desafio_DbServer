using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_DBServer.Interfaces
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string url) where T : class;

        Task<byte[]> GetImageBytesAsync(string url);
    }
}
