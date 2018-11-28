using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Абстрактный класс спаунера
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class Spawner : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Родительский объект для спауна")]
        protected Transform _spawnFolder;

        [Tooltip("Минимальное (x) и максимальное (y) время до следующего спауна (в секундах)")]
        public Vector2 spawnTime = new Vector2(2, 3);


        // Признак активности
        private bool _enabled = false;

        // Корутина, в которой происходит спаун
        private Coroutine _spawnCoroutine;
        


        


        /// <summary>
        /// Активировать или деактивировать спаунер
        /// </summary>
        /// <param name="enabled">true - активировать, false - деактивировать</param>
        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;

            if (enabled)
            {
                // При активации запускаем корутину
                _spawnCoroutine = StartCoroutine(ProcessSpawn());
            }
            else if (_spawnCoroutine != null)
            {
                // При деактивации останавливаем и удаляем корутину
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

               

        /// <summary>
        /// Обработка спауна. Метод корутины
        /// </summary>
        /// <returns></returns>
        private IEnumerator ProcessSpawn()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(GetNextSpawnTime());
            }
        }


        /// <summary>
        /// Сгенерировать случайное время следующего спауна
        /// </summary>
        /// <returns></returns>
        private float GetNextSpawnTime()
        {
            return Random.Range(spawnTime.x, spawnTime.y);
        }



        /// <summary>
        /// Удалить экземпляры сгенерированных объектов
        /// </summary>
        public abstract void ClearInstances();

        /// <summary>
        /// Создать экземпляр объекта
        /// </summary>
        protected abstract void Spawn();


    }

}
