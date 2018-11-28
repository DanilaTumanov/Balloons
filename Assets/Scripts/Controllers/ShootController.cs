using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Контроллер выстрелов
    /// </summary>
    [DisallowMultipleComponent]
    public class ShootController : MonoBehaviour
    {

        /// <summary>
        /// Слой по-умолчанию для определения попаданий
        /// </summary>
        const string DEFAULT_SHOOT_INTERACTION_LAYER = "Balloons";


        /// <summary>
        /// Слои в которых производится определение попаданий
        /// </summary>
        public LayerMask shootInteractionLayers;



        private void OnValidate()
        {
            // Если не выбран ни один слой - устанавливаем слой по-умолчанию
            if(shootInteractionLayers == 0)
                shootInteractionLayers = 1 << LayerMask.NameToLayer(DEFAULT_SHOOT_INTERACTION_LAYER);
        }


        private void Update()
        {
            ProcessShoot();
        }


        /// <summary>
        /// Обработка выстрела
        /// </summary>
        private void ProcessShoot()
        {
            // Если был "тап"
            if (InputManager.Controller.IsTapDown)
            {
                var point = Camera.main.ScreenToWorldPoint(InputManager.Controller.TouchPosition);
                var collider = Physics2D.OverlapPoint(point, shootInteractionLayers);

                // Если попали в коллайдер в одном из слоев
                if (collider != null)
                {
                    IDamagable damagable = collider.transform.GetComponent<IDamagable>();

                    // Проверяем, может ли объект плучать урон
                    if (damagable != null)
                    {
                        damagable.OnDamaged();

                        IScoresHolder scoresHolder = collider.transform.GetComponent<IScoresHolder>();

                        // Проверяем, дает ли объект очки за попадание
                        if(scoresHolder != null)
                        {
                            Main.SceneController.AddScores(scoresHolder.Scores);
                        }
                    }
                }
            }
        }

    }

}
