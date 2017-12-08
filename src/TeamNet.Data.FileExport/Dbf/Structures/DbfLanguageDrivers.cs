// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
namespace TeamNet.Data.FileExport.Dbf.Structures
{
    class DbfLanguageDrivers
    {
        public static readonly byte DosUsa = 0x01;
        public static readonly byte DosMultilingual = 0x02;
        public static readonly byte WindowsAnsi = 0x03;
        public static readonly byte StandardMacintosh = 0x04;
        public static readonly byte EeMsDos = 0x64;
        public static readonly byte NordicMsDos = 0x65;
        public static readonly byte RussianMsDos = 0x66;
        public static readonly byte IcelandicMsDos = 0x67;
        public static readonly byte KamenickyCzechMsDos = 0x68;
        public static readonly byte MazoviaPolishMsDos = 0x69;
        public static readonly byte GreekMsDos437G = 0x6A;
        public static readonly byte TurkishMsDos = 0x6B;
        public static readonly byte RussianMacintosh = 0x96;
        public static readonly byte EasternEuropeanMacintosh = 0x97;
        public static readonly byte GreekMacintosh = 0x98;
        public static readonly byte WindowsEe = 0xC8;
        public static readonly byte RussianWindows = 0xC9;
        public static readonly byte TurkishWindows = 0xCA;
        public static readonly byte GreekWindows = 0xCB;
    }
}