using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    public abstract class ControllerHelperBase
    {
        protected List<string> axisKeys;
        protected Dictionary<string, AxisValues> axisValueDict;

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

            OnInitialize();
        }

        #region ABSTRACT METHODS
        /// <summary>
        /// axisKeys에 Axis Input들의 이름들 추가.
        /// axisValueDict 초기화.
        /// </summary>
        protected abstract void OnInitialize();

        public abstract Vector2 GetLStickAxis();
        public abstract Vector2 GetRStickAxis();
        
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
        #endregion

        public void UpdateFrames(float deltaTime)
        {
            foreach (var key in axisKeys)
                axisValueDict[key].UpdateValues();
        }
    }
}