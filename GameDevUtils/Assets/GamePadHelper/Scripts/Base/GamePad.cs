using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    public class GamePad : GamePadBase
    {
        private int gamePadIndex = 0;

        private GamePadType gamePadType = GamePadType.XBOX;

        public GamePad()
        {
            gamePadIndex = 1;
            gamePadType = GamePadType.XBOX;
            ApplyAxisName(string.Empty);
        }

        public void SetGamePadIndex(int gamePadIndex)
        {
            this.gamePadIndex = gamePadIndex;
        }

        public void SetGamePadType(GamePadType type)
        {
            gamePadType = type;
        }

        public void ApplyAxisName(string axisNameAssetPath)
        {
            AxisNameAsset axisNames = Resources.Load<AxisNameAsset>(axisNameAssetPath);
            if (axisNames == null)
            {
                axisNames = new AxisNameAsset();
                axisNames.LeftAnalogStickX = DefaultAxisNames.LeftAnalogStickX;
                axisNames.LeftAnalogStickY = DefaultAxisNames.LeftAnalogStickY;
                axisNames.RightAnalogStickX = DefaultAxisNames.RightAnalogStickX;
                axisNames.RightAnalogStickY = DefaultAxisNames.RightAnalogStickY;
                axisNames.DPadX = DefaultAxisNames.DPadX;
                axisNames.DPadY = DefaultAxisNames.DPadY;
                axisNames.LTrigger = DefaultAxisNames.LTrigger;
                axisNames.RTrigger = DefaultAxisNames.RTrigger;
            }

            axisKeys = new List<string>()
            {
                axisNames.LeftAnalogStickX,
                axisNames.LeftAnalogStickY,
                axisNames.RightAnalogStickX,
                axisNames.RightAnalogStickY,
                axisNames.DPadX,
                axisNames.DPadY,
                axisNames.LTrigger,
                axisNames.RTrigger
            };

            axisValueDict = new Dictionary<string, AxisValues>();
            foreach (var key in axisKeys)
                axisValueDict[key] = new AxisValues(key);

            axisValueDict[axisNames.LTrigger].SetThreshold(axisNames.LTriggerThreshold);
            axisValueDict[axisNames.RTrigger].SetThreshold(axisNames.RTriggerThreshold);
            axisValueDict[axisNames.LeftAnalogStickX].SetThreshold(axisNames.LStickXThreshold);
            axisValueDict[axisNames.LeftAnalogStickY].SetThreshold(axisNames.LStickYThreshold);
            axisValueDict[axisNames.RightAnalogStickX].SetThreshold(axisNames.RStickXThreshold);
            axisValueDict[axisNames.RightAnalogStickY].SetThreshold(axisNames.RStickYThreshold);
        }

        protected override KeyCode GetKeyCode(GamePadKey key)
        {
            if (gamePadType == GamePadType.XBOX)
                return KeyConverter.GetXboxKeyCode(gamePadIndex, key);
            else if (gamePadType == GamePadType.DUALSHOCK)
                return KeyConverter.GetDS4KeyCode(gamePadIndex, key);

            return KeyCode.None;
        }
    }

    internal static class KeyConverter
    {
        // REF : http://wiki.unity3d.com/index.php/Xbox360Controller
        public static KeyCode GetXboxKeyCode(int gamePadNum, GamePadKey key)
        {
            KeyCode keyCode = KeyCode.None;

            switch (key)
            {
                case GamePadKey.BACK:
                    keyCode = KeyCode.Joystick1Button6;
                    break;

                case GamePadKey.START:
                    keyCode = KeyCode.Joystick1Button7;
                    break;

                case GamePadKey.A:
                case GamePadKey.FUNC_DOWN:
                    keyCode = KeyCode.Joystick1Button0;
                    break;

                case GamePadKey.B:
                case GamePadKey.FUNC_RIGHT:
                    keyCode = KeyCode.Joystick1Button1;
                    break;

                case GamePadKey.Y:
                case GamePadKey.FUNC_UP:
                    keyCode = KeyCode.Joystick1Button3;
                    break;

                case GamePadKey.X:
                case GamePadKey.FUNC_LEFT:
                    keyCode = KeyCode.Joystick1Button2;
                    break;

                case GamePadKey.LB:
                    keyCode = KeyCode.Joystick1Button4;
                    break;

                case GamePadKey.RB:
                    keyCode = KeyCode.Joystick1Button5;
                    break;

                case GamePadKey.L_STICK:
                    keyCode = KeyCode.Joystick1Button8;
                    break;

                case GamePadKey.R_STICK:
                    keyCode = KeyCode.Joystick1Button9;
                    break;
            }

            if (gamePadNum > 1)
                keyCode += 20;

            return keyCode;
        }

        // Ref : https://ritchielozada.com/2016/11/21/playstation-4-dual-shock-controller-input-mapping-with-unity-on-windows-10/
        public static KeyCode GetDS4KeyCode(int gamePadNum, GamePadKey key)
        {
            KeyCode keyCode = KeyCode.None;

            switch (key)
            {
                case GamePadKey.BACK:
                    keyCode = KeyCode.Joystick1Button8;
                    break;

                case GamePadKey.START:
                    keyCode = KeyCode.Joystick1Button9;
                    break;

                case GamePadKey.A:
                case GamePadKey.FUNC_DOWN:
                    keyCode = KeyCode.Joystick1Button1;
                    break;

                case GamePadKey.B:
                case GamePadKey.FUNC_RIGHT:
                    keyCode = KeyCode.Joystick1Button2;
                    break;

                case GamePadKey.Y:
                case GamePadKey.FUNC_UP:
                    keyCode = KeyCode.Joystick1Button3;
                    break;

                case GamePadKey.X:
                case GamePadKey.FUNC_LEFT:
                    keyCode = KeyCode.Joystick1Button0;
                    break;

                case GamePadKey.LB:
                    keyCode = KeyCode.Joystick1Button4;
                    break;

                case GamePadKey.RB:
                    keyCode = KeyCode.Joystick1Button5;
                    break;

                case GamePadKey.L_STICK:
                    keyCode = KeyCode.Joystick1Button10;
                    break;

                case GamePadKey.R_STICK:
                    keyCode = KeyCode.Joystick1Button11;
                    break;
            }

            if (gamePadNum > 1)
                keyCode += 20;

            return keyCode;
        }
    }
}