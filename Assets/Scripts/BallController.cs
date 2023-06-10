using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PB.Game
{
    public class BallController : MonoBehaviour
    {   
        public AIController controller;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private MeshRenderer renderer;
        
        public Material win;
        private bool scored = false;

        public void Shoot(float force, Vector3 direction)
        {
            rb.AddForce(direction * force, ForceMode.Impulse);
        }

        public void OnCollisionEnter(Collision other){
            
            if(!other.collider.CompareTag("Ground"))
                return;
            if(controller)
                controller.AddReward(scored?1:-1);
            Destroy(gameObject,2);
        }

        private void OnTriggerEnter(Collider other){
            if(!other.CompareTag("Net"))
                return;
            scored = true;
            renderer.material = win;
            
        }
        
    }

}
