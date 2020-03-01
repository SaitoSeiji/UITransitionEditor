using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class UITransitionWindow : DefaultWindow
    {
        SimpleNodeSet nodeSet = new SimpleNodeSet(new Vector2(100,100),new Vector2(100,100));

        public static void OpenWindow()
        {
            ShowWindow<UITransitionWindow>();
            
        }
        private void OnGUI()
        {
            if (GUILayout.Button("add"))
            {
                nodeSet.AddNode();
            }

            if (GUILayout.Button("remove"))
            {
                nodeSet.RemoveNode(0);
            }

            BeginWindows();
            nodeSet.DrawNode("test", 0);
            EndWindows();
        }
    }
}
#endif
