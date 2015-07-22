using PugTrace.Storage;
using System;
using System.Collections.Generic;

namespace PugTrace.Dashboard.Pages
{
    partial class HomePage
    {
        public int Page { get; private set; }
        public int PageCount { get; private set; }
        public int RowCount { get; private set; }
        public int RowsPerPage { get; private set; }
        public IEnumerable<TraceData> Rows { get; private set; }
        public Pager Pager { get; private set; }

        public void Initialize()
        {
            int page;
            int rowsPerPage;
            int.TryParse(Query("page"), out page);
            int.TryParse(Query("count"), out rowsPerPage);
            if (page == 0)
            {
                page = 1;
            }
            if (rowsPerPage == 0)
            {
                rowsPerPage = 10;
            }
            this.RowsPerPage = rowsPerPage;
            this.Page = page;
            using (var connection = this.Storage.GetConnection())
            {
                this.RowCount = connection.Count();
                int skip = (Page - 1) * RowsPerPage;
                int top = RowsPerPage;
                if (skip >= this.RowCount || Page < 1)
                {
                    throw new ArgumentOutOfRangeException("page");
                }
                this.Rows = connection.Get(skip, top);
            }
            this.Pager = new Pager(this.Page, this.RowsPerPage, this.RowCount);
        }
    }
}