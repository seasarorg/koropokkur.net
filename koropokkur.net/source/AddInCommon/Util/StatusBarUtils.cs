using EnvDTE80;

namespace AddInCommon.Util
{
    /// <summary>
    /// 画面下部のステータスバー制御ユーティリティ
    /// </summary>
    public class StatusBarUtils
    {
        /// <summary>
        /// 表示しているステータスバーを消す
        /// </summary>
        /// <param name="applicationObject"></param>
        public static void Clear(DTE2 applicationObject)
        {
            applicationObject.StatusBar.Progress(false, "", 0, 0);
            applicationObject.StatusBar.Clear();
        }
    }
}