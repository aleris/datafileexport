// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System.Text;

namespace TeamNet.Data.FileExport.Dbf.Structures
{
    class DbfDeletedFlags
    {
        public static readonly byte Deleted = Encoding.ASCII.GetBytes("*")[0];
        public static readonly byte Valid = Encoding.ASCII.GetBytes(" ")[0];

        public static readonly int StructureSize = sizeof(byte);
    }
}