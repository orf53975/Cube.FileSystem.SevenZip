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
#include "QueryInfoSettings.h"
#include "Log.h"

namespace Cube {
namespace FileSystem {
namespace Ice {

/* ------------------------------------------------------------------------- */
///
/// QueryInfoSettings
///
/// <summary>
/// オブジェクトを初期化します。
/// </summary>
///
/* ------------------------------------------------------------------------- */
QueryInfoSettings::QueryInfoSettings() :
    enabled_(false), count_(0) {}

/* ------------------------------------------------------------------------- */
///
/// Load
///
/// <summary>
/// ユーザ設定を読み込みます。
/// </summary>
///
/* ------------------------------------------------------------------------- */
void QueryInfoSettings::Load() {
    try {
        auto hkey = Open(_T("Software\\CubeSoft\\CubeICE\\v3"));
        if (hkey == nullptr) return;

        auto v0  = GetDword(hkey, _T("ToolTip"), static_cast<DWORD>(0));
        enabled_ = (v0 != 0);
        auto v1  = GetDword(hkey, _T("ToolTipCount"), static_cast<DWORD>(5));
        count_   = v1;

        RegCloseKey(hkey);
    }
    catch (...) { CUBE_LOG << _T("Registry error"); }
}

/* ------------------------------------------------------------------------- */
///
/// Open
///
/// <summary>
/// HKEY_CURRENT_USER 下にあるサブキーを読み込み専用で開きます。
/// </summary>
///
/* ------------------------------------------------------------------------- */
HKEY QueryInfoSettings::Open(const TString& name) {
    HKEY dest;
    auto result = RegOpenKeyEx(HKEY_CURRENT_USER, name.c_str(), 0, KEY_READ, &dest);
    return result == ERROR_SUCCESS ? dest : nullptr;
}

/* ------------------------------------------------------------------------- */
///
/// GetDword
///
/// <summary>
/// DWORD の値を取得します。
/// </summary>
///
/* ------------------------------------------------------------------------- */
DWORD QueryInfoSettings::GetDword(HKEY hkey, const TString& name, DWORD alternate) {
    DWORD dest = 0;
    DWORD size = sizeof(dest);
    auto result = RegQueryValueEx(hkey, name.c_str(), nullptr, nullptr, reinterpret_cast<LPBYTE>(&dest), &size);
    return result == ERROR_SUCCESS ? dest : alternate;
}

}}} // Cube::FileSystem::Ice