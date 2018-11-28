using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Контроллер интерфейса
    /// </summary>
    [DisallowMultipleComponent]
    public class UIController : MonoBehaviour
    {
        
        [SerializeField]
        [Tooltip("Контроллер HUD")]
        private HUDController _hud;

        [SerializeField]
        [Tooltip("Контроллер экранов")]
        private ScreenController _screens;




        /// <summary>
        /// Контроллер HUD
        /// </summary>
        public HUDController HUD
        {
            get
            {
                return _hud;
            }
        }

        /// <summary>
        /// Контроллер экранов
        /// </summary>
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





        /// <summary>
        /// Обработчик нажатия кнопки выхода из игры
        /// </summary>
        public void Exit()
        {
            Main.SceneController.ExitGame();
        }


        /// <summary>
        /// Обработчик нажатия кнопки перезапуска игры
        /// </summary>
        public void Restart()
        {
            Main.SceneController.RestartGame();
        }

    }

}


