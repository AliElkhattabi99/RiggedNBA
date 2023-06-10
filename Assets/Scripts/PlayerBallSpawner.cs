using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float throwStrength;

    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            Rigidbody ballToSpawn = Instantiate(ball, playerCamera.position, playerCamera.rotation).GetComponent<Rigidbody>();

            ballToSpawn.velocity = playerCamera.TransformDirection(Vector3.forward * throwStrength);
        }
    }
}
