using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

namespace PB.Game{
    public class AIController : Agent
    {
        [SerializeField] BallSpawner spawner;
        [SerializeField] public GameObject prefab;
        [SerializeField] public Transform target;

        public int max = 10;
        public float maxRange;
        public float minRange;
        private int count =0;
        public bool ready = true;

        public float amplifer = 20;
        public override void Initialize(){

        }
        public override void OnEpisodeBegin(){
            count =0;
            transform.localPosition = new Vector3(0,1,Random.Range(minRange,maxRange));
            ready = true;
        }
        public override void CollectObservations(VectorSensor sensor){
            Vector3 velocity = (target.position-transform.position).normalized;
            float distance = velocity.magnitude/maxRange;


            //sensor.AddObservation(velocity.x);
            //sensor.AddObservation(velocity.y);
            //sensor.AddObservation(velocity.z);
            sensor.AddObservation(distance);
        }
        public override void OnActionReceived(ActionBuffers actions){
            if(!ready)
                return;
            Debug.Log($"ACTION {Time.time}");

            var ball = Instantiate(prefab,spawner.transform.position,Quaternion.identity,transform).GetComponent<BallController>();
            ball.controller = this;
            Vector3 velocity = (target.position - transform.position).normalized;
            velocity.y*=2;
            var continuousActions =  actions.ContinuousActions;

            float force =continuousActions[0] * amplifer;

            ball.Shoot(force,velocity );
            count ++;

            if(count >= max ){
                ready = false;
                StartCoroutine(Terminate());
            }

        }

        private IEnumerator Terminate(){
            yield return new WaitForSeconds(3);
            EndEpisode();
        }
        public override void Heuristic(in ActionBuffers actionsOut){

        } 



        void Update(){
            if(Input.GetKeyDown(KeyCode.C))
                EndEpisode();
        }
    }
}
