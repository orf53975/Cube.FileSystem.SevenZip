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

namespace Cube.FileSystem.SevenZip.Archives
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArchiveItemExtension
    ///
    /// <summary>
    /// Provides extended methods of the ArchiveItem class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ArchiveItemExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectory
        ///
        /// <summary>
        /// Creates the directory.
        /// </summary>
        ///
        /// <param name="item">Information of an archived item.</param>
        /// <param name="root">Path of the root directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void CreateDirectory(this ArchiveItem item, string root) =>
            CreateDirectory(item, root, new IO());

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectory
        ///
        /// <summary>
        /// Creates the directory.
        /// </summary>
        ///
        /// <param name="item">Information of the archived item.</param>
        /// <param name="root">Path of the root directory.</param>
        /// <param name="io">I/O handler.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void CreateDirectory(this ArchiveItem item, string root, IO io)
        {
            if (!item.IsDirectory) return;
            var path = io.Combine(root, item.FullName);
            if (!io.Exists(path)) io.CreateDirectory(path);
            SetAttributes(item, root, io);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributes
        ///
        /// <summary>
        /// Sets attributes, creation time, last written time, and last
        /// accessed time to the extracted file or directory.
        /// </summary>
        ///
        /// <param name="item">Information of the archived item.</param>
        /// <param name="root">Path of the root directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void SetAttributes(this ArchiveItem item, string root) =>
            SetAttributes(item, root, new IO());

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributes
        ///
        /// <summary>
        /// Sets attributes, creation time, last written time, and last
        /// accessed time to the extracted file or directory.
        /// </summary>
        ///
        /// <param name="item">Information of the archived item.</param>
        /// <param name="root">Path of the root directory.</param>
        /// <param name="io">I/O handler.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void SetAttributes(this ArchiveItem item, string root, IO io)
        {
            var path = io.Combine(root, item.FullName);
            if (!io.Exists(path)) return;

            io.SetAttributes(path, item.Attributes);
            SetCreationTime(item, path, io);
            SetLastWriteTime(item, path, io);
            SetLastAccessTime(item, path, io);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// SetCreationTime
        ///
        /// <summary>
        /// Sets the creation time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetCreationTime(this ArchiveItem item, string path, IO io)
        {
            var time = item.CreationTime  != DateTime.MinValue ? item.CreationTime :
                       item.LastWriteTime != DateTime.MinValue ? item.LastWriteTime :
                       item.LastAccessTime;
            if (time != DateTime.MinValue) io.SetCreationTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastWriteTime
        ///
        /// <summary>
        /// Sets the last written time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetLastWriteTime(this ArchiveItem item, string path, IO io)
        {
            var time = item.LastWriteTime  != DateTime.MinValue ? item.LastWriteTime :
                       item.LastAccessTime != DateTime.MinValue ? item.LastAccessTime :
                       item.CreationTime;
            if (time != DateTime.MinValue) io.SetLastWriteTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastAccessTime
        ///
        /// <summary>
        /// Sets the last accessed time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SetLastAccessTime(this ArchiveItem item, string path, IO io)
        {
            var time = item.LastAccessTime != DateTime.MinValue ? item.LastAccessTime :
                       item.LastWriteTime  != DateTime.MinValue ? item.LastWriteTime :
                       item.CreationTime;
            if (time != DateTime.MinValue) io.SetLastAccessTime(path, time);
        }

        #endregion
    }
}
