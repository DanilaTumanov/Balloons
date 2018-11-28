using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    public abstract class SpawnerType<T> : Spawner
        where T : MonoBehaviour
    {

        protected override void Spawn()
        {
            var prefab = GetPrefab();

            InitInstance(Instantiate(prefab, _spawnFolder));
        }


        public override void ClearInstances()
        {
            var balloons = _spawnFolder.GetComponentsInChildren<T>();

            foreach (var balloon in balloons)
            {
                DestroyImmediate(balloon.gameObject);
            }
        }


        protected abstract T GetPrefab();

        protected abstract void InitInstance(T instance);

    }

}


