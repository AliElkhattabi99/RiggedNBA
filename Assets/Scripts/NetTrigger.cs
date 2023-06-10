using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    [SerializeField] private bool player;
    [SerializeField] private bool agent;
    private GameManager gameManger;

    private void Start()
    {
        gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (player)
            {
                gameManger.PlayerAddScore();
            }
            else if (agent)
            {
                gameManger.AgentAddScore();
            }
        }
    }
}
