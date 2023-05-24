using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PB.Game{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] public float force;
        [SerializeField] public Vector3 direction;
        [SerializeField] public GameObject prefab;


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                Shoot();
        }

        private void Shoot(){
            var ball = Instantiate(prefab,transform.position,Quaternion.identity,transform).GetComponent<BallController>();
            ball.Shoot(force,direction);
        }
    }
}