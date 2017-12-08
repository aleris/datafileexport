// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
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