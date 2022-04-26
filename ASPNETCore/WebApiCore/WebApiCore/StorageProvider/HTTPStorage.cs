using C1.Web.Api.Storage;
using System.IO;
using System.Net;
using System;
using System.Net.Http;

namespace WebApi
{
    internal class HTTPStorage : IFileStorage
    {
        public string Name { get; private set; }
        public string Path { get; set; }

        public HTTPStorage(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public bool Exists
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return true;
            }
        }

        public Stream Read()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(Path).Result;
                response.EnsureSuccessStatusCode();
                using (var stream = response.Content.ReadAsStreamAsync().Result)
                using (var reader = new BinaryReader(stream))
                {
                    var buffer = reader.ReadBytes((int)stream.Length);
                    return new MemoryStream(buffer);
                }
            }
        }

        public void Write(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
