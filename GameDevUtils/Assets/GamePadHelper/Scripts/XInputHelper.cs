﻿using UnityEngine;
using System.Collections.Generic;

namespace GamePadHelper
{
    // REF : http://wiki.unity3d.com/index.php/Xbox360Controller
    public class XInputHelper : GamePadBase
    {
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