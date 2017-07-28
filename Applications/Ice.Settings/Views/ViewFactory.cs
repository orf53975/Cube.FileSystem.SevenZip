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
namespace Cube.FileSystem.App.Ice.Settings
{
    /* --------------------------------------------------------------------- */
    ///
    /// ViewFactory
    ///
    /// <summary>
    /// 各種 View の生成用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ViewFactory
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CreateSettingsView
        /// 
        /// <summary>
        /// 設定画面を生成します。
        /// </summary>
        /// 
        /// <returns>View オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public virtual ISettingsView CreateSettingsView() => new SettingsForm();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Views
    ///
    /// <summary>
    /// 各種 View の生成用クラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// Views は ViewFactory のプロキシとして実装されています。
    /// 実際の View 生成コードは ViewFactory および継承クラスで実装して
    /// 下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static class Views
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        /// 
        /// <summary>
        /// Facotry オブジェクトを設定します。
        /// </summary>
        /// 
        /// <param name="factory">Factory オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure(ViewFactory factory)
            => _factory = factory;

        #region Factory methods

        public static ISettingsView CreateSettingsView()
            => _factory?.CreateSettingsView();

        #endregion

        #region Fields
        private static ViewFactory _factory = new ViewFactory();
        #endregion
    }
}