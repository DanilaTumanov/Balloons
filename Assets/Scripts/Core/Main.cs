using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Основна точка входа для доступа к базовым узлам
    /// </summary>
    [DisallowMultipleComponent]
    public class Main : MonoBehaviour
    {

        /// <summary>
        /// Контроллер текущей сцены
        /// </summary>
        public static SceneController SceneController { get; private set; }

        /// <summary>
        /// Контроллер UI
        /// </summary>
        public static UIController UIController { get; private set; }


        private void Awake()
        {
            SceneController = FindObjectOfType<SceneController>();
            UIController = FindObjectOfType<UIController>();
        }

    }

}
