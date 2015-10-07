﻿using System;
using System.Collections.Generic;

namespace PugTrace.Dashboard
{
    public class Pager
    {
        private const int PageItemsCount = 7;
        private const int DefaultRecordsPerPage = 10;

        private int _startPageIndex = 1;
        private int _endPageIndex = 1;

        public Pager(int page, int perPage, long total, string typeFilter, string fromTime, string toTime, string value)
        {
            FromRecord = (page - 1) * perPage;
            RecordsPerPage = perPage > 0 ? perPage : DefaultRecordsPerPage;
            TotalRecordCount = total;
            CurrentPage = FromRecord / RecordsPerPage + 1;
            TotalPageCount = (int)Math.Ceiling((double)TotalRecordCount / RecordsPerPage);
            TypeFilter = typeFilter;
            MsgFrom = fromTime;
            MsgTo = toTime;
            SearchValue = value;

            PagerItems = GenerateItems();
        }

        public string BasePageUrl { get; set; }

        public int FromRecord { get; private set; }
        public int RecordsPerPage { get; private set; }
        public int CurrentPage { get; private set; }
        public string TypeFilter { get; private set; }
        public string MsgFrom { get; set; }
        public string MsgTo { get; set; }
        public string SearchValue { get; set; }

        public int TotalPageCount { get; private set; }
        public long TotalRecordCount { get; private set; }

        public ICollection<Item> PagerItems { get; private set; }

        public string PageUrl(int page)
        {
            if (page < 1 || page > TotalPageCount) return "#";

            return BasePageUrl + "?page=" + page + "&count=" + RecordsPerPage + (TypeFilter == null ? "" : "&type=" + TypeFilter);
        }

        public string RecordsPerPageUrl(int perPage)
        {
            if (perPage <= 0) return "#";
            return BasePageUrl + "?page=1&count=" + perPage + (TypeFilter == null ? "" : "&type=" + TypeFilter);
        }

        public string TypeFilterUrl(string typeFilter)
        {
            return BasePageUrl + "?page=1&count=" + RecordsPerPage + (typeFilter == null ? "" : "&type=" + typeFilter);
        }

        public string GetFromTime() {
            if (MsgFrom != null)
            {
                return MsgFrom;
            }
            else {
                return "";
            }
        }

        public string GetToTime()
        {
            if (MsgTo != null)
            {
                return MsgTo;
            }
            else
            {
                return "";
            }
        }

        public string GetSearchValue()
        {
            if (SearchValue != null) {
                return SearchValue;
            } else {
                return "";
            }
        }

        public string GetFilterType()
        {
            if (TypeFilter != null)
            {
                return TypeFilter;
            }
            else
            {
                return "";
            }
        }

        private ICollection<Item> GenerateItems()
        {
            // start page index
            _startPageIndex = CurrentPage - (PageItemsCount / 2);
            if (_startPageIndex + PageItemsCount > TotalPageCount)
                _startPageIndex = TotalPageCount + 1 - PageItemsCount;
            if (_startPageIndex < 1)
                _startPageIndex = 1;

            // end page index
            _endPageIndex = _startPageIndex + PageItemsCount - 1;
            if (_endPageIndex > TotalPageCount)
                _endPageIndex = TotalPageCount;

            var pagerItems = new List<Item>();
            if (TotalPageCount == 0) return pagerItems;

            AddPrevious(pagerItems);

            // first page
            if (_startPageIndex > 1)
                pagerItems.Add(new Item(1, false, ItemType.Page));

            // more page before numeric page buttons
            AddMoreBefore(pagerItems);

            // numeric page
            AddPageNumbers(pagerItems);

            // more page after numeric page buttons
            AddMoreAfter(pagerItems);

            // last page
            if (_endPageIndex < TotalPageCount)
                pagerItems.Add(new Item(TotalPageCount, false, ItemType.Page));

            // Next page
            AddNext(pagerItems);

            return pagerItems;
        }

        private void AddPrevious(ICollection<Item> results)
        {
            var item = new Item(CurrentPage - 1, CurrentPage == 1, ItemType.PrevPage);
            results.Add(item);
        }

        private void AddMoreBefore(ICollection<Item> results)
        {
            if (_startPageIndex > 2)
            {
                var index = _startPageIndex - 1;
                if (index < 1) index = 1;
                var item = new Item(index, false, ItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<Item> results)
        {
            if (_endPageIndex < TotalPageCount - 1)
            {
                var index = _startPageIndex + PageItemsCount;
                if (index > TotalPageCount) { index = TotalPageCount; }
                var item = new Item(index, false, ItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<Item> results)
        {
            for (var pageIndex = _startPageIndex; pageIndex <= _endPageIndex; pageIndex++)
            {
                var item = new Item(pageIndex, false, ItemType.Page);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<Item> results)
        {
            var item = new Item(CurrentPage + 1, CurrentPage >= TotalPageCount, ItemType.NextPage);
            results.Add(item);
        }

        public class Item
        {
            public Item(int pageIndex, bool disabled, ItemType type)
            {
                PageIndex = pageIndex;
                Disabled = disabled;
                Type = type;
            }

            public int PageIndex { get; private set; }
            public bool Disabled { get; private set; }
            public ItemType Type { get; private set; }
        }

        public enum ItemType
        {
            Page,
            PrevPage,
            NextPage,
            MorePage
        }
    }
}