using System.Collections.Generic;
using SignalrSqlDependency1.Models;

namespace SignalrSqlDependency1.Repository
{
    public interface ILenhDatRepository
    {
        List<LenhDat> GetAllLenhDats();

        string AddLenhDat(LenhDat lenhDat);
    }
}