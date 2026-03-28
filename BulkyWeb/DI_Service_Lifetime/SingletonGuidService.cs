using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Service_Lifetime
{
    public class SingletonGuidService : ISingletonGuidService
    {
        private readonly Guid Id;

        public SingletonGuidService() { 
            Id = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
