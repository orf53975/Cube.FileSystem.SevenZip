﻿/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cube.FileSystem.SevenZip;
using Cube.Log;

namespace Cube.FileSystem.App.Ice
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArchiveFacade
    ///
    /// <summary>
    /// 圧縮処理を実行するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ArchiveFacade : ProgressFacade
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveFacade
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="request">コマンドライン</param>
        ///
        /* ----------------------------------------------------------------- */
        public ArchiveFacade(Request request) : base(request) { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        /// 
        /// <summary>
        /// 圧縮フォーマットを取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public Format Format => Request.Format;

        /* ----------------------------------------------------------------- */
        ///
        /// Sources
        /// 
        /// <summary>
        /// 圧縮するファイルまたはフォルダの一覧を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public IList<string> Sources => Request.Sources.ToList();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// StartAsync
        /// 
        /// <summary>
        /// 圧縮を非同期で開始します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public async Task StartAsync()
        {
            using (var writer = new ArchiveWriter(Format))
            {
                try
                {
                    SetDestination();
                    foreach (var item in Sources) writer.Add(item);
                    ProgressStart();

                    var dummy = new CancellationTokenSource();
                    await writer.SaveAsync(Destination,
                        null,
                        new Progress<ArchiveReport>(x => ProgressReport = x),
                        dummy.Token
                    );

                    var name = System.IO.Path.GetFileName(Destination);
                    this.LogDebug($"{name}:{ProgressReport.DoneSize}/{ProgressReport.FileSize}");
                    ProgressReport.DoneSize = ProgressReport.FileSize; // hack

                    OnProgress(ValueEventArgs.Create(ProgressReport));
                }
                finally { ProgressStop(); }
            }
        }

        #endregion
    }
}