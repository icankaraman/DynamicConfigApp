using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLibrary.Lib.Services
{
    public interface IConfigurationService
    {
        T GetValue<T>(string key);
        
    }
}
