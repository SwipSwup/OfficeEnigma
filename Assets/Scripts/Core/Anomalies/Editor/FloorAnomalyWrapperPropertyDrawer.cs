using UnityEditor;
using UnityEngine;

namespace Core.Anomalies
{
    [CustomPropertyDrawer(typeof(FloorAnomalyWrapper))]
    public class FloorAnomalyWrapperPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Calculate the width for each element
            int anomalyCount = property.FindPropertyRelative("anomalies").arraySize;
            float elementWidth = position.width / anomalyCount;

            // Draw each anomaly
            for (int i = 0; i < anomalyCount; i++)
            {
                SerializedProperty anomalyProperty = property.FindPropertyRelative("anomalies").GetArrayElementAtIndex(i).FindPropertyRelative("probability");
                Rect elementPosition = new Rect(position.x + i * elementWidth, position.y, elementWidth, position.height);
                //EditorGUI.PropertyField(elementPosition, anomalyProperty, new GUIContent(""));
                DrawAnomalyField(elementPosition, anomalyProperty);
            }

            EditorGUI.EndProperty();
        }
        
        private void DrawAnomalyField(Rect position, SerializedProperty anomalyProperty)
        {
            float labelWidth = 40f;
            
            EditorGUI.DrawRect(position, Color.blue);
            GUI.skin.box.normal.background = MakeTex((int)position.width, (int)position.height, Color.blue);
            //GUI.color = Color.blue;
            
            Rect leftLabelPosition = new Rect(position.x, position.y, labelWidth, position.height);
            EditorGUI.LabelField(leftLabelPosition, "", EditorStyles.centeredGreyMiniLabel);
    
            Rect floatFieldPosition = new Rect(position.x + labelWidth, position.y, position.width - 2 * labelWidth, position.height);
            EditorGUI.PropertyField(floatFieldPosition, anomalyProperty, GUIContent.none);
    
            Rect rightLabelPosition = new Rect(position.x + position.width - labelWidth, position.y, labelWidth, position.height);
            EditorGUI.LabelField(rightLabelPosition, "", EditorStyles.centeredGreyMiniLabel);
        }
        
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}