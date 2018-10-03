using UnityEngine;

namespace GamePadHelper
{
    public class AxisValues
    {
        private string axisName;
        private float prevValue;
        private float curValue;
        private float threshold = 1f;

        public AxisValues(string axisName)
        {
            this.axisName = axisName;
            this.prevValue = 0f;
            this.curValue = 0f;
        }

        public void SetThreshold(float threshold)
        {
            this.threshold = threshold;
            if (this.threshold < 0f)
                this.threshold *= -1;

            if (this.threshold == 0f)
                throw new System.ArgumentException("Axis Threshold CAN NOT BE ZERO");
        }

        public void UpdateValues()
        {
            if (string.IsNullOrEmpty(axisName))
                return;

            prevValue = curValue;
            curValue = Input.GetAxis(axisName);
        }

        public bool Press(bool toPositive)
        {
            return !IsActiveValue(prevValue, toPositive) 
                && IsActiveValue(curValue, toPositive);
        }

        public bool Released(bool fromPositive)
        {
            return IsActiveValue(prevValue, fromPositive)
                && !IsActiveValue(curValue, fromPositive);
        }

        public bool IsPressing(bool isPositive)
        {
            return IsActiveValue(prevValue, isPositive)
                & IsActiveValue(curValue, isPositive);
        }

        private bool IsActiveValue(float value, bool positive)
        {
            if (positive)
                return (value >= threshold);
            else
                return (value <= -threshold);
        }
    }
}