using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject startUI; // 시작 UI
    public GameObject gameOverUI; // 게임 오버 UI
    public TextMeshProUGUI scoreText; // 점수 텍스트
    [Header("점수")]
    int score = 0; // 점수

    [Header("플레이어")]
    public PlayerController player;

    private static bool isRestarting = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스는 파괴
            return;
        }
        // 게임 일시 정지
        Time.timeScale = 0f;
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {

        if (isRestarting)
        {
            player.run = true; // 플레이어가 달리기 시작
            // 리스타트 상태에서는 startUI를 비활성화
            startUI?.SetActive(false);
            gameOverUI?.SetActive(false);
            Time.timeScale = 1f;
            isRestarting = false;
        }
        else
        {
            player.run = true; // 플레이어가 달리기 시작
            // 일반적인 첫 시작
            startUI?.SetActive(true);
            gameOverUI?.SetActive(false);
            Time.timeScale = 0f;
        }

        score = 0;
        if (scoreText != null)
            scoreText.text = "Score: 0";
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        startUI?.SetActive(false);
        gameOverUI?.SetActive(false);
    }
    public void RestartGame()
    {
        isRestarting = true; // 리스타트 상태 저장
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
    
}
