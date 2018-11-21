using UnityEngine;
using System;

namespace GamePadHelper.Demo
{
    public class GamePadHelperDemoScene : MonoBehaviour
    {
        private string axisValueFmt = @"L Stick : {0}
R Stick : {1}
L Trigger : {2}
R Trigger : {3}";

        private XInputHelper xinput;

        void Start ()
        {
            xinput = new XInputHelper();
            xinput.Initialize("XInputAxisPlayer1");
        }
	
	    void Update () {
            if (xinput == null)
                return;

            xinput.UpdateFrames(Time.deltaTime);

            foreach (GamePadKey key in (GamePadKey[])Enum.GetValues(typeof(GamePadKey)))
            {
                var phase = xinput.GetButtonPhase(key);

                if (phase != KeyPhase.NONE && phase != KeyPhase.KEEP)
                    Debug.LogFormat("Input Key:{0} Phase:{1}", key, phase);
            }
        }

        private void OnGUI()
        {
            GUILayout.Box(
                string.Format(
                    axisValueFmt,
                    xinput.GetLStickAxis(),
                    xinput.GetRStickAxis(),
                    Input.GetAxis(xinput.axisNames.LTrigger),
                    Input.GetAxis(xinput.axisNames.RTrigger)));

            foreach (GamePadKey key in (GamePadKey[])Enum.GetValues(typeof(GamePadKey)))
            {
                var phase = xinput.GetButtonPhase(key);

                if (phase != KeyPhase.NONE)
                    GUILayout.Box(key.ToString() + " is keep press");
            }
        }
    }
}

