using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GamePadHelper;

namespace GamePadHelper.Editor
{
    public class XInputSetting : EditorWindow
    {
        public class XInputAxisNameSet
        {
            public string LeftAnalogStickX;
            public string LeftAnalogStickY;
            public string RightAnalogStickX;
            public string RightAnalogStickY;

            public string DPadX;
            public string DPadY;

            public string LTrigger;
            public string RTrigger;

            public XInputAxisNameSet()
            {
                SetDefault(1);
            }

            public XInputAxisNameSet(int playerId)
            {
                SetDefault(playerId);
            }

            public void SetDefault(int playerId)
            {
                switch (playerId)
                {
                    case 1:
                        LeftAnalogStickX    = XInputAxisNames.LeftAnalogStickX;
                        LeftAnalogStickY    = XInputAxisNames.LeftAnalogStickY;
                        RightAnalogStickX   = XInputAxisNames.RightAnalogStickX;
                        RightAnalogStickY   = XInputAxisNames.RightAnalogStickY;
                        DPadX               = XInputAxisNames.DPadX;
                        DPadY               = XInputAxisNames.DPadY;
                        LTrigger            = XInputAxisNames.LTrigger;
                        RTrigger            = XInputAxisNames.RTrigger;
                        break;
                }
            }
        }

        [MenuItem("GamePadHelper/InputManager Update")]
        private static void OpenWindow()
        {
            var wind = EditorWindow.GetWindow<XInputSetting>();
            wind.Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Apply Inputs"))
            {
                ApplyValues();
            }
        }

        private void ApplyValues()
        {
            var tmp = new XInputAxisNameSet();

            AddAxis(MakeInputAxis(tmp.LeftAnalogStickX, 1));
            AddAxis(MakeInputAxis(tmp.LeftAnalogStickY, 2));
            AddAxis(MakeInputAxis(tmp.RightAnalogStickX, 4));
            AddAxis(MakeInputAxis(tmp.RightAnalogStickY, 5));
            AddAxis(MakeInputAxis(tmp.DPadX, 6));
            AddAxis(MakeInputAxis(tmp.DPadY, 7));
            AddAxis(MakeInputAxis(tmp.LTrigger, 9));
            AddAxis(MakeInputAxis(tmp.RTrigger, 10));
        }

        private InputAxis MakeInputAxis(string name, int axis)
        {
            var result = new InputAxis();

            result.name = name;
            result.axis = axis;

            // Any positive or negative values that are less than this number will register as zero. Useful for joysticks.
            result.dead = 0.001f;

            // For keyboard input, a larger value will result in faster response time. 
            // A lower value will be more smooth. 
            // For Mouse delta the value will scale the actual mouse delta.
            result.sensitivity = 3;

            // How fast will the input recenter. Only used when the Type is key / mouse button.
            result.gravity = 3;
            result.snap = true;

            result.type = AxisType.JoystickAxis;

            result.joyNum = 1;

            return result;
        }


        private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
        {
            SerializedProperty child = parent.Copy();
            child.Next(true);
            do
            {
                if (child.name == name) return child;
            }
            while (child.Next(false));
            return null;
        }

        private static bool AxisDefined(string axisName)
        {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

            axesProperty.Next(true);
            axesProperty.Next(true);
            while (axesProperty.Next(false))
            {
                SerializedProperty axis = axesProperty.Copy();
                axis.Next(true);
                if (axis.stringValue == axisName) return true;
            }
            return false;
        }

        public enum AxisType
        {
            KeyOrMouseButton = 0,
            MouseMovement = 1,
            JoystickAxis = 2
        };

        public class InputAxis
        {
            public string name;
            public string descriptiveName;
            public string descriptiveNegativeName;
            public string negativeButton;
            public string positiveButton;
            public string altNegativeButton;
            public string altPositiveButton;

            public float gravity;
            public float dead;
            public float sensitivity;

            public bool snap = false;
            public bool invert = false;

            public AxisType type;

            public int axis;
            public int joyNum;
        }

        private static void AddAxis(InputAxis axis)
        {
            if (AxisDefined(axis.name)) return;

            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

            axesProperty.arraySize++;
            serializedObject.ApplyModifiedProperties();

            SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

            GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
            GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
            GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
            GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
            GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
            GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
            GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
            GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
            GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
            GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
            GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
            GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
            GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
            GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
            GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

            serializedObject.ApplyModifiedProperties();
        }
    }
}