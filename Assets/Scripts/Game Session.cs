using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerHealth = 3;
    int startHealth;

    void Awake()
    {
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        startHealth = playerHealth;
        Debug.Log("Player Health:" + playerHealth);
        Debug.Log("Player Lives:" + playerLives);
    }

    public void ProcessPlayerDamage()
    {
        if(playerHealth > 1)
        {
            TakeHealth();
        }
        else
        {
            TakeLife();
        }
    }

    void TakeHealth()
    {
        playerHealth--;
        Debug.Log("Player Health:" + playerHealth);
    }

    void TakeLife()
    {
        if(playerLives > 1)
        {
            playerLives--;
            playerHealth = startHealth;
            Debug.Log("Player Lives:" + playerLives);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            ResetGameSession();
        }

        void ResetGameSession()
        {
            FindFirstObjectByType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
