using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Абстрактный дженерик-класс спаунера MonoBehaviour объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SpawnerType<T> : Spawner
        where T : MonoBehaviour
    {

        /// <summary>
        /// Спаун объекта
        /// </summary>
        protected override void Spawn()
        {
            var prefab = GetPrefab();

            InitInstance(Instantiate(prefab, _spawnFolder));
        }


        /// <summary>
        /// Удаление экземпляров сгенерированных объектов
        /// </summary>
        public override void ClearInstances()
        {
            var balloons = _spawnFolder.GetComponentsInChildren<T>();

            foreach (var balloon in balloons)
            {
                DestroyImmediate(balloon.gameObject);
            }
        }


        /// <summary>
        /// Получить префаб объекта
        /// </summary>
        /// <returns></returns>
        protected abstract T GetPrefab();

        /// <summary>
        /// Инициализировать экземпляр объекта
        /// </summary>
        /// <param name="instance"></param>
        protected abstract void InitInstance(T instance);

    }

}


