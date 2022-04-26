
using System;
using System.Net.Http;

namespace WebApiConsoleSample
{
    public abstract class BaseController<S> where S : BaseService
    {
        public S service;
        public BaseController(S s) { 
          this.service = s;
        }
        public abstract void Run();

    }
}