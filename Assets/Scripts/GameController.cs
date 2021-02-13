using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class is in a Singleton Pattern.
public class GameController : MonoBehaviour {

    private static GameController instance;
    [SerializeField] private Text scoreText;
    private bool isGameOver;
    private int score;
    private int defaultTimeScale = 1;

    public static GameController GetInstance() {
        return instance;
    }

    public bool GetIsGameOver() {
        return isGameOver;
    }

    private void Awake() {
        Time.timeScale = defaultTimeScale;
        isGameOver = false;
        score = 0;
        scoreText.text = "0";

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            Debug.Log("There is already an instance of GameController. "
                    + "To avoid violating the Singleton Pattern of this class "
                    + "the other instance/s will be destroyed.");
        }
    }

    private void Start() {
        
    }

    private void Update() {
        if (GetIsGameOver() && Input.GetMouseButtonDown(0)) {
            RestartGame();
        }
    }

    public void AddScore() {
        if (GetIsGameOver()) {
            return;
        }
        ++score;
        scoreText.text = score.ToString();
    }

    public void GameOver() {
        isGameOver = true;
        Debug.Log("Game Over");
    }

    private void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
        Debug.Log("Game is restarted.");
    }

    public void PauseGame() {
        Time.timeScale = 0;
        Debug.Log("Game is paused.");
    }

    public void ResumeGame() {
        Time.timeScale = defaultTimeScale;
        Debug.Log("Game is resumed.");
    }
}
