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

            AddMenuItem();
        }
        
        //右クリックで開くメニューを記述
        void AddMenuItem()
        {
            Event evt = Event.current;

            Rect contextRect = new Rect(0, 0, Screen.width, Screen.height);
            if (evt.type == EventType.ContextClick)
            {
                Vector2 mousePos = evt.mousePosition;

                var node = nodeSet.GetClickNode(mousePos);
                if (node!=null)
                {
                    GenericMenu menu = new GenericMenu();
                    SetRemoveData(node);
                    menu.AddItem(new GUIContent("removeNode"), false, RemoveNode, "nodeItem1");
                    menu.ShowAsContext();
                    evt.Use();
                }
                else if (contextRect.Contains(mousePos))
                {
                    // Now create the menu, add items and show it
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("MenuItem1"), false, CreateNode, "item 1");
                    menu.AddItem(new GUIContent("MenuItem2"), false, CreateNode, "item 2");
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("SubMenu/MenuItem3"), false, CreateNode, "item 3");

                    menu.ShowAsContext();

                    evt.Use();
                }
            }
        }

        #region 右クリックで開くメニューのコールバックと補助関数
        void CreateNode(object obj)
        {
            nodeSet.AddNode();
        }


        NodeData removeData;
        void SetRemoveData(NodeData data)
        {
            removeData = data;
        }
        void RemoveNode(object obj)
        {
            nodeSet.RemoveNode((SimpleNodeData)removeData);
            removeData = null;
        }
        #endregion
    }
}
#endif
