using System;
using System.Collections.Generic;
using System.Text;

namespace Operationen
{
    public class ExportOperationenDataItem : System.IComparable<ExportOperationenDataItem>
    {
        public string opsCode;
        public string opsText;

        internal ExportOperationenDataItem(string code, string text)
        {
            opsCode = code;
            opsText = text;
        }

        public int CompareTo(ExportOperationenDataItem other)
        {
            return opsCode.CompareTo(other.opsCode);
        }
    };
}
