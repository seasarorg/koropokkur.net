using System.IO;
using EnvDTE;
using EnvDTE80;
using NUnit.Framework;

namespace VSArrangeTest.Console
{
    [TestFixture]
    public class ActivatorTest
    {
        [Test]
        public void TestGetSolutionObject()
        {
            const string TEST_SOL_PATH = @"E:\source\seasar\s2net_40\source\Seasar.sln";

            DTE2 vs = null;
            try
            {
                // DTE2オブジェクト取得
                vs = (DTE2)System.Activator.CreateInstance(
                    System.Type.GetTypeFromProgID("VisualStudio.DTE.9.0"));
                // VisualStudio2010で作成したソリューションファイルを開く
                vs.Solution.Open(TEST_SOL_PATH);
                System.Console.WriteLine(vs.Solution.FullName);

                //// 読み込んだソリューション下のプロジェクト名を出力
                //foreach (Project p in vs.Solution.Projects)
                //{
                //    System.Console.WriteLine(p.FullName);
                //}
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            finally
            {
                if (vs != null && vs.Solution.IsOpen)
                {
                    vs.Solution.Close();
                }
            }
        }
    }
}
