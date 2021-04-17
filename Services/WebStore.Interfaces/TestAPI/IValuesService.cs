using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> Get();

        string Get(int id);

        Uri Create(string value);

        HttpStatusCode Edit(int id, string value);

        bool Remove(int id);
    }
}
