﻿
using UnityEditor;
using UnityEngine;

namespace Kandooz
{
    [CustomEditor(typeof(GameEvent))]
    public class EventEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}