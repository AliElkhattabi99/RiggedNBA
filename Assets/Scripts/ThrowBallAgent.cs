using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ThrowBallAgent : Agent
{
    public Transform ballSpawn;
    public GameObject ballPrefab;
    public Transform target;
    public Transform net;

    private Rigidbody ballRb;
    private bool hasBall = true;

    public override void OnEpisodeBegin()
    {
        // Reset agent position and rotation
        this.transform.localPosition = new Vector3(0, 1, 0);
        this.transform.localRotation = Quaternion.identity;

        // Reset ball position and velocity
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.transform.position = ballSpawn.position;

        // Set hasBall to true
        hasBall = true;

        // Move target to a new random position
        target.position = new Vector3(Random.Range(-5.0f, 5.0f), 1, Random.Range(-5.0f, 5.0f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add agent and target position to observations
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Get continuous actions
        float moveX = actionBuffers.ContinuousActions[0];
        float moveZ = actionBuffers.ContinuousActions[1];
        float throwX = actionBuffers.ContinuousActions[2];
        float throwY = actionBuffers.ContinuousActions[3];

        // Move agent
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        this.transform.position += movement * Time.deltaTime * 3;

        // Rotate agent towards target
        Vector3 targetDir = target.position - this.transform.position;
        float step = 5.0f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        this.transform.rotation = Quaternion.LookRotation(newDir);

        // Throw ball if agent has ball
        if (hasBall && throwY > 0)
        {
            Vector3 throwForce = new Vector3(throwX * 500, throwY * 500, 1000);
            ballRb.AddForce(throwForce);
            hasBall = false;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Get continuous actions from user input
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
        continuousActionsOut[2] = Input.GetAxis("ThrowX");
        continuousActionsOut[3] = Input.GetAxis("ThrowY");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if ball entered net
        if (collision.gameObject.CompareTag("Net"))
        {
            Debug.Log("Ball entered net!");
            SetReward(1f);
            EndEpisode();
        }
    }

    private void Start()
    {
        // Spawn ball
        GameObject ball = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);
        ballRb = ball.GetComponent<Rigidbody>();
    }
}
