using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    public enum InputDevice
    {
        KEYBOARD_MOUSE,
        GAMEPAD,
        NONE
    }

    public enum GamePadType
    {
        NONE,
        XBOX,
        DUALSHOCK,
        OTHERS
    }

    public delegate void InputDeviceChange(InputDevice prevDevice, InputDevice curDevice);

    public class InputDetector
    {
        private InputDeviceChange onInputChanged;
        private InputDevice currentDevice = InputDevice.KEYBOARD_MOUSE;
        private GamePadType currentGamePadType = GamePadType.NONE;

        public void RegistOnInputDeviceChange(InputDeviceChange ev)
        {
            if (ev != null)
                onInputChanged += ev;
        }

        public void UnregistInputDeviceChange(InputDeviceChange ev)
        {
            if (ev != null)
                onInputChanged -= ev;
        }

        /// <summary>
        /// This method MUST BE CALLING in MonoBehaviour.OnGUI
        /// </summary>
        public void CheckInputDevice()
        {
            if (currentDevice != InputDevice.KEYBOARD_MOUSE)
            {
                if (IsKeyboardMouseInput())
                {
                    ChangeCurrentDevice(InputDevice.KEYBOARD_MOUSE);
                }
            }
            else
            {
                if (IsControllerInput())
                {
                    ChangeCurrentDevice(InputDevice.GAMEPAD);
                }
            }
        }

        private void ChangeCurrentDevice(InputDevice newDeviceType)
        {
            if (onInputChanged != null)
                onInputChanged(currentDevice, newDeviceType);
            currentDevice = newDeviceType;
        }

        private bool IsKeyboardMouseInput()
        {
            // mouse & keyboard buttons
            if (Event.current.isKey ||
                Event.current.isMouse)
            {
                return true;
            }
            // mouse movement
            if (Input.GetAxis("Mouse X") != 0.0f ||
                Input.GetAxis("Mouse Y") != 0.0f)
            {
                return true;
            }

            return false;
        }

        private bool IsControllerInput()
        {
            // joystick buttons
            if (Input.GetKey(KeyCode.Joystick1Button0) ||
               Input.GetKey(KeyCode.Joystick1Button1) ||
               Input.GetKey(KeyCode.Joystick1Button2) ||
               Input.GetKey(KeyCode.Joystick1Button3) ||
               Input.GetKey(KeyCode.Joystick1Button4) ||
               Input.GetKey(KeyCode.Joystick1Button5) ||
               Input.GetKey(KeyCode.Joystick1Button6) ||
               Input.GetKey(KeyCode.Joystick1Button7) ||
               Input.GetKey(KeyCode.Joystick1Button8) ||
               Input.GetKey(KeyCode.Joystick1Button9) ||
               Input.GetKey(KeyCode.Joystick1Button10) ||
               Input.GetKey(KeyCode.Joystick1Button11) ||
               Input.GetKey(KeyCode.Joystick1Button12) ||
               Input.GetKey(KeyCode.Joystick1Button13) ||
               Input.GetKey(KeyCode.Joystick1Button14) ||
               Input.GetKey(KeyCode.Joystick1Button15) ||
               Input.GetKey(KeyCode.Joystick1Button16) ||
               Input.GetKey(KeyCode.Joystick1Button17) ||
               Input.GetKey(KeyCode.Joystick1Button18) ||
               Input.GetKey(KeyCode.Joystick1Button19))
            {
                return true;
            }

            // TODO : Add Axis Detect

            return false;
        }
    }
}