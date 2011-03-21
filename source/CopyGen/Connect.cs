#region Copyright
/*
 * Copyright 2005-2011 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using AddInCommon.Core;
using CopyGen.Core;
using EnvDTE;
using Extensibility;

namespace CopyGen
{
    /// <summary>アドインを実装するためのオブジェクトです。</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        private readonly ConnectCoreBase _core;

        /// <summary>アドイン オブジェクトのコンストラクタを実装します。初期化コードをこのメソッド内に配置してください。</summary>
        public Connect()
        {
            _core = new CopyGenConnectCore();
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnConnection メソッドを実装します。アドインが読み込まれる際に通知を受けます。</summary>
        /// <param term='application'>ホスト アプリケーションのルート オブジェクトです。</param>
        /// <param term='connectMode'>アドインの読み込み状態を説明します。</param>
        /// <param term='addInInst'>このアドインを表すオブジェクトです。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _core.OnConnection(application, connectMode, addInInst, ref custom);
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnDisconnection メソッドを実装します。アドインがアンロードされる際に通知を受けます。</summary>
        /// <param term='disconnectMode'>アドインのアンロード状態を説明します。</param>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
            _core.OnDisconnection(disconnectMode, ref custom);
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnAddInsUpdate メソッドを実装します。アドインのコレクションが変更されたときに通知を受けます。</summary>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
            _core.OnAddInsUpdate(ref custom);
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnStartupComplete メソッドを実装します。ホスト アプリケーションが読み込みを終了したときに通知を受けます。</summary>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
            _core.OnStartupComplete(ref custom);
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnBeginShutdown メソッドを実装します。ホスト アプリケーションがアンロードされる際に通知を受けます。</summary>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
            _core.OnBeginShutdown(ref custom);
        }

        #region IDTCommandTarget メンバ

        /// <summary>IDTCommandTarget インターフェイスの QueryStatus メソッドを実装します。これは、コマンドの可用性が更新されたときに呼び出されます。</summary>
        /// <param term='commandName'>状態を決定するためのコマンド名です。</param>
        /// <param term='neededText'>コマンドに必要なテキストです。</param>
        /// <param term='status'>ユーザー インターフェイス内のコマンドの状態です。</param>
        /// <param term='commandText'>neededText パラメータから要求されたテキストです。</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            _core.OnQueryStatus(commandName, neededText, ref status, ref commandText);
        }

        /// <summary>IDTCommandTarget インターフェイスの Exec メソッドを実装します。これは、コマンドが実行されるときに呼び出されます。</summary>
        /// <param term='commandName'>実行するコマンド名です。</param>
        /// <param term='executeOption'>コマンドの実行方法を説明します。</param>
        /// <param term='varIn'>呼び出し元からコマンド ハンドラへ渡されたパラメータです。</param>
        /// <param term='varOut'>コマンド ハンドラから呼び出し元へ渡されたパラメータです。</param>
        /// <param term='handled'>コマンドが処理されたかどうかを呼び出し元に通知します。</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            _core.OnExec(commandName, executeOption, ref varIn, ref varOut, ref handled);
        }

        #endregion
    }
}
