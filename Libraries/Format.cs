﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.IO;

namespace Cube.FileSystem.SevenZip
{
    /* --------------------------------------------------------------------- */
    ///
    /// Format
    ///
    /// <summary>
    /// 対応している圧縮形式一覧を表す列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Format
    {
        /// <summary>不明</summary>
        Unknown = -1,
        /// <summary>7z with SFX module</summary>
        Sfx = -2,
        /// <summary>Open Zip archive format</summary>
        Zip = 0x01,
        /// <summary>Open BZip2 archive format</summary>
        BZip2 = 0x02,
        /// <summary>RarLab Rar archive format</summary>
        Rar = 0x03,
        /// <summary>Proprietary Arj archive format</summary>
        Arj = 0x04,
        /// <summary>Open LZW archive format (a.k.a "Z" archive format)</summary>
        Lzw = 0x05,
        /// <summary>Open Lzh archive format</summary>
        Lzh = 0x06,
        /// <summary>Open 7-zip archive format</summary>
        SevenZip = 0x07,
        /// <summary>Microsoft cabinet archive format</summary>
        Cab = 0x08,
        /// <summary>Nullsoft installation package format</summary>
        Nsis = 0x09,
        /// <summary>Open core 7-zip Lzma raw archive format</summary>
        Lzma = 0x0a,
        /// <summary>Open core 7-zip Lzma archive format</summary>
        Lzma86 = 0x0b,
        /// <summary>Open XZ archive format</summary>
        XZ = 0x0c,
        /// <summary>PPMD format</summary>
        Ppmd = 0x0d,
        /// <summary>Linux file system format</summary>
        Ext = 0xc7,
        /// <summary>VMware disk format</summary>
        Vmdk = 0xc8,
        /// <summary>VirtualBox disk format</summary>
        Vdi = 0xc9,
        /// <summary>QEMU file format</summary>
        Qcow = 0xca,
        /// <summary>Open GUID Partition Table</summary>
        Gpt = 0xcb,
        /// <summary>RarLab Rar5 archive format</summary>
        Rar5 = 0xcc,
        /// <summary>Intel HEX format</summary>
        IHex = 0xcd,
        /// <summary>Microsoft Help 2.0 file format</summary>
        Hxs = 0xce,
        /// <summary>TE format</summary>
        TE = 0xcf,
        /// <summary>UEFI based file format</summary>
        Uefic = 0xd0,
        /// <summary>UEFI based file format</summary>
        Uefis = 0xd1,
        /// <summary>Linux read-only filesystem format</summary>
        SquashFS = 0xd2,
        /// <summary>Linux read-only filesystem format</summary>
        CramFS = 0xd3,
        /// <summary>Adobe APM file format</summary>
        Apm = 0xd4,
        /// <summary>MSLZ archive format</summary>
        Mslz = 0xd5,
        /// <summary>Flash video format</summary>
        Flv = 0xd6,
        /// <summary>Shockwave Flash format</summary>
        Swf = 0xd7,
        /// <summary>Shockwave Flash format</summary>
        Swfc = 0xd8,
        /// <summary>Windows NT filesystem format</summary>
        Ntfs = 0xd9,
        /// <summary>Windows FAT filesystem format</summary>
        Fat = 0xda,
        /// <summary>Linux MBR filesystem format</summary>
        Mbr = 0xdb,
        /// <summary>Microsoft virtual hard disk file format</summary>
        Vhd = 0xdc,
        /// <summary>Windows PE executable format</summary>
        PE = 0xdd,
        /// <summary>Linux executable format</summary>
        Elf = 0xde,
        /// <summary>Apple Mac OS executable format</summary>
        MachO = 0xdf,
        /// <summary>Open Udf disk image format</summary>
        Udf = 0xe0,
        /// <summary>Xar open source archive format</summary>
        Xar = 0xe1,
        /// <summary>Mub format</summary>
        Mub = 0xe2,
        /// <summary>Macintosh Disk Image on CD</summary>
        Hfs = 0xe3,
        /// <summary>Apple Mac OS X Disk Copy Disk Image format</summary>
        Dmg = 0xe4,
        /// <summary>Microsoft Compound file format</summary>
        Compound = 0xe5,
        /// <summary>Microsoft Windows Imaging disk image format</summary>
        Wim = 0xe6,
        /// <summary>Open ISO disk image format</summary>
        Iso = 0xe7,
        /// <summary>Microsoft Compiled HTML Help file format</summary>
        Chm = 0xe9,
        /// <summary>Open split file format</summary>
        Split = 0xea,
        /// <summary>Open Rpm software package format</summary>
        Rpm = 0xeb,
        /// <summary>Open Debian software package format</summary>
        Deb = 0xec,
        /// <summary>Open Debian software package format</summary>
        Cpio = 0xed,
        /// <summary>Open Tar archive format.</summary>
        Tar = 0xee,
        /// <summary>Open GZip archive format.</summary>
        GZip = 0xef,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CompressionLevel
    ///
    /// <summary>
    /// 圧縮レベルを表す列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum CompressionLevel
    {
        /// <summary>None</summary>
        None = 0,
        /// <summary>Fast</summary>
        Fast = 1,
        /// <summary>Low</summary>
        Low = 3,
        /// <summary>Normal</summary>
        Normal = 5,
        /// <summary>High</summary>
        High = 7,
        /// <summary>Ultra</summary>
        Ultra = 9,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CompressionMethod
    ///
    /// <summary>
    /// 圧縮アルゴリズムを表す列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum CompressionMethod
    {
        /// <summary>コピー</summary>
        Copy,
        /// <summary>Deflate</summary>
        Deflate,
        /// <summary>Deflate (64bit)</summary>
        Deflate64,
        /// <summary>GZip</summary>
        GZip,
        /// <summary>BZip2</summary>
        BZip2,
        /// <summary>XZ</summary>
        XZ,
        /// <summary>LZMA</summary>
        Lzma,
        /// <summary>LZMA2</summary>
        Lzma2,
        /// <summary>PPMD</summary>
        Ppmd,
        /// <summary>初期設定</summary>
        Default,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// EncryptionMethod
    ///
    /// <summary>
    /// Format に対する拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum EncryptionMethod
    {
        /// <summary>AES 128bit</summary>
        Aes128,
        /// <summary>AES 192bit</summary>
        Aes192,
        /// <summary>AES 256bit</summary>
        Aes256,
        /// <summary>Zip crypto algorithm</summary>
        ZipCrypto,
        /// <summary>初期設定</summary>
        Default,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Formats
    ///
    /// <summary>
    /// Format に対する拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Formats
    {
        #region Methods

        #region ToXxx

        /* ----------------------------------------------------------------- */
        ///
        /// ToClassId
        ///
        /// <summary>
        /// Format に対応する Class ID を取得します。
        /// </summary>
        ///
        /// <param name="format">Format オブジェクト</param>
        ///
        /// <returns>Class ID</returns>
        ///
        /// <remarks>
        /// GUID は {23170F69-40C1-278A-1000-000110xx0000} となります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static Guid ToClassId(this Format format) =>
            format != Format.Unknown ?
            new Guid($"23170f69-40c1-278a-1000-000110{((int)format):x2}0000") :
            Guid.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// ToExtension
        ///
        /// <summary>
        /// Format に対応する拡張子を取得します。
        /// </summary>
        ///
        /// <param name="format">Format オブジェクト</param>
        ///
        /// <returns>拡張子</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToExtension(this Format format)
        {
            switch (format)
            {
                case Format.SevenZip: return ".7z";
                case Format.BZip2:    return ".bz2";
                case Format.GZip:     return ".gz";
                case Format.Lzw:      return ".z";
                case Format.Sfx:      return ".exe";
                case Format.Unknown:  return string.Empty;
                default: return $".{format.ToString().ToLower()}";
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToExtension
        ///
        /// <summary>
        /// CompressionMethod に対応する拡張子を取得します。
        /// </summary>
        ///
        /// <param name="method">CompressionMethod オブジェクト</param>
        ///
        /// <returns>拡張子</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string ToExtension(this CompressionMethod method)
        {
            switch (method)
            {
                case CompressionMethod.BZip2: return ".bz2";
                case CompressionMethod.GZip:  return ".gz";
                case CompressionMethod.XZ:    return ".xz";
                default: return string.Empty;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToMethod
        ///
        /// <summary>
        /// Format に対応する CompressionMethod を取得します。
        /// </summary>
        ///
        /// <param name="format">Format オブジェクト</param>
        ///
        /// <returns>CompressionMethod オブジェクト</returns>
        ///
        /// <remarks>
        /// 一部の Format は CompressionMethod に指定可能なため、
        /// その対応関係をこのメソッドに記述しています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static CompressionMethod ToMethod(this Format format)
        {
            switch (format)
            {
                case Format.BZip2: return CompressionMethod.BZip2;
                case Format.GZip:  return CompressionMethod.GZip;
                case Format.XZ:    return CompressionMethod.XZ;
                default: return CompressionMethod.Default;
            }
        }

        #endregion

        #region FromXxx

        /* ----------------------------------------------------------------- */
        ///
        /// FromMethod
        ///
        /// <summary>
        /// CompressionMethod に対応する Format を取得します。
        /// </summary>
        ///
        /// <param name="method">CompressionMethod</param>
        ///
        /// <returns>Format オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Format FromMethod(CompressionMethod method)
        {
            switch (method)
            {
                case CompressionMethod.GZip:  return Format.GZip;
                case CompressionMethod.BZip2: return Format.BZip2;
                case CompressionMethod.XZ:    return Format.XZ;
                default: return Format.Unknown;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FromString
        ///
        /// <summary>
        /// 文字列に対応する Format を取得します。
        /// </summary>
        ///
        /// <param name="format">Format を表す文字列</param>
        ///
        /// <returns>Format オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Format FromString(string format)
        {
            var cvt = format.ToLower();
            if (cvt == "7z")  return Format.SevenZip;
            if (cvt == "exe") return Format.Sfx;
            foreach (Format item in Enum.GetValues(typeof(Format)))
            {
                if (item.ToString().ToLower() == cvt) return item;
            }
            return Format.Unknown;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FromExtension
        ///
        /// <summary>
        /// 拡張子に対応する Format を取得します。
        /// </summary>
        ///
        /// <param name="ext">拡張子</param>
        ///
        /// <returns>Format オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Format FromExtension(string ext)
        {
            if (_ext == null) _ext = CreateExtensionMap();
            var cvt = ext.ToLower();
            return _ext.ContainsKey(cvt) ? _ext[cvt] : Format.Unknown;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FromStream
        ///
        /// <summary>
        /// ストリームの内容に対応する Format を取得します。
        /// </summary>
        ///
        /// <param name="stream">ストリーム</param>
        ///
        /// <returns>Format オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Format FromStream(Stream stream)
        {
            if (_sig == null) _sig = CreateSignatureMap();

            var preserve = stream.Position;
            try
            {
                var bytes = new byte[16];
                var count = stream.Read(bytes, 0, 16);
                if (count <= 0) return Format.Unknown;

                var src = BitConverter.ToString(bytes, 0, count);
                foreach (var cmp in _sig)
                {
                    if (src.StartsWith(cmp.Key, StringComparison.OrdinalIgnoreCase)) return cmp.Value;
                }

                // for special signature
                if (Match(stream, 0x101, 5, "75-73-74-61-72")) return Format.Tar;
                if (Match(stream, 0x002, 3, "2D-6C-68"))       return Format.Lzh;

                return Format.Unknown;
            }
            finally { stream.Seek(preserve, SeekOrigin.Begin); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FromFile
        ///
        /// <summary>
        /// ファイルに対応する Format を取得します。
        /// </summary>
        ///
        /// <param name="path">ファイル名</param>
        ///
        /// <returns>Format オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Format FromFile(string path) => FromFile(path, new IO());

        /* ----------------------------------------------------------------- */
        ///
        /// FromFile
        ///
        /// <summary>
        /// ファイルに対応する Format を取得します。
        /// </summary>
        ///
        /// <param name="path">ファイル名</param>
        /// <param name="io">ファイル操作用オブジェクト</param>
        ///
        /// <returns>Format オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Format FromFile(string path, IO io)
        {
            var info = io.Get(path);

            if (info.Exists)
            {
                using (var stream = io.OpenRead(path))
                {
                    var dest = FromStream(stream);
                    if (dest != Format.Unknown) return dest;
                }
            }
            return FromExtension(info.Extension);
        }

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Match
        ///
        /// <summary>
        /// ストリームの特定の内容が一致するかどうか判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static bool Match(Stream stream, int offset, int count, string compared)
        {
            var bytes = new byte[count];
            stream.Seek(offset, SeekOrigin.Begin);
            if (stream.Read(bytes, 0, count) < count) return false;
            return BitConverter.ToString(bytes).StartsWith(compared, StringComparison.OrdinalIgnoreCase);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateExtensionMap
        ///
        /// <summary>
        /// Format と拡張子の対応関係を示すオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IDictionary<string, Format> CreateExtensionMap()
        {
            var dest = new Dictionary<string, Format>
            {
                { ".7z",  Format.SevenZip },
                { ".bz2", Format.BZip2    },
                { ".tbz", Format.BZip2    },
                { ".gz",  Format.GZip     },
                { ".tgz", Format.GZip     },
                { ".xz",  Format.XZ       },
                { ".txz", Format.XZ       },
                { ".z",   Format.Lzw      },
            };

            foreach (Format item in Enum.GetValues(typeof(Format)))
            {
                var ext = $".{item.ToString().ToLower()}";
                if (!dest.ContainsKey(ext)) dest.Add(ext, item);
            }

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateSignatureMap
        ///
        /// <summary>
        /// Format と Signature の対応関係を示すオブジェクトを取得します。
        /// </summary>
        ///
        /// <remarks>
        /// このマップでは、ファイルの先頭に Signature が記載されている
        /// もののみを対象としています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private static IDictionary<string, Format> CreateSignatureMap() =>
            new Dictionary<string, Format>
            {
                { "50-4B-03-04",                Format.Zip      },
                { "42-5A-68",                   Format.BZip2    },
                { "52-61-72-21-1A-07-00",       Format.Rar      },
                { "60-EA",                      Format.Arj      },
                { "1F-9D-90",                   Format.Lzw      },
                { "37-7A-BC-AF-27-1C",          Format.SevenZip },
                { "4D-53-43-46",                Format.Cab      },
                { "5D-00-00-40-00",             Format.Lzma     },
                { "FD-37-7A-58-5A",             Format.XZ       },
                { "52-61-72-21-1A-07-01-00",    Format.Rar5     },
                { "46-4C-56",                   Format.Flv      },
                { "46-57-53",                   Format.Swf      },
                { "63-6F-6E-65-63-74-69-78",    Format.Vhd      },
                { "4D-5A",                      Format.PE       },
                { "7F-45-4C-46",                Format.Elf      },
                { "78-61-72-21",                Format.Xar      },
                { "78",                         Format.Dmg      },
                { "4D-53-57-49-4D-00-00-00",    Format.Wim      },
                { "43-44-30-30-31",             Format.Iso      },
                { "49-54-53-46",                Format.Chm      },
                { "ED-AB-EE-DB",                Format.Rpm      },
                { "1F-8B-08",                   Format.GZip     },
            };

        #endregion

        #region Fields
        private static IDictionary<string, Format> _ext;
        private static IDictionary<string, Format> _sig;
        #endregion
    }
}
