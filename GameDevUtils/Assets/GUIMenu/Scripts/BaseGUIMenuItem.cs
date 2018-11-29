using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIMenu
{
    public abstract class BaseGUIMenuItem
    {
        public void DrawItem()
        {
            DrawItemImplement();
        }

        protected abstract void DrawItemImplement();

        public void ProcessMenu()
        {

        }
    }

    public class ButtonMenuItem : BaseGUIMenuItem
    {
        private string buttonName;

        public ButtonMenuItem(string buttonName)
        {
            this.buttonName = buttonName;
        }

        protected override void DrawItemImplement()
        {
            GUILayout.Button(buttonName);
        }
    }

    //public class ToggleMenuItem : BaseGUIMenuItem
    //{

    //}
}
