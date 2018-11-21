using UnityEngine;
using System.Collections.Generic;

namespace GamePadHelper
{
    // REF : http://wiki.unity3d.com/index.php/Xbox360Controller
    public class XInputHelper : ControllerHelperBase
    {
        protected override bool TryProcessAxisValue(GamePadKey key, KeyPhase phase)
        {
            string axisName = string.Empty;
            bool isPositiveValue = true;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            switch (key)
            {
                case GamePadKey.D_UP:
                    axisName = axisNames.DPadY;
                    isPositiveValue = true;
                    break;
                case GamePadKey.D_DOWN:
                    axisName = axisNames.DPadY;
                    isPositiveValue = false;
                    break;
                case GamePadKey.D_LEFT:
                    axisName = axisNames.DPadX;
                    isPositiveValue = false;
                    break;
                case GamePadKey.D_RIGHT:
                    axisName = axisNames.DPadX;
                    isPositiveValue = true;
                    break;

                case GamePadKey.LT:
                    axisName = axisNames.LTrigger;
                    isPositiveValue = true;
                    break;
                case GamePadKey.RT:
                    axisName = axisNames.RTrigger;
                    isPositiveValue = true;
                    break;

                case GamePadKey.ANALOG_L_UP:
                    axisName = axisNames.LeftAnalogStickY;
                    isPositiveValue = true;
                    break;
                case GamePadKey.ANALOG_L_DOWN:
                    axisName = axisNames.LeftAnalogStickY;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_L_LEFT:
                    axisName = axisNames.LeftAnalogStickX;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_L_RIGHT:
                    axisName = axisNames.LeftAnalogStickX;
                    isPositiveValue = true;
                    break;

                case GamePadKey.ANALOG_R_UP:
                    axisName = axisNames.RightAnalogStickY;
                    isPositiveValue = true;
                    break;
                case GamePadKey.ANALOG_R_DOWN:
                    axisName = axisNames.RightAnalogStickY;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_R_LEFT:
                    axisName = axisNames.RightAnalogStickX;
                    isPositiveValue = false;
                    break;
                case GamePadKey.ANALOG_R_RIGHT:
                    axisName = axisNames.RightAnalogStickX;
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
    }
}