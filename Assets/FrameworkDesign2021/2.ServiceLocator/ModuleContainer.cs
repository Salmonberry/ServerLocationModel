using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkDesign2021
{
    public class ModuleContainer<T>
    {
        private List<T> mModules = new List<T>();

        public List<T> Modules
        {
            get { return mModules; }
        }

        public void Scan(string assemblyName)
        {

            // 1.获取当前项目中所有的 assembly (可以理解为 代码编译好的 dll)
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // 2.获取指定程序集(dll)
            var assembly = assemblies.First(a => a.FullName.StartsWith(assemblyName));
            // 3.获取 IEditorPlatformModule 类型
            var moduleType = typeof(T);

            mModules = assembly
                // 获取所有的指定环境中的类型 
                .GetTypes()
                // 过滤掉抽象类型（接口/抽象类)、和未实现 IEditorPlatformModule 的类型
                .Where(type => moduleType.IsAssignableFrom(type) && !type.IsAbstract)
                // 获取类型的构造创建实例
                .Select(type => type.GetConstructors().First().Invoke(null))
                // 强制转换成 IEditorPlatformModule 类型
                .Cast<T>()
                // 转换成 List<IEditorPlatformModule>
                .ToList();
        }
    }
}