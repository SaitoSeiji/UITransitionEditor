using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

namespace aoji_EditorUI {
    //SearchWindowをProvideするものです
    public class SampleSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private TermGraphView _graphView;

        public void Initialize(TermGraphView graphView)
        {
            this._graphView = graphView;
        }

        List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && (type.IsSubclassOf(typeof(TermNode)))
                        && type != typeof(TermNode))
                    {
                        entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 1, userData = type });
                    }
                }
            }

            return entries;
        }

        bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var type = searchTreeEntry.userData as System.Type;
            var node = Activator.CreateInstance(type) as TermNode;
            _graphView.AddNode(node);
            return true;
        }
    }
}
