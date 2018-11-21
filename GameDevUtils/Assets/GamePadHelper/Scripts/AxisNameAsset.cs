using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    [CreateAssetMenu(fileName = "AxisNames", menuName = "GamePad Input/Assets/Axis Names", order = 1)]
    public class AxisNameAsset : ScriptableObject
    {
        public string LeftAnalogStickX  = DefaultAxisNames.LeftAnalogStickX;
        public string LeftAnalogStickY  = DefaultAxisNames.LeftAnalogStickY;
        public string RightAnalogStickX = DefaultAxisNames.RightAnalogStickX;
        public string RightAnalogStickY = DefaultAxisNames.RightAnalogStickY;
        public string DPadX             = DefaultAxisNames.DPadX;
        public string DPadY             = DefaultAxisNames.DPadY;
        public string LTrigger          = DefaultAxisNames.LTrigger;
        public string RTrigger          = DefaultAxisNames.RTrigger;
    }

    public static class DefaultAxisNames
    {
        public static string LeftAnalogStickX = "Analog Left X";
        public static string LeftAnalogStickY = "Analog Left Y";
        public static string RightAnalogStickX = "Analog Right X";
        public static string RightAnalogStickY = "Analog Right Y";
        public static string DPadX = "D Pad X";
        public static string DPadY = "D Pad Y";
        public static string LTrigger = "Left Trigger";
        public static string RTrigger = "Right Trigger";
    }
}