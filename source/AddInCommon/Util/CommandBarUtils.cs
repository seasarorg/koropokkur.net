using System;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace AddInCommon.Util
{
    /// <summary>
    /// CommandBar利用ユーティリティ
    /// </summary>
    public class CommandBarUtils
    {
        /// <summary>
        /// ソリューション右クリックで表示されるメニューコントロールを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetSolutionContextMenu(DTE2 applicationObject)
        {
            return GetCommandBar(applicationObject, "Solution");
        }

        /// <summary>
        /// プロジェクト右クリックで表示されるメニューコントロールを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetProjectContextMenu(DTE2 applicationObject)
        {
            return GetCommandBar(applicationObject, "Project");
        }

        /// <summary>
        /// 引数コントロールの一番後ろにメニューを一つ追加する
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public static CommandBarButton CreateButtonControl(CommandBar commandBar)
        {
            return (CommandBarButton)commandBar.Controls.Add(
                                         MsoControlType.msoControlButton, Type.Missing,
                                         Type.Missing, Type.Missing, true);
        }

        #region 補助メソッド

        /// <summary>
        /// コントロール取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="commandBarName"></param>
        /// <returns></returns>
        private static CommandBar GetCommandBar(DTE2 applicationObject, string commandBarName)
        {
            return ((CommandBars)applicationObject.CommandBars)[commandBarName];
        }

        #endregion
    }
}