﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using Cube.Conversions;
using Cube.Forms;
using Cube.Images.Icons;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Cube.FileSystem.SevenZip.Ice.App
{
    /* --------------------------------------------------------------------- */
    ///
    /// OverwriteForm
    ///
    /// <summary>
    /// 上書き確認ダイアログを表示するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class OverwriteForm : Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OverwriteForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OverwriteForm()
        {
            InitializeComponent();

            IconPictureBox.Image = StockIcons.Warning.GetIcon(IconSize.Large).ToBitmap();

            YesButton.Click          += (s, e) => Execute(OverwriteMode.Yes);
            NoButton.Click           += (s, e) => Execute(OverwriteMode.No);
            ExitButton.Click         += (s, e) => Execute(OverwriteMode.Cancel);
            AlwaysYesButton.Click    += (s, e) => Execute(OverwriteMode.AlwaysYes);
            AlwaysNoButton.Click     += (s, e) => Execute(OverwriteMode.AlwaysNo);
            AlwaysRenameButton.Click += (s, e) => Execute(OverwriteMode.AlwaysRename);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// 上書き元の情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Information Source { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Destination
        ///
        /// <summary>
        /// 上書き先の情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Information Destination { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// OverwriteMode
        ///
        /// <summary>
        /// 上書き方法を示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OverwriteMode OverwriteMode { get; private set; } = OverwriteMode.Cancel;

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnShown
        ///
        /// <summary>
        /// フォーム表示時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnShown(EventArgs e)
        {
            UpdateDescription();
            base.OnShown(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateDescription
        ///
        /// <summary>
        /// ラベルの内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateDescription()
        {
            var ss = new StringBuilder();
            ss.AppendLine(Properties.Resources.MessageOverwrite);
            ss.AppendLine();

            ss.AppendLine(Properties.Resources.MessageCurrent);
            if (Destination != null)
            {
                ss.AppendLine(Destination.FullName);
                ss.AppendLine(string.Format("{0} : {1}",
                    Properties.Resources.MessageBytes,
                    Destination.Length.ToPrettyBytes()
                ));
                ss.AppendLine(string.Format("{0} : {1}",
                    Properties.Resources.MessageLastWriteTime,
                    Destination.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss")
                ));
            }
            else ss.AppendLine(Properties.Resources.MessageUnknownFile);
            ss.AppendLine();

            ss.AppendLine(Properties.Resources.MessageNewFile);
            if (Source != null)
            {
                ss.AppendLine(Source.FullName);
                ss.AppendLine(string.Format("{0} : {1}",
                    Properties.Resources.MessageBytes,
                    Source.Length.ToPrettyBytes()
                ));
                ss.AppendLine(string.Format("{0} : {1}",
                    Properties.Resources.MessageLastWriteTime,
                    Source.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss")
                ));
            }
            else ss.AppendLine(Properties.Resources.MessageUnknownFile);

            DescriptionLabel.Text = ss.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Execute
        ///
        /// <summary>
        /// 上書き内容を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Execute(OverwriteMode mode)
        {
            OverwriteMode = mode;
            Close();
        }

        #endregion
    }
}
