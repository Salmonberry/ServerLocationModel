using UnityEditor;
using UnityEngine;

namespace FrameworkDesign2021
{
    public class EditorModulizationPlatformEditor : EditorWindow
    {
        /// <summary>
        /// 用来缓存模块的容器
        /// </summary>
        static ModuleContainer<IEditorPlatformModule> mModuleContainer = new ModuleContainer<IEditorPlatformModule>();

        /// <summary>
        /// 打开窗口
        /// </summary>
        [MenuItem("FrameworkDesign2021/0.EditorModulizationPlatform")]
        public static void Open()
        {
            var editorPlatform = GetWindow<EditorModulizationPlatformEditor>();

            editorPlatform.position = new Rect(
                Screen.width / 2,
                Screen.height * 2 / 3,
                600,
                500
            );

            editorPlatform.Show();

            // 清空掉之前的实例
            mModuleContainer.Modules.Clear();

            // 扫描 编辑器环境
            mModuleContainer.Scan("Assembly-CSharp-Editor");

            editorPlatform.Show();
        }

        private void OnGUI()
        {
            // 渲染
            foreach (var editorPlatformModule in mModuleContainer.Modules)
            {
                editorPlatformModule.OnGUI();
            }
        }
    }
}