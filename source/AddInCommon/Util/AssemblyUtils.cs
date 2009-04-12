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
using System.IO;
using System.Reflection;
using EnvDTE;

namespace AddInCommon.Util
{
    /// <summary>
    /// �A�Z���u�������g�p������������舵�����[�e�B���e�B
    /// </summary>
    public static class AssemblyUtils
    {
        /// <summary>
        /// ���s���̃A�Z���u���p�X��Ԃ�
        /// </summary>
        /// <returns></returns>
        public static string GetExecutingAssemblyPath()
        {
            return GetAssemblyPath(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// �A�Z���u���p�X��Ԃ�
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetAssemblyPath(Assembly assembly)
        {
            return assembly.CodeBase.Replace("file:///", "");
        }

        /// <summary>
        /// �h�L�������g��񂩂�^�����擾����
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetTypeName(Document document)
        {
            string ns = "";
            using (StreamReader reader = new StreamReader(document.FullName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("namespace"))
                    {
                        //  ���O��Ԗ��̒��o
                        ns = line.Replace("namespace", "").Replace("{", "").Trim();
                        break;
                    }
                }
            }

            return ns + "." + Path.GetFileNameWithoutExtension(document.FullName);
        }
        /// <summary>
        /// �h�L�������g��񂩂�A�Z���u�������擾
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetAssemblyName(Document document)
        {
            EnvDTE.Project project = document.ProjectItem.ContainingProject;
            EnvDTE.Configuration config =
               project.ConfigurationManager.ActiveConfiguration;

            // =====================================================
            //                      �r���h�o�͐�̃p�X���擾���ĕ\��
            //                      ================================
            string currentAssemblyPath =
               string.Format(@"{0}{1}{2}",
                   project.Properties.Item("FullPath").Value,
                   config.Properties.Item("OutputPath").Value,
                   project.Properties.Item("OutputFileName").Value);
            return currentAssemblyPath;
        }

        /// <summary>
        /// �R���X�g���N�^���̎擾
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static ConstructorInfo GetConstructorInfo(Type type, Type[] argTypes)
        {
            Type[] types = argTypes ?? Type.EmptyTypes;
            ConstructorInfo constructor = type.GetConstructor(types);
            return constructor;
        }

        /// <summary>
        /// �N���X������Type�����擾
        /// </summary>
        /// <param name="className"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static Type ForName(string className, Assembly[] assemblys)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                return type;
            }
            foreach (Assembly assembly in assemblys)
            {
                type = assembly.GetType(className);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        /// <summary>
        /// ���ݎg�p�\�ȃA�Z���u���̒�����A
        /// �N���X�����g���Č^���擾����
        /// </summary>
        /// <param name="className">���O��Ԃ��܂ރN���X��</param>
        /// <returns>�Y������^</returns>
        public static Type ForName(string className)
        {
            return ForName(className, AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// �C���X�^���X�̐���
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object NewInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// �N���X���ƃA�Z���u��������C���X�^���X�𐶐�
        /// </summary>
        /// <param name="className"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static object NewInstance(string className, string assemblyName)
        {
            Assembly[] asms = new Assembly[1] { Assembly.LoadFrom(assemblyName) };
            return NewInstance(ForName(className, asms));
        }
    }
}