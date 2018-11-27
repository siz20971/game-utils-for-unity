using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    public abstract class GamePadBase
    {
        private int gamePadIndex = 0;

        protected List<string> axisKeys;
        protected Dictionary<string, AxisValues> axisValueDict;

        private Vector2 leftStickAxis   = Vector2.zero;
        private Vector2 rightStickAxis  = Vector2.zero;

        private Vector2 leftStickPrevAxis   = Vector2.zero;
        private Vector2 rightStickPrevAxis  = Vector2.zero;

        public AxisNameAsset axisNames { get; private set; }

        public void Initialize(string axisAssetName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("AxisNameObject/");
            sb.Append(axisAssetName);

            axisNames = Resources.Load<AxisNameAsset>(sb.ToString());
            if (axisNames == null)
            {
                axisNames = new AxisNameAsset();
                axisNames.LeftAnalogStickX  = DefaultAxisNames.LeftAnalogStickX;
                axisNames.LeftAnalogStickY  = DefaultAxisNames.LeftAnalogStickY;
                axisNames.RightAnalogStickX = DefaultAxisNames.RightAnalogStickX;
                axisNames.RightAnalogStickY = DefaultAxisNames.RightAnalogStickY;
                axisNames.DPadX             = DefaultAxisNames.DPadX;
                axisNames.DPadY             = DefaultAxisNames.DPadY;
                axisNames.LTrigger          = DefaultAxisNames.LTrigger;
                axisNames.RTrigger          = DefaultAxisNames.RTrigger;
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

        public Vector2 GetLStickAxis()
        {
            return leftStickAxis;
        }

        public Vector2 GetRStickAxis()
        {
            return rightStickAxis;
        }

        public Vector2 GetLStickPrevAxis()
        {
            return leftStickPrevAxis;
        }

        public Vector2 GetRStickPrevAxis()
        {
            return rightStickPrevAxis;
        }

        #region ABSTRACT METHODS
        protected abstract KeyCode GetKeyCode(GamePadKey key);
        #endregion

        #region PUBLIC
        public virtual bool GetKeyDown(GamePadKey key)
        {
            if (TryProcessAxisValue(key, KeyPhase.PRESS))
                return true;

            var keyCode = GetKeyCode(key);
            return Input.GetKeyDown(keyCode);
        }

        public virtual bool GetKey(GamePadKey key)
        {
            if (TryProcessAxisValue(key, KeyPhase.PRESSED))
                return true;

            var keyCode = GetKeyCode(key);
            return Input.GetKey(keyCode);
        }

        public virtual bool GetKeyUp(GamePadKey key)
        {
            if (TryProcessAxisValue(key, KeyPhase.RELEASE))
                return true;

            var keyCode = GetKeyCode(key);
            return Input.GetKeyUp(keyCode);
        }

        public KeyPhase GetButtonPhase(GamePadKey key)
        {
            if (GetKeyDown(key))
                return KeyPhase.PRESS;

            if (GetKeyUp(key))
                return KeyPhase.RELEASE;

            if (GetKey(key))
                return KeyPhase.PRESSED;

            return KeyPhase.NONE;
        }

        public void UpdateFrames(float deltaTime)
        {
            foreach (var key in axisKeys)
                axisValueDict[key].UpdateValues();

            // Axis Value Update
            leftStickPrevAxis.x = axisValueDict[axisNames.LeftAnalogStickX].prevValue;
            leftStickPrevAxis.y = axisValueDict[axisNames.LeftAnalogStickY].prevValue;
            rightStickPrevAxis.x = axisValueDict[axisNames.RightAnalogStickX].prevValue;
            rightStickPrevAxis.y = axisValueDict[axisNames.RightAnalogStickY].prevValue;

            leftStickAxis.x = axisValueDict[axisNames.LeftAnalogStickX].curValue;
            leftStickAxis.y = axisValueDict[axisNames.LeftAnalogStickY].curValue;
            rightStickAxis.x = axisValueDict[axisNames.RightAnalogStickX].curValue;
            rightStickAxis.y = axisValueDict[axisNames.RightAnalogStickY].curValue;
        }

        public bool IsPressAnyInput()
        {
            if (KeyPhase.NONE != GetButtonPhase(GamePadKey.D_UP) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.D_DOWN) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.D_LEFT) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.D_RIGHT) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.L_STICK) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.R_STICK) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.LB) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.RB) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.BACK) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.START) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.TOUCHPAD) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.FUNC_UP) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.FUNC_DOWN) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.FUNC_LEFT) ||
                KeyPhase.NONE != GetButtonPhase(GamePadKey.FUNC_RIGHT))
            {
                return true;
            }

            // joystick axis
            if (Input.GetAxis(axisNames.LeftAnalogStickX) != 0.0f ||
               Input.GetAxis(axisNames.LeftAnalogStickY) != 0.0f ||
               Input.GetAxis(axisNames.RightAnalogStickX) != 0.0f ||
               Input.GetAxis(axisNames.RightAnalogStickY) != 0.0f ||
               Input.GetAxis(axisNames.DPadX) != 0.0f ||
               Input.GetAxis(axisNames.DPadY) != 0.0f ||
               Input.GetAxis(axisNames.LTrigger) != 0.0f ||
               Input.GetAxis(axisNames.RTrigger) != 0.0f)
            {
                return true;
            }

            return false;
        }
        #endregion


        protected bool TryProcessAxisValue(GamePadKey key, KeyPhase phase)
        {
            string axisName = string.Empty;
            bool isPositiveValue = true;
            
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
            
            if (string.IsNullOrEmpty(axisName))
                return false;
            else if (!axisValueDict.ContainsKey(axisName))
                return false;

            if (phase == KeyPhase.RELEASE)
                return axisValueDict[axisName].Released(isPositiveValue);
            else if (phase == KeyPhase.PRESS)
                return axisValueDict[axisName].Press(isPositiveValue);
            else if (phase == KeyPhase.PRESSED)
                return axisValueDict[axisName].IsPressing(isPositiveValue);

            return false;
        }
    }
}