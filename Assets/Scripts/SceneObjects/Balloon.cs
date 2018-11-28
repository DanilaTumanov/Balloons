using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(Collider2D))]
    public class Balloon : MonoBehaviour, IShootable, IScoresHolder
    {
        [Tooltip("Скорость шарика")]
        public float speed = 1;

        [Tooltip("Высота, достигнув которую шарик уничтожится")]
        public float verticalLimit = 100;



        private SpriteRenderer _spriteRenderer;
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


        private void Move()
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }


        private void CheckLimits()
        {
            if(transform.position.y > verticalLimit)
            {
                Destroy(gameObject);
            }
        }






        public float GetHorizontalExtent()
        {
            return _spriteRenderer.sprite.bounds.extents.x * transform.localScale.x;
        }

        public float GetVertiacalExtent()
        {
            return _spriteRenderer.sprite.bounds.extents.y * transform.localScale.y;
        }

        

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }



        public void OnShooted()
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
            _bang.Play();
            Destroy(gameObject, _bang.main.duration);
        }
    }

}
