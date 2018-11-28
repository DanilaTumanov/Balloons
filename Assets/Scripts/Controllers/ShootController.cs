using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    public class ShootController : MonoBehaviour
    {

        const string DEFAULT_SHOOT_INTERACTION_LAYER = "Balloons";


        public LayerMask shootInteractionLayers;


        private void OnValidate()
        {
            if(shootInteractionLayers == 0)
                shootInteractionLayers = 1 << LayerMask.NameToLayer(DEFAULT_SHOOT_INTERACTION_LAYER);
        }


        private void Update()
        {
            ProcessShoot();
        }



        private void ProcessShoot()
        {
            if (InputManager.Controller.IsTapDown)
            {
                var point = Camera.main.ScreenToWorldPoint(InputManager.Controller.TouchPosition);
                var collider = Physics2D.OverlapPoint(point, shootInteractionLayers);

                if (collider != null)
                {
                    IShootable shootable = collider.transform.GetComponent<IShootable>();

                    if (shootable != null)
                    {
                        shootable.OnShooted();

                        IScoresHolder scoresHolder = collider.transform.GetComponent<IScoresHolder>();

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
