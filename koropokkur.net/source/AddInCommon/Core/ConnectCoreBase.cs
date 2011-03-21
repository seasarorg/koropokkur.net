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
using System.Collections.Generic;
using System.Windows.Forms;
using AddInCommon.Command;
using AddInCommon.Const;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using Microsoft.VisualStudio.CommandBars;

namespace AddInCommon.Core
{
    /// <summary>
    /// アドインConnect共通処理基底クラス
    /// </summary>
    public class ConnectCoreBase
    {
        private DTE2 _applicationObject;
        private AddIn _addInInstance;
        private readonly IDictionary<string, IDTCExecCommand> _eventCommands;

        /// <summary>アドイン オブジェクトのコンストラクタを実装します。初期化コードをこのメソッド内に配置してください。</summary>
        public ConnectCoreBase()
        {
            _eventCommands = new Dictionary<string, IDTCExecCommand>();
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnConnection メソッドを実装します。アドインが読み込まれる際に通知を受けます。</summary>
        /// <param term='application'>ホスト アプリケーションのルート オブジェクトです。</param>
        /// <param term='connectMode'>アドインの読み込み状態を説明します。</param>
        /// <param term='addInInst'>このアドインを表すオブジェクトです。</param>
        /// <seealso class='IDTExtensibility2' />
        /// <returns></returns>
        public virtual void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2) application;
            _addInInstance = (AddIn) addInInst;

            if (connectMode == ext_ConnectMode.ext_cm_AfterStartup ||
                connectMode == ext_ConnectMode.ext_cm_Startup)
            {
                try
                {
                    RegisterEventCommandAfterStartUp(_eventCommands);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("アドインの再登録はできません。お手数ですが一度VisualStudioを再起動して下さい。",
                        KoropokkurConst.NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                try
                {
                    OnConnectionAfterUISetup(_applicationObject, _addInInstance);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("アドインの初回登録処理に失敗しました。\n" + ex.Message,
                                    KoropokkurConst.NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnDisconnection メソッドを実装します。アドインがアンロードされる際に通知を受けます。</summary>
        /// <param term='disconnectMode'>アドインのアンロード状態を説明します。</param>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />
        public virtual void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
            if(disconnectMode == ext_DisconnectMode.ext_dm_HostShutdown ||
               disconnectMode == ext_DisconnectMode.ext_dm_UserClosed)
            {
                try
                {
                    ////  登録したイベント処理をクリアする
                    foreach (IDTCExecCommand execCommand in _eventCommands.Values)
                    {
                        DeleteCommand(execCommand.CommandName);
                    }

                    //  「Koropokkurの設定」メニューをクリアする
                    CommandBar koropokkurMenu = VSCommandUtils.GetKoropokkurMenuBar(ApplicationObject);

                    //  他にKoropokkur設定を使用しているアドインがない場合に
                    //  Koropokkur設定メニューを削除する
                    if (koropokkurMenu != null && koropokkurMenu.Controls.Count == 0)
                    {
                        CommandBar toolBar = VSCommandUtils.GetCommandBar(_applicationObject, CommandBarConst.TOOL_MENU);
                        foreach (CommandBarControl control in toolBar.Controls)
                        {
                            if (control.Caption == KoropokkurConst.CONFIG_MENU_NAME)
                            {
                                control.Delete();
                                break;
                            }
                        }
                    }
                }
                catch(System.Exception ex)
                {
                    MessageBox.Show("アドインのアンロード処理に失敗しました。\n" + ex.Message,
                                    KoropokkurConst.NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        

        /// <summary>IDTExtensibility2 インターフェイスの OnAddInsUpdate メソッドを実装します。アドインのコレクションが変更されたときに通知を受けます。</summary>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />		
        public virtual void OnAddInsUpdate(ref Array custom)
        {
            //  空実装
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnStartupComplete メソッドを実装します。ホスト アプリケーションが読み込みを終了したときに通知を受けます。</summary>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />
        public virtual void OnStartupComplete(ref Array custom)
        {
            //  空実装
        }

        /// <summary>IDTExtensibility2 インターフェイスの OnBeginShutdown メソッドを実装します。ホスト アプリケーションがアンロードされる際に通知を受けます。</summary>
        /// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
        /// <seealso class='IDTExtensibility2' />
        public virtual void OnBeginShutdown(ref Array custom)
        {
            //  空実装
        }

        #region IDTCommandTarget メンバ

        /// <summary>IDTCommandTarget インターフェイスの QueryStatus メソッドを実装します。これは、コマンドの可用性が更新されたときに呼び出されます。</summary>
        /// <param term='commandName'>状態を決定するためのコマンド名です。</param>
        /// <param term='neededText'>コマンドに必要なテキストです。</param>
        /// <param term='status'>ユーザー インターフェイス内のコマンドの状態です。</param>
        /// <param term='commandText'>neededText パラメータから要求されたテキストです。</param>
        /// <seealso class='Exec' />
        public virtual void OnQueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if(neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (_eventCommands.ContainsKey(commandName))
                {
                    status = _eventCommands[commandName].GetCommandStatus(
                        _applicationObject, _addInInstance, ref commandText);
                    return;
                }
            }
        }

        /// <summary>IDTCommandTarget インターフェイスの Exec メソッドを実装します。これは、コマンドが実行されるときに呼び出されます。</summary>
        /// <param term='commandName'>実行するコマンド名です。</param>
        /// <param term='executeOption'>コマンドの実行方法を説明します。</param>
        /// <param term='varIn'>呼び出し元からコマンド ハンドラへ渡されたパラメータです。</param>
        /// <param term='varOut'>コマンド ハンドラから呼び出し元へ渡されたパラメータです。</param>
        /// <param term='handled'>コマンドが処理されたかどうかを呼び出し元に通知します。</param>
        /// <seealso class='Exec' />
        public virtual void OnExec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (_eventCommands.ContainsKey(commandName))
                {
                    handled = _eventCommands[commandName].Execute(
                        _applicationObject, _addInInstance, ref varIn, ref varOut);
                    return;
                }
            }
        }

        #endregion

        /// <summary>
        /// 動作中のVisualStudio構成情報
        /// </summary>
        public DTE2 ApplicationObject { get { return _applicationObject; } }

        /// <summary>
        /// 動作中のアドイン自体のインスタンス
        /// </summary>
        public AddIn AddInInstance { get { return _addInInstance; } }

        /// <summary>
        /// 登録したCommandの削除
        /// </summary>
        /// <param name="commandName"></param>
        protected virtual void DeleteCommand(string commandName)
        {
            EnvDTE.Command myCommand = _applicationObject.Commands.Item(_addInInstance.ProgID + "." + commandName, -1);
            if (myCommand != null)
            {
                myCommand.Delete();
            }
        }

        /// <summary>
        /// アドイン起動のタイミングでIDTCCommandTarget#Execイベント処理を登録する
        /// </summary>
        /// <param name="commands"></param>
        protected virtual void RegisterEventCommandAfterStartUp(IDictionary<string, IDTCExecCommand> commands)
        {
            //  空実装。イベント処理を登録する場合はこのメソッドを拡張する
        }

        /// <summary>
        /// アドイン初回起動のタイミングでの処理実行
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="addInInstance"></param>
        protected virtual void OnConnectionAfterUISetup(DTE2 applicationObject, AddIn addInInstance)
        {
            //  空実装。イベント処理を登録する場合はこのメソッドを拡張する
        }
    }
}
