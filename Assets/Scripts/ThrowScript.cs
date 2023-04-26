using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public GameObject ball;
    public Transform hoop;
    public float shootingForce = 10f;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 direction = (hoop.position - ball.transform.position).normalized;
        ball.GetComponent<Rigidbody2D>().AddForce(direction * shootingForce, ForceMode2D.Impulse);
    }
}


