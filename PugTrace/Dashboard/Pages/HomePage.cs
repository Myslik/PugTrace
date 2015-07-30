using PugTrace.Storage;
using System;
using System.Collections.Generic;

namespace PugTrace.Dashboard.Pages
{
    partial class HomePage
    {
        public IEnumerable<TraceData> Rows { get; private set; }
        public Pager Pager { get; private set; }

        public void Initialize()
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
                this.Rows = connection.Get(skip, top, typeFilter);
            }
            this.Pager = new Pager(page, rowsPerPage, rowCount, typeFilter);
        }
    }
}