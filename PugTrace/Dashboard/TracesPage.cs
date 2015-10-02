using PugTrace.Storage;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PugTrace.Dashboard
{
    public abstract class TracesPage : RazorPage<IEnumerable<TraceData>>
    {
        public TracesPage ()
        {
            Model = Enumerable.Empty<TraceData>();
        }

        public Pager Pager { get; set; }

        public long Count { get { return Model.Count(); } }

        public override void OnAssigned()
        {
            int page;
            int rowsPerPage;
            int rowCount;
            string typeFilter;
            int.TryParse(Query("page"), out page);
            int.TryParse(Query("count"), out rowsPerPage);
            typeFilter = Query("type");
            if (page == 0)
            {
                page = 1;
            }
            if (rowsPerPage == 0)
            {
                rowsPerPage = 10;
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
                Model = connection.Get(skip, top, typeFilter);
            }
            Pager = new Pager(page, rowsPerPage, rowCount, typeFilter);
        }
    }
}