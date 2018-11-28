using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    public class UIController : MonoBehaviour
    {

        [SerializeField]
        private HUDController _hud;

        [SerializeField]
        private ScreenController _screens;





        public HUDController HUD
        {
            get
            {
                return _hud;
            }
        }


        public ScreenController Screens
        {
            get
            {
                return _screens;
            }
        }





        private void Awake()
        {
            if (_hud == null)
                _hud = FindObjectOfType<HUDController>();
        }






        public void Exit()
        {
            Main.SceneController.ExitGame();
        }


        public void Restart()
        {
            Main.SceneController.RestartGame();
        }

    }

}


