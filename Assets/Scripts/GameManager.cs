using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore;
    [SerializeField] private int agentScore;
    [SerializeField] private int targetScore;
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text agentScoreText;
    [SerializeField] private TMP_Text WinScreen;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerBallSpawner playerBallSpawner;
    public void PlayerAddScore()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        if (playerScore >= targetScore)
        {
            restartButton.SetActive(true);
            WinScreen.gameObject.SetActive(true);
            WinScreen.text = "Player Won!";
            DisableOnWin();
        }
    }

    public void AgentAddScore()
    {
        agentScore++;
        agentScoreText.text = agentScore.ToString();
        if (agentScore >= targetScore)
        {
            restartButton.SetActive(true);
            WinScreen.gameObject.SetActive(true);
            WinScreen.text = "Agent Won!";
            DisableOnWin();
        }
    }

    private void DisableOnWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerController.enabled = false;
        playerBallSpawner.enabled = false;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloads current scene  
    }
}
