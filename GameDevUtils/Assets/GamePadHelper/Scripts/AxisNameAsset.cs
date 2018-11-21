using UnityEngine;

namespace GamePadHelper
{
    [CreateAssetMenu(fileName = "AxisNames", menuName = "GamePad Input/Assets/Axis Names", order = 1)]
    public class AxisNameAsset : ScriptableObject
    {
        [Header("Left Stick")]
        public string LeftAnalogStickX = DefaultAxisNames.LeftAnalogStickX;
        [Range(0f, 1f)]
        public float LStickXThreshold = 1f;

        [Space]
        public string LeftAnalogStickY = DefaultAxisNames.LeftAnalogStickY;
        [Range(0f, 1f)]
        public float LStickYThreshold = 1f;

        [Space]
        public string RightAnalogStickX = DefaultAxisNames.RightAnalogStickX;
        [Range(0f, 1f)]
        public float RStickXThreshold = 1f;

        [Space]
        public string RightAnalogStickY = DefaultAxisNames.RightAnalogStickY;
        [Range(0f, 1f)]
        public float RStickYThreshold = 1f;

        [Space]
        public string DPadX = DefaultAxisNames.DPadX;
        public string DPadY = DefaultAxisNames.DPadY;

        [Space]
        public string LTrigger = DefaultAxisNames.LTrigger;
        [Range(0f, 1f)]
        public float LTriggerThreshold = 1f;

        [Space]
        public string RTrigger = DefaultAxisNames.RTrigger;
        [Range(0f, 1f)]
        public float RTriggerThreshold = 1f;
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