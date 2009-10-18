#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using Microsoft.VisualStudio.CommandBars;
using VSArrange.Control;

namespace VSArrange
{
	/// <summary>アドインを実装するためのオブジェクトです。</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2
	{
        private DTE2 _applicationObject;
        private AddIn _addInInstance;
        private readonly IList<CommandBarControl> _commandBars;

		/// <summary>アドイン オブジェクトのコンストラクタを実装します。初期化コードをこのメソッド内に配置してください。</summary>
		public Connect()
		{
            _commandBars = new List<CommandBarControl>();
		}

		/// <summary>IDTExtensibility2 インターフェイスの OnConnection メソッドを実装します。アドインが読み込まれる際に通知を受けます。</summary>
		/// <param term='application'>ホスト アプリケーションのルート オブジェクトです。</param>
		/// <param term='connectMode'>アドインの読み込み状態を説明します。</param>
		/// <param term='addInInst'>このアドインを表すオブジェクトです。</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
            //  起動時かアドインマネージャ選択時
            if (connectMode == ext_ConnectMode.ext_cm_Startup ||
                connectMode == ext_ConnectMode.ext_cm_AfterStartup)
            {
                try
                {
                    #region 「プロジェクト要素の整理」メニュー追加
                    //  プロジェクト要素整理コントロール生成
                    ArrangeControl refreshControl = new ArrangeControl(_applicationObject);

                    //  ソリューションアイコン上右クリックメニュー追加
                    CommandBar solutionMenu = CommandBarUtils.GetSolutionContextMenu(_applicationObject);
                    CommandBarControl solutionMenuItem =
                        refreshControl.CreateSolutionContextMenuItem(solutionMenu);
                    if (!_commandBars.Contains(solutionMenuItem))
                    {
                        _commandBars.Add(solutionMenuItem);
                    }

                    //  プロジェクトアイコン上右クリックメニュー追加
                    CommandBar projectMenu = CommandBarUtils.GetProjectContextMenu(_applicationObject);
                    CommandBarControl projectMenuItem = refreshControl.CreateProjectContextMenuItem(
                        projectMenu);
                    if (!_commandBars.Contains(projectMenuItem))
                    {
                        _commandBars.Add(projectMenuItem);
                    }
                    #endregion

                    #region メニューに追加

                    ConfigMenu menu = new ConfigMenu();

                    CommandBarPopup popup = CommandBarUtils.GetKoropokkurConfigMenu(_applicationObject, _commandBars);
                    CommandBarControl configMenu = menu.CreateSolutionContextMenuItem(popup);
                    if (!_commandBars.Contains(configMenu))
                    {
                        _commandBars.Add(configMenu);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("アドインの読み込みに失敗しました。\n" + ex.Message,
                        "Koropokkur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
		}

		/// <summary>IDTExtensibility2 インターフェイスの OnDisconnection メソッドを実装します。アドインがアンロードされる際に通知を受けます。</summary>
		/// <param term='disconnectMode'>アドインのアンロード状態を説明します。</param>
		/// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
            if (_commandBars != null)
            {
                _commandBars.Clear();
            }
		}

		/// <summary>IDTExtensibility2 インターフェイスの OnAddInsUpdate メソッドを実装します。アドインのコレクションが変更されたときに通知を受けます。</summary>
		/// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>IDTExtensibility2 インターフェイスの OnStartupComplete メソッドを実装します。ホスト アプリケーションが読み込みを終了したときに通知を受けます。</summary>
		/// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
		}

		/// <summary>IDTExtensibility2 インターフェイスの OnBeginShutdown メソッドを実装します。ホスト アプリケーションがアンロードされる際に通知を受けます。</summary>
		/// <param term='custom'>ホスト アプリケーション固有のパラメータの配列です。</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
		}
	}
}