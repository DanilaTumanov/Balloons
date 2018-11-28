using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balloons
{

    /// <summary>
    /// Базовый класс UI экрана
    /// </summary>
    public class UIScreen : MonoBehaviour
    {

        /// <summary>
        /// Показать экран
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Скрыть экран
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

    }

}