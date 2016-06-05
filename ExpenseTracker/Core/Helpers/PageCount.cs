using System;

namespace ExpenseTracker.Core.Helpers
{
    public static class PageCount
    {
        public static int Count(int totalRecord, int pageSize)
        {
            return Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(totalRecord) / pageSize));
        }
    }
}