using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Util
{
    internal static class vars
    {
        static vars()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        internal static readonly int defaultCodePage = CultureInfo.CurrentCulture.TextInfo.OEMCodePage;
    }
}
