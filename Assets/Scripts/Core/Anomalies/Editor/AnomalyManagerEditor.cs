using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Anomalies
{
    [CustomEditor(typeof(AnomalyManager))]
    public class AnomalyManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            /*base.OnInspectorGUI();
            return;*/
            
            AnomalyManager anomalyManager = (AnomalyManager)target;

            serializedObject.Update();

            SerializedProperty probabilityForBaseFloorProperty =
                serializedObject.FindProperty("probabilityForBaseFloor");
            SerializedProperty gimmickFloorsProperty = serializedObject.FindProperty("gimmickFloors");
            SerializedProperty easyFloorsProperty = serializedObject.FindProperty("easyFloors");
            SerializedProperty normalFloorsProperty = serializedObject.FindProperty("normalFloors");
            SerializedProperty hardFloorsProperty = serializedObject.FindProperty("hardFloors");
            SerializedProperty impossibleFloorsProperty = serializedObject.FindProperty("impossibleFloors");

            //EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(probabilityForBaseFloorProperty);

            DrawAnomalyProbabilityArray();

            EditorGUILayout.PropertyField(gimmickFloorsProperty);
            EditorGUILayout.PropertyField(easyFloorsProperty);
            EditorGUILayout.PropertyField(normalFloorsProperty);
            EditorGUILayout.PropertyField(hardFloorsProperty);
            EditorGUILayout.PropertyField(impossibleFloorsProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAnomalyProbabilityArray()
        {
            SerializedProperty floorsProperty = serializedObject.FindProperty("floors");

            EditorGUILayout.BeginVertical();

            // Display table headers
            EditorGUILayout.BeginHorizontal();
            float labelWidth = EditorGUIUtility.currentViewWidth / 5f;

            GUILayout.Space(30);
            
            GUILayout.FlexibleSpace();
            GUILayout.Label("Floor", GUILayout.Width(labelWidth), GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Easy", GUILayout.Width(labelWidth), GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Normal", GUILayout.Width(labelWidth), GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Hard", GUILayout.Width(labelWidth), GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Impossible", GUILayout.Width(labelWidth), GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();


            for (int i = floorsProperty.arraySize - 1; i >= 0 ; i--)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(EditorGUIUtility.FindTexture("TreeEditor.Trash"), GUILayout.Width(24),
                        GUILayout.Height(24)))
                {
                    floorsProperty.DeleteArrayElementAtIndex(i);
                }
                GUILayout.Space(6);
                GUIStyle style = new GUIStyle() { alignment = TextAnchor.MiddleCenter };
                style.normal.textColor = Color.white;
                
                GUILayout.Label((i + 1).ToString(),  style,GUILayout.Width(24), GUILayout.Height(24));
                EditorGUILayout.PropertyField(floorsProperty.GetArrayElementAtIndex(i), GUILayout.Height(24));
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Floor"))
            {
                floorsProperty.InsertArrayElementAtIndex(floorsProperty.arraySize);
            }

            EditorGUILayout.EndVertical();
        }
    }
}