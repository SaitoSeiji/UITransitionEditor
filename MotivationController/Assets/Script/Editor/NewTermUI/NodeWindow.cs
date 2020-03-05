using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class NodeWindow : DefaultWindow
    {
        public static void OpenWindow()
        {
            ShowWindow<NodeWindow>();
        }

        SanpleGraphView graphView;
        Box panel;

        bool beforeTrue = false;
        private void OnGUI()
        {
            if (graphView.a()&&!beforeTrue)
            {
                SetPanelContent_Arrow();
                beforeTrue = true;
            }
            else if(!graphView.a()&&beforeTrue)
            {
                ResetPanelContent();
                beforeTrue = false;
            }
        }

        override protected void OnEnable()
        {
            base.OnEnable();
            graphView = new SanpleGraphView()
            {
                style = { flexGrow = 1 }
            };

            rootVisualElement.Add(graphView);
            panel = new Box()
            {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(panel);

        }

        void ResetPanelContent()
        {
            panel.Clear();
        }

        void SetPanelContent_Arrow()
        {
            panel.Add(new Label("hoge"));
        }
    }
}
