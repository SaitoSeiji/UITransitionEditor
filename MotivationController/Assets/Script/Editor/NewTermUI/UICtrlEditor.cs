﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using aojiru_UI;

namespace aoji_EditorUI
{
    [CustomEditor(typeof(CanvasBaseCtrl))]
    public class UICtrlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("openEditor"))
            {
                NodeWindow.OpenWindow();
            }
        }
    }
}
#endif