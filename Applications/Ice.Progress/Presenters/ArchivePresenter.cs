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
using Cube.FileSystem.SevenZip;
using Cube.FileSystem.Ice;

namespace Cube.FileSystem.App.Ice
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArchivePresenter
    ///
    /// <summary>
    /// 圧縮用の Presenter クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ArchivePresenter : ProgressPresenter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ArchivePresenter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="view">View オブジェクト</param>
        /// <param name="args">コマンドライン</param>
        /// <param name="settings">ユーザ設定</param>
        /// <param name="events">イベント集約用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public ArchivePresenter(IProgressView view, Request args,
            SettingsFolder settings, IEventAggregator events)
            : base(view, new ArchiveFacade(args, settings), settings, events)
        {
            // View
            View.Logo   = Properties.Resources.HeaderArchive;
            View.Status = Properties.Resources.MessagePreArchive;

            // Model
            var model = Model as ArchiveFacade;
            model.DestinationRequired += WhenDestinationRequired;
            model.PasswordRequired    += WhenPasswordRequired;
            model.Progress            += WhenProgress;
            model.SettingsRequired    += WhenSettingsRequired;
        }

        #endregion

        #region Handlers

        /* ----------------------------------------------------------------- */
        ///
        /// WhenSettingsRequired
        /// 
        /// <summary>
        /// 詳細設定要求時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenSettingsRequired(object sender, QueryEventArgs<string, ArchiveDetails> e)
            => ShowDialog(() => Views.ShowDetailsView(e));

        /* ----------------------------------------------------------------- */
        ///
        /// WhenDestinationRequired
        /// 
        /// <summary>
        /// 保存パス要求時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenDestinationRequired(object sender, QueryEventArgs<string, string> e)
            => ShowDialog(() => Views.ShowSaveView(e, false));

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPasswordRequired
        /// 
        /// <summary>
        /// パスワード要求時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenPasswordRequired(object sender, QueryEventArgs<string, string> e)
            => ShowDialog(() => Views.ShowPasswordView(e, true));

        /* ----------------------------------------------------------------- */
        ///
        /// WhenProgress
        /// 
        /// <summary>
        /// 進捗状況の更新時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenProgress(object sender, ValueEventArgs<ArchiveReport> e)
            => Sync(() =>
        {
            View.FileName   = Model.IO.Get(Model.Destination).Name;
            View.TotalCount = e.Value.TotalCount;
            View.Count      = e.Value.Count;
            View.Status     = string.Format(Properties.Resources.MessageArchive, Model.Destination);
            View.Value      = Math.Max(Math.Max((int)(e.Value.Ratio * View.Unit), 1), View.Value);
        });

        #endregion
    }
}