using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_BApi.Validating
{
    public class ExporterRepository : IDisposable
    {
        IzvozB_sEntities context = new IzvozB_sEntities();

        public Exporter ValidateExp(string username, string password)
        {
            return context.Exporters.FirstOrDefault(
                exp => exp.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && exp.Password == password);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}