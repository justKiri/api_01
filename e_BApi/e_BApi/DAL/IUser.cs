using e_BApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace e_BApi.DAL
{
    interface IUser
    {
        mExp GetExp(int expid);
        mExpEP GetFNPDC(int expid, int dateid);
        bool PostPrediction(Prediction pr);
        bool PostDE(DailyEntry pr);

        List<mDE_list> GetListDe(int id, int dateid);

        List<mExpEP> GetListFNPDC(int dateid);

        bool PostDate(Date d);

        mPredEntr GetPE(int dateid);

        List<mExp> GetListExp();
    }
}
