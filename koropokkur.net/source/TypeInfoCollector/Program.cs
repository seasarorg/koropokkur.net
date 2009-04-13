using System;
using System.IO;
using System.Reflection;

namespace TypeInfoCollector
{
    class Program
    {
        /// <summary>
        /// 指定したアセンブリのプロパティ名をファイルに書き出します
        /// </summary>
        /// <param name="args">
        /// [0]:読み込むアセンブリのパス
        /// [1]:プロパティ情報を取り出したい型
        /// [2]:プロパティ一覧の出力先
        /// </param>
        static void Main(string[] args)
        {
            if(args.Length < 3)
            {
                return;
            }

            string assemblyPath = args[0];
            string typeName = args[1];
            string outputPath = args[2];

            try
            {
                //  古いプロパティ情報は消しておく
                if(File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }

                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                Type type = assembly.GetType(typeName);
                if(type == null)
                {
                    using(StreamWriter writer = new StreamWriter(string.Format("{0}.warn.log", assemblyPath), true))
                    {
                        writer.WriteLine("{0} {1} is not found in {2}",
                            DateTime.Now, typeName, assemblyPath);
                    }
                    return;
                }

                PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.CanRead && propertyInfo.CanWrite)
                        {
                            writer.WriteLine(propertyInfo.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (var errorWriter = new StreamWriter(string.Format("{0}.error.log", outputPath), true))
                {
                    errorWriter.WriteLine("{0} {1}\n{2}", DateTime.Now, ex.Message, ex.StackTrace);
                }
            }
        }
    }
}
