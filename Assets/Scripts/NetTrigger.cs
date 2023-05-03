using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball entered net!");
            // Add reward to agent here
  
            // End episode if needed
        }
    }

}
