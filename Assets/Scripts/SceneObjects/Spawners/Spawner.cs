using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    public abstract class Spawner : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Родительский объект для спауна")]
        protected Transform _spawnFolder;

        [Tooltip("Минимальное (x) и максимальное (y) время до следующего спауна (в секундах)")]
        public Vector2 spawnTime = new Vector2(2, 3);



        private bool _enabled = false;

        private Coroutine _spawnCoroutine;
        


        



        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;

            if (enabled)
            {
                _spawnCoroutine = StartCoroutine(ProcessSpawn());
            }
            else if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

               


        private IEnumerator ProcessSpawn()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(GetNextSpawnTime());
            }
        }



        private float GetNextSpawnTime()
        {
            return Random.Range(spawnTime.x, spawnTime.y);
        }




        public abstract void ClearInstances();

        protected abstract void Spawn();


    }

}
