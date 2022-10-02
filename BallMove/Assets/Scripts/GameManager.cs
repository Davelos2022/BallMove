using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    [Space]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [Header("Text UI")]
    [Space]
    [SerializeField] private TextMeshProUGUI txtPlayerCoins;
    [Header("Link to AudioManager")]
    [Space]
    [SerializeField] private AudioManager audioManager;

    private int playerCoins;
    private int currentCoins;
    private int maxCoins;
    private bool isGame = false; public bool IsGame { get { return isGame; } }

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PreLoadGame();
    }

    private void PreLoadGame()
    {
        if (PlayerPrefs.HasKey("Coins"))
            playerCoins = PlayerPrefs.GetInt("Coins");
        else
            playerCoins = 0;

        txtPlayerCoins.text = $"{playerCoins}";
        currentCoins = 0;
    }

    public void CheckItem(List<GameObject> gameObjects)
    {
        for (int x = 0; x < gameObjects.Count; x++)
        {
            if (gameObjects[x].CompareTag("Coin"))
                maxCoins++;
        }
    }

    public void ResultGame(bool win)
    {
        isGame = false;

        if (win)
        {
            winPanel.SetActive(true);
            PlayerPrefs.SetInt("Coins", playerCoins);

            if (audioManager != null)
                audioManager.PlaySound(AudioManager.typeClips.Win);
        }
        else
        {
            losePanel.SetActive(true);

            if (audioManager != null)
                audioManager.PlaySound(AudioManager.typeClips.Lose);
        }
    }

    public void UpdateCoins()
    {
        currentCoins++;
        playerCoins++;

        txtPlayerCoins.text = $"{playerCoins}";

        if (audioManager != null)
            audioManager.PlaySound(AudioManager.typeClips.Give);

        if (currentCoins >= maxCoins)
        {
            ResultGame(true);
        }
    }

    public void PlayGame()
    {
        isGame = true;
    }

    public void ReplayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
