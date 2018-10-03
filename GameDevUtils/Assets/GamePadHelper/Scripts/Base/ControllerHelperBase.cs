using System.Collections.Generic;
using UnityEngine;

namespace GamePadHelper
{
    public abstract class ControllerHelperBase
    {
        // TODO : Implement other Player's input
        //protected int playerIndex { get; private set; }

        protected List<string> axisKeys;
        protected Dictionary<string, AxisValues> axisValueDict;

        public ControllerHelperBase()
        {
            Initialize();
        }

        #region ABSTRACT METHODS
        /// <summary>
        /// axisKeys에 Axis Input들의 이름들 추가.
        /// axisValueDict 초기화.
        /// </summary>
        protected abstract void Initialize();

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