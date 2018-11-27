using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    // Ref : https://ritchielozada.com/2016/11/21/playstation-4-dual-shock-controller-input-mapping-with-unity-on-windows-10/
    public class DS4Input : GamePadBase
    {
        protected override KeyCode GetKeyCode(GamePadKey key)
        {
            switch (key)
            {
                case GamePadKey.BACK:
                    return KeyCode.Joystick1Button8;

                case GamePadKey.START:
                    return KeyCode.Joystick1Button9;

                case GamePadKey.A:
                case GamePadKey.FUNC_DOWN:
                    return KeyCode.Joystick1Button1;

                case GamePadKey.B:
                case GamePadKey.FUNC_RIGHT:
                    return KeyCode.Joystick1Button2;

                case GamePadKey.Y:
                case GamePadKey.FUNC_UP:
                    return KeyCode.Joystick1Button3;

                case GamePadKey.X:
                case GamePadKey.FUNC_LEFT:
                    return KeyCode.Joystick1Button0;

                case GamePadKey.LB:
                    return KeyCode.Joystick1Button4;

                case GamePadKey.RB:
                    return KeyCode.Joystick1Button5;

                case GamePadKey.L_STICK:
                    return KeyCode.Joystick1Button10;

                case GamePadKey.R_STICK:
                    return KeyCode.Joystick1Button11;
            }

            // PS Button : Joystick Button12
            // Touchpad Click : Joystick Button13

            return KeyCode.None;
        }
    }
}