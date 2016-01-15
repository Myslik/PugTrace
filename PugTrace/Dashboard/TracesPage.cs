using PugTrace.Storage;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PugTrace.Dashboard
{
    public abstract class TracesPage : RazorPage<IEnumerable<Trace>>
    {
        public TracesPage ()
        {
            Model = Enumerable.Empty<Trace>();
        }

        public Pager Pager { get; set; }

        public long Count { get { return Model.Count(); } }

        public override void OnAssigned()
        {
            int page;
            int rowsPerPage;
            int rowCount;
            string typeFilter = null;
            string from = null;
            string to = null;
            string value = null;
            if (Query("from") != "") { from = Query("from"); }
            if (Query("to") != "") { to = Query("to"); }
            if (Query("val") != "") { value = Query("val"); }
            int.TryParse(Query("page"), out page);
            int.TryParse(Query("count"), out rowsPerPage);
            if (Query("type") != "") { typeFilter = Query("type"); }
            if (page == 0)
            {
                page = 1;
            }
            if (rowsPerPage == 0)
            {
                rowsPerPage = 100;
            }
            using (var connection = this.Storage.GetConnection())
            {
                rowCount = connection.Count(typeFilter);
                int skip = (page - 1) * rowsPerPage;
                int top = rowsPerPage;
                if (skip >= Math.Max(1, rowCount) || page < 1)
                {
                    throw new ArgumentOutOfRangeException("page");
                }
                if ((from != null) && (to != null) && (value != null)) {
                    DateTime fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime toDate = DateTime.ParseExact(to, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    Model = connection.Search(fromDate, toDate, value, typeFilter);
                    rowCount = Model.Count();
                } else {
                    Model = connection.Get(skip, top, typeFilter);
                }
            }
            Pager = new Pager(page, rowsPerPage, rowCount, typeFilter, from, to, value);
        }
    }
}