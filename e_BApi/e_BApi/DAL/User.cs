using e_BApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace e_BApi.DAL
{
    public class User : IUser
    {
        IzvozB_sEntities db = new IzvozB_sEntities();
        public mExp GetExp(int expid)
        {
            
            mExp mxp = new mExp();

            IEnumerable<mExp> getmxp = from ex in db.Exporters
                                       where ex.ExpID == expid
                                       select new mExp()
                                       {
                                           ExpID = ex.ExpID,
                                           Name = ex.Name,
                                           Surname = ex.Surname,
                                           AuthorizationID = ex.AuthorizationID
                                       };
            mxp = getmxp.FirstOrDefault();        


            return mxp;
        }

        public mExpEP GetFNPDC(int expid, int dateid)
        {

            IEnumerable<mDate> date = from d in db.Dates
                                      where d.DateID == dateid
                                      select new mDate()
                                      {
                                          DateID = d.DateID,
                                          StartDate = d.StartDate,
                                          EndDate = d.EndDate
                                      };

            mDate md = (mDate)date.FirstOrDefault();

            var checkDe = db.DailyEntries.Where(cde => cde.Date >= md.StartDate
                                                 && cde.Date <= md.EndDate
                                                 && cde.ExpID == expid).Count();

            int allDe;

            if (checkDe > 0)
            {
                allDe = db.DailyEntries.Where(de => de.Date >= md.StartDate
                                                 && de.Date <= md.EndDate
                                                 && de.ExpID == expid).Sum(de => de.Quantity);
            }
            else
                allDe = 0;

            string name = db.Exporters.Where(ex => ex.ExpID == expid).Select(ex => ex.Name).FirstOrDefault();
            string surname = db.Exporters.Where(ex => ex.ExpID == expid).Select(ex => ex.Surname).FirstOrDefault();
            int prediction = db.Predictions.Where(p => p.ExpID == expid).Select(p => p.Quantity).FirstOrDefault();




            string fullname = $"{name} {surname}";

            mExpEP eep = new mExpEP();
            eep.FullName = fullname;
            eep.Prediction = prediction;
            eep.DE = allDe;
            eep.BrUpisa = checkDe;



            return eep;
        }

        public List<mDE_list> GetListDe(int id, int dateid)
        {
            List<mDE_list> lde = new List<mDE_list>();
            

            IEnumerable<mDate> date = from d in db.Dates
                                      where d.DateID == dateid
                                      select new mDate()
                                      {
                                          DateID = d.DateID,
                                          StartDate = d.StartDate,
                                          EndDate = d.EndDate
                                      };
            mDate md = (mDate)date.FirstOrDefault();

            string name = db.Exporters.Where(ex => ex.ExpID == id).Select(ex => ex.Name).FirstOrDefault();
            string surname = db.Exporters.Where(ex => ex.ExpID == id).Select(ex => ex.Surname).FirstOrDefault();

            string fullname = $"{name} {surname}";

            IQueryable<mDE_list> iqList = db.DailyEntries.Where(de => de.Date >= md.StartDate
                                                 && de.Date <= md.EndDate
                                                 && de.ExpID == id).Select(de => new mDE_list {Quantity = de.Quantity, Date = de.Date });
            foreach(var iteam in iqList)
            {  
                mDE_list dailyE = new mDE_list();
                dailyE.Name = fullname;
                dailyE.Quantity = iteam.Quantity;
                dailyE.Date = iteam.Date;

                lde.Add(dailyE);
            }


            return lde;
        }

        public List<mExp> GetListExp()
        {
            List<mExp> exp = new List<mExp>();

            IQueryable<int> qw = db.Exporters.Select(de => de.ExpID);
           

            foreach (var q in qw)
            {
                exp.Add(GetExp(q));    
            }
            return exp;
        }

        public List<mExpEP> GetListFNPDC(int dateid)
        {
            List<mExpEP> list = new List<mExpEP>();

            IEnumerable<mDate> date = from d in db.Dates
                                      where d.DateID == dateid
                                      select new mDate()
                                      {
                                          DateID = d.DateID,
                                          StartDate = d.StartDate,
                                          EndDate = d.EndDate
                                      };

            mDate md = (mDate)date.FirstOrDefault();

            IQueryable<int> checkDe = db.DailyEntries.Where(cde => cde.Date >= md.StartDate
                                                 && cde.Date <= md.EndDate).Select(cde => cde.ExpID);

            foreach(int i in checkDe.Distinct())
            {
                list.Add(GetFNPDC(i, dateid));
            }


            return list;
        }

        public mPredEntr GetPE(int dateid)
        {
            IEnumerable<mDate> date = from d in db.Dates
                                      where d.DateID == dateid
                                      select new mDate()
                                      {
                                          DateID = d.DateID,
                                          StartDate = d.StartDate,
                                          EndDate = d.EndDate
                                      };
            mDate md = (mDate)date.FirstOrDefault();

            var P = db.Predictions.Where(p => p.DateID == dateid).Sum(p => p.Quantity);

            var E = db.DailyEntries.Where(de => de.Date >= md.StartDate
                                                && de.Date <= md.EndDate
                                                ).Sum(de => de.Quantity);
            mPredEntr mpe = new mPredEntr();
            mpe.P = P;
            mpe.E = E;
            mpe.dateid = dateid;

            return mpe;
        }

        public bool PostDate(Date d)
        {
            bool isOk = true;
            try
            {
                db.Dates.Add(d);
                db.SaveChanges();
            }
            catch
            {
                isOk = false;
            }
            return isOk;
        }

        public bool PostDE(DailyEntry pr)
        {
            bool isOk = true;
            try
            {
                db.DailyEntries.Add(pr);
                db.SaveChanges();
            }
            catch
            {
                isOk = false;
            }
            return isOk;
        }

        public bool PostPrediction(Prediction pr)
        {
            bool isOk = true;
            try
            {
                db.Predictions.Add(pr);
                db.SaveChanges();
            }
            catch
            {
                isOk = false;
            }
            return isOk;
        }
    }
}