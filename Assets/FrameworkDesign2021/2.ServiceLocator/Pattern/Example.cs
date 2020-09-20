using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FrameworkDesign2021.ServiceLocator.Pattern.Example
{
    public class Example : MonoBehaviour
    {
        /// <summary>
        /// 自定义的 InitialContext
        /// </summary>
        public class InitialContext : AbstractInitialContext
        {
            public override IService LookUp(string name)
            {
                // 获取 Example 所在的 Service
                var assembly = typeof(Example).Assembly;

                var serviceType = typeof(IService);

                var service = assembly
                    .GetTypes()

                    // 获取所有实现 IService 接口的类型
                    .Where(t => serviceType.IsAssignableFrom(t) && !t.IsAbstract)
                    // 创建实例
                    .Select(t => t.GetConstructors().First<ConstructorInfo>().Invoke(null))
                    // 转型为 IService
                    .Cast<IService>()
                    // 获取符合条件的 Service 对象
                    .SingleOrDefault(s => s.Name == name);

                return service;
            }
        }

        /// <summary>
        /// 蓝牙服务
        /// </summary>
        public class BluetoothService : IService
        {
            /// <summary>
            /// 服务
            /// </summary>
            public string Name
            {
                get { return "bluetooth"; }
            }

            public void Execute()
            {
                Debug.Log("蓝牙服务启动");
            }
        }

        void Start()
        {
            // 创建查找器
            var context = new InitialContext();

            // 创建服务定位器
            var serviceLocator = new ServiceLocator(context);

            // 获取蓝牙服务
            var bluetoothServie = serviceLocator.GetService("bluetooth");

            // 执行服务
            bluetoothServie.Execute();
        }
    }
}