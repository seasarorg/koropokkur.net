#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
using System.IO;
using System.Reflection;
using EnvDTE;
using VSLangProj;

namespace AddInCommon.Util
{
    /// <summary>
    /// �A�Z���u������g�p�����������舵�����[�e�B���e�B
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
        /// �h�L�������g��񂩂�^����擾����
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetTypeName(Document document)
        {
            return GetTypeName(document.FullName);
        }

        /// <summary>
        /// �h�L�������g��񂩂�^����擾����
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public static string GetTypeName(string documentPath)
        {
            string ns = GetNamespace(documentPath);
            string className = Path.GetFileNameWithoutExtension(documentPath);
            if(string.IsNullOrEmpty(ns))
            {
                return className;
            }
            return ns + "." + className;
        }

        /// <summary>
        /// �t�@�C�����疼�O��Ԃ�擾
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public static string GetNamespace(string documentPath)
        {
            string ns = "";
            using (StreamReader reader = new StreamReader(documentPath))
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
            return ns;
        }

        /// <summary>
        /// �h�L�������g��񂩂�A�Z���u������擾
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetAssemblyName(Document document)
        {
            EnvDTE.Project project = document.ProjectItem.ContainingProject;
            EnvDTE.Configuration config =
               project.ConfigurationManager.ActiveConfiguration;

            // =====================================================
            //                      �r���h�o�͐�̃p�X��擾���ĕ\��
            //                      ================================
            string currentAssemblyPath =
               string.Format(@"{0}{1}{2}",
                   project.Properties.Item("FullPath").Value,
                   config.Properties.Item("OutputPath").Value,
                   project.Properties.Item("OutputFileName").Value);
            return currentAssemblyPath;
        }

        /// <summary>
        /// �Q�Ɛ�A�Z���u���p�X�ꗗ��擾
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetReferencePath(Document document)
        {
            EnvDTE.Project project = document.ProjectItem.ContainingProject;
            VSProject vsProject = project.Object as VSProject;
            if(vsProject == null)
            {
                return null;
            }

            List<string> referencePaths = new List<string>();
            referencePaths.Add(GetAssemblyName(document));

            foreach (Reference reference in vsProject.References)
            {
                referencePaths.Add(reference.Path);
            }

            return string.Join(",", referencePaths.ToArray());
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
        /// �N���X������Type����擾
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
        /// �N���X����g���Č^��擾����
        /// </summary>
        /// <param name="className">���O��Ԃ�܂ރN���X��</param>
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