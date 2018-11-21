using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    public abstract class ControllerHelperBase
    {
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
                axisNames.LeftAnalogStickX = "Analog Left X";
                axisNames.LeftAnalogStickY = "Analog Left Y";
                axisNames.RightAnalogStickX = "Analog Right X";
                axisNames.RightAnalogStickY = "Analog Right Y";
                axisNames.DPadX = "D Pad X";
                axisNames.DPadY = "D Pad Y";
                axisNames.LTrigger = "Left Trigger";
                axisNames.RTrigger = "Right Trigger";
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

            axisValueDict[axisNames.LTrigger].SetThreshold(1);
            axisValueDict[axisNames.RTrigger].SetThreshold(1);
            axisValueDict[axisNames.LeftAnalogStickX].SetThreshold(1);
            axisValueDict[axisNames.LeftAnalogStickY].SetThreshold(1);
            axisValueDict[axisNames.RightAnalogStickX].SetThreshold(1);
            axisValueDict[axisNames.RightAnalogStickY].SetThreshold(1);
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
        protected abstract bool TryProcessAxisValue(GamePadKey key, KeyPhase phase);
        #endregion

        #region PUBLIC
        public virtual bool GetKeyDown(GamePadKey key)
        {
            if (TryProcessAxisValue(key, KeyPhase.DOWN))
                return true;

            var keyCode = GetKeyCode(key);
            return Input.GetKeyDown(keyCode);
        }

        public virtual bool GetKey(GamePadKey key)
        {
            if (TryProcessAxisValue(key, KeyPhase.KEEP))
                return true;

            var keyCode = GetKeyCode(key);
            return Input.GetKey(keyCode);
        }

        public virtual bool GetKeyUp(GamePadKey key)
        {
            if (TryProcessAxisValue(key, KeyPhase.UP))
                return true;

            var keyCode = GetKeyCode(key);
            return Input.GetKeyUp(keyCode);
        }

        public KeyPhase GetButtonPhase(GamePadKey key)
        {
            if (GetKeyDown(key))
                return KeyPhase.DOWN;

            if (GetKeyUp(key))
                return KeyPhase.UP;

            if (GetKey(key))
                return KeyPhase.KEEP;

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
        #endregion
    }
}