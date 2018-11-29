using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIMenu
{
    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public class GUIMenu : MonoBehaviour
    {
        public bool IsOpened { get; private set; }

        public int col = 3;
        public int row = 3;

        public void MoveCursor(DIRECTION direction)
        {

        }

        public void SelectCurrentMenu()
        {

        }

        private void OnGUI()
        {
            var tmp = new List<BaseGUIMenuItem>();

            for (int i = 0; i < (col * row); i++)
                tmp.Add(new ButtonMenuItem((i + 1) + " Btn"));

            for (int i = 0; i < col; i++)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < row; j++)
                {
                    int index = j * col + i;

                    if (tmp.Count > index)
                        tmp[index].DrawItem();
                }
                GUILayout.EndHorizontal();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                MoveCursor(DIRECTION.UP);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                MoveCursor(DIRECTION.DOWN);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                MoveCursor(DIRECTION.LEFT);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                MoveCursor(DIRECTION.RIGHT);

            if (Input.GetKeyDown(KeyCode.Return))
                SelectCurrentMenu();
        }
    }
}

