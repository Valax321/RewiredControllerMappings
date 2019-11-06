using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Valax321.ControllerPrompts.Editor
{
    [CustomEditor(typeof(GlyphMapAlias))]
    public class GlyphMapAliasEditor : UnityEditor.Editor
    {
        private GlyphMapAlias alias;
        private SerializedProperty m_original;
        [SerializeField] private bool m_foldOut;
        
        private void OnEnable()
        {
            alias = (GlyphMapAlias)serializedObject.targetObject;
            m_original = serializedObject.FindProperty("m_original");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.HelpBox("Associate a controller glyph GUID with a human readable name using this asset.", MessageType.Info);
            EditorGUILayout.PropertyField(m_original, new GUIContent("Glyph Map Asset"));
            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.Separator();

            m_foldOut = EditorGUILayout.Foldout(m_foldOut, new GUIContent("Glyph Asset"), true);
            if (alias.hasAsset && m_foldOut)
            {
                EditorGUI.indentLevel++;
                GUI.enabled = false;
                foreach (var e in alias)
                {
                    EditorGUILayout.ObjectField(new GUIContent($"ID: {e.id}"), e.asset, typeof(Sprite), false);
                }

                GUI.enabled = true;
                EditorGUI.indentLevel--;
            }
            else if (!alias.hasAsset && m_foldOut)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("Assign an asset to this alias to see preview", MessageType.Info);
                EditorGUI.indentLevel--;
            }
        }
    }
}