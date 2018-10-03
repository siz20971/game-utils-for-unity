using UnityEngine;
using System.Collections.Generic;

namespace GamePadHelper
{
    // REF : http://wiki.unity3d.com/index.php/Xbox360Controller
    public class XInputHelper : ControllerHelperBase
    {
        private Vector2 leftStickAxis = Vector2.zero;
        private Vector2 rightStickAxis = Vector2.zero;

        protected override void Initialize()
        {
            axisKeys = new List<string>()
            {
                XInputAxisNames.LeftAnalogStickX,
                XInputAxisNames.LeftAnalogStickY,
                XInputAxisNames.RightAnalogStickX,
                XInputAxisNames.RightAnalogStickY,
                XInputAxisNames.DPadX,
                XInputAxisNames.DPadY,
                XInputAxisNames.LTrigger,
                XInputAxisNames.RTrigger
            };

            axisValueDict = new Dictionary<string, AxisValues>();
            foreach (var key in axisKeys)
                axisValueDict[key] = new AxisValues(key);

            axisValueDict[XInputAxisNames.LTrigger].SetThreshold(1);
            axisValueDict[XInputAxisNames.RTrigger].SetThreshold(1);
            axisValueDict[XInputAxisNames.LeftAnalogStickX].SetThreshold(1);
            axisValueDict[XInputAxisNames.LeftAnalogStickY].SetThreshold(1);
            axisValueDict[XInputAxisNames.RightAnalogStickX].SetThreshold(1);
            axisValueDict[XInputAxisNames.RightAnalogStickY].SetThreshold(1);
        }

        protected override bool TryProcessAxisValue(GamePadKey key, KeyPhase phase)
        {
            string axisName = string.Empty;
            bool isPositiveValue = true;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            switch (key)
            {
                case GamePadKey.D_UP:
                    axisName = XInputAxisNames.DPadY;
                    isPositiveValue = true;
                    break;
                case GamePadKey.D_DOWN:
                    axisName = XInputAxisNames.DPadY;
                    isPositiveValue = false;
                    break;
                case GamePadKey.D_LEFT:
                    axisName = XInputAxisNames.DPadX;
                    isPositiveValue = false;
                    break;
                case GamePadKey.D_RIGHT:
                    axisName = XInputAxisNames.DPadX;
                    isPositiveValue = true;
                    break;

                case GamePadKey.LT:
                    axisName = XInputAxisNames.LTrigger;
                    isPositiveValue = true;
                    break;
                case GamePadKey.RT:
                    axisName = XInputAxisNames.RTrigger;
                    isPositiveValue = true;
                    break;

                case GamePadKey.ANALOG_L_UP:
                    axisName = XInputAxisNames.LeftAnalogStickY;
                    isPositiveValue = true;
                    break;
                case GamePadKey.ANALOG_L_DOWN:
                    axisName = XInputAxisNames.LeftAnalogStickY;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_L_LEFT:
                    axisName = XInputAxisNames.LeftAnalogStickX;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_L_RIGHT:
                    axisName = XInputAxisNames.LeftAnalogStickX;
                    isPositiveValue = true;
                    break;

                case GamePadKey.ANALOG_R_UP:
                    axisName = XInputAxisNames.RightAnalogStickY;
                    isPositiveValue = true;
                    break;
                case GamePadKey.ANALOG_R_DOWN:
                    axisName = XInputAxisNames.RightAnalogStickY;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_R_LEFT:
                    axisName = XInputAxisNames.RightAnalogStickX;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_R_RIGHT:
                    axisName = XInputAxisNames.RightAnalogStickX;
                    isPositiveValue = true;
                    break;
            }

#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            // TODO : Windows 환경만 우선적으로 구현.
#endif

            if (string.IsNullOrEmpty(axisName))
                return false;
            else if (!axisValueDict.ContainsKey(axisName))
                return false;

            if (phase == KeyPhase.UP)
                return axisValueDict[axisName].Released(isPositiveValue);
            else if (phase == KeyPhase.DOWN)
                return axisValueDict[axisName].Press(isPositiveValue);
            else if (phase == KeyPhase.KEEP)
                return axisValueDict[axisName].IsPressing(isPositiveValue);

            return false;
        }

        protected override KeyCode GetKeyCode(GamePadKey key)
        {
            switch (key)
            {
                case GamePadKey.BACK:
                    return KeyCode.Joystick1Button6;

                case GamePadKey.START:
                    return KeyCode.Joystick1Button7;

                case GamePadKey.A:
                case GamePadKey.FUNC_DOWN:
                    return KeyCode.Joystick1Button0;

                case GamePadKey.B:
                case GamePadKey.FUNC_RIGHT:
                    return KeyCode.Joystick1Button1;

                case GamePadKey.Y:
                case GamePadKey.FUNC_UP:
                    return KeyCode.Joystick1Button3;

                case GamePadKey.X:
                case GamePadKey.FUNC_LEFT:
                    return KeyCode.Joystick1Button2;

                case GamePadKey.LB:
                    return KeyCode.Joystick1Button4;

                case GamePadKey.RB:
                    return KeyCode.Joystick1Button5;

                case GamePadKey.L_STICK:
                    return KeyCode.Joystick1Button8;

                case GamePadKey.R_STICK:
                    return KeyCode.Joystick1Button9;
            }

            return KeyCode.None;
        }

        public override Vector2 GetLStickAxis()
        {
            leftStickAxis.x = Input.GetAxis(XInputAxisNames.LeftAnalogStickX);
            leftStickAxis.y = Input.GetAxis(XInputAxisNames.LeftAnalogStickY);
            return leftStickAxis;
        }

        public override Vector2 GetRStickAxis()
        {
            rightStickAxis.x = Input.GetAxis(XInputAxisNames.RightAnalogStickX);
            rightStickAxis.y = Input.GetAxis(XInputAxisNames.RightAnalogStickY);
            return rightStickAxis;
        }
    }

    public static class XInputAxisNames
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