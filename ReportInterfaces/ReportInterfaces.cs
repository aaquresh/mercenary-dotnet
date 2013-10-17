using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportInterfaces
{
    //Abstract Factory Interface
    public interface IAbstractReportFactory
    {
        IAbstractReportProduct CreateReport(string Json);
    }

    //Abstract Product Interface
    public interface IAbstractReportProduct
    {
        string Run();
    }
}
