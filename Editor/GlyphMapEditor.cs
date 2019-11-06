using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Valax321.ControllerPrompts.Editor
{
    [CustomEditor(typeof(GlyphMap))]
    public class GlyphMapEditor : UnityEditor.Editor
    {
        private const string k_headerLabel = "Glyphs";
        private const string k_idLabel = "ID";
        private const string k_glyphLabel = "Glyph";
        
        private SerializedProperty m_glyphAssetList;
        private SerializedProperty m_defaultGlyph;

        private ReorderableList m_itemList;

        private void OnEnable()
        {
            m_glyphAssetList = serializedObject.FindProperty("m_glyphs");
            m_defaultGlyph = serializedObject.FindProperty("m_defaultGlyph");
            m_itemList = new ReorderableList(serializedObject, m_glyphAssetList, true, true, true, true);
            m_itemList.drawHeaderCallback += rect =>
            {
                EditorGUI.LabelField(rect, k_headerLabel);
            };

            m_itemList.drawElementCallback += (rect, index, active, focused) =>
            {
                var propRoot = m_glyphAssetList.GetArrayElementAtIndex(index);
                var id = propRoot.FindPropertyRelative("id");
                var asset = propRoot.FindPropertyRelative("asset");

                var h = EditorGUI.GetPropertyHeight(id);

                EditorGUI.PropertyField(rect, id, new GUIContent(k_idLabel));

                rect.y += h;
                rect.height -= h;

                EditorGUI.PropertyField(rect, asset, new GUIContent(k_glyphLabel));
            };
            
            m_itemList.elementHeightCallback += index =>
            {
                var propRoot = m_glyphAssetList.GetArrayElementAtIndex(index);
                var id = propRoot.FindPropertyRelative("id");
                var asset = propRoot.FindPropertyRelative("asset");
                var y = EditorGUI.GetPropertyHeight(id);
                y += EditorGUI.GetPropertyHeight(asset);
                return y;
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_defaultGlyph);
            m_itemList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}