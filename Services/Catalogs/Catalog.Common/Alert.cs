using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Common
{
    public static class Alert
    {
        public enum Public
        {
            [Description("خطایی سمت سرور رخ داده است")]
            ServerException,
            [Description("موفق")]
            Success,
            [Description("ناموفق")]
            UnSuccess,
            [Description("موردی یافت نشد")]
            NotFound,
            [Description("خطای احراز هویت")]
            AuthError,
            [Description("بازیابی انجام شد")]
            Recovery,
        }
    }
}
