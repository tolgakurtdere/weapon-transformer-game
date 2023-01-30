using LoxiGames.Settings;
using UnityEditor;
using UnityEngine;

namespace LoxiGames
{
    public class GameSettingsViewer : EditorWindow
    {
        private static GameSettings _target;
        private static Editor _editor;

        [MenuItem("Loxi Games/GameSettings Viewer", priority = 0)]
        private static void ShowWindow()
        {
            GetWindow(typeof(GameSettingsViewer));
            _target = Resources.Load<GameSettings>("GameSettings");
            if (_target != null)
            {
                _editor = Editor.CreateEditor(_target);
            }
        }

        private void OnGUI()
        {
            if (!_target || !_editor)
            {
                _target = Resources.Load<GameSettings>("GameSettings");

                if (_target)
                {
                    _editor = Editor.CreateEditor(_target);
                }

                if (_target && _editor) return;

                EditorGUILayout.HelpBox("There is no GameSettings file in any Resources folder!", MessageType.Error);
            }
            else
            {
                _editor.OnInspectorGUI();
            }
        }
    }
}