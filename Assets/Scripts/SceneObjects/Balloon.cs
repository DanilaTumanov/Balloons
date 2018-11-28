using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Шарик
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(Collider2D))]
    public class Balloon : MonoBehaviour, IDamagable, IScoresHolder
    {
        [Tooltip("Скорость шарика")]
        public float speed = 1;

        [Tooltip("Высота, достигнув которую шарик уничтожится")]
        public float verticalLimit = 100;


        
        private SpriteRenderer _spriteRenderer;

        // Система частиц с эффектом взрыва
        private ParticleSystem _bang;
        
        private Collider2D _collider;



        /// <summary>
        /// Количество очков за сбитый шарик
        /// </summary>
        public int Scores { get; set; }



        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _bang = GetComponent<ParticleSystem>();
            _collider = GetComponent<Collider2D>();
        }


        private void FixedUpdate()
        {
            Move();
            CheckLimits();
        }


        /// <summary>
        /// Обработка движения шарика
        /// </summary>
        private void Move()
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }


        /// <summary>
        /// Проверка превышения шариком вертикальной границы
        /// </summary>
        private void CheckLimits()
        {
            if(transform.position.y > verticalLimit)
            {
                Destroy(gameObject);
            }
        }





        /// <summary>
        /// Получить размер горизонтального пространства (половина ширины)
        /// </summary>
        /// <returns></returns>
        public float GetHorizontalExtent()
        {
            return _spriteRenderer.sprite.bounds.extents.x * transform.localScale.x;
        }

        /// <summary>
        /// Получить размер вертикального пространства (половина высоты)
        /// </summary>
        /// <returns></returns>
        public float GetVertiacalExtent()
        {
            return _spriteRenderer.sprite.bounds.extents.y * transform.localScale.y;
        }

        
        /// <summary>
        /// Установить спрайт шарика
        /// </summary>
        /// <param name="sprite"></param>
        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }


        /// <summary>
        /// Обработчик получения урона
        /// </summary>
        public void OnDamaged()
        {
            // Отключаем отображение шарика
            _spriteRenderer.enabled = false;

            // Отключаем коллайдер, чтобы не регистрировались повторные нажатия
            _collider.enabled = false;

            // Включаем анимацию и удаляем шарик после нее
            _bang.Play();
            Destroy(gameObject, _bang.main.duration);
        }
    }

}
