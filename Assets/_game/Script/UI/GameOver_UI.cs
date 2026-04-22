using UnityEngine;
using UnityEngine.UI;

public class GameOver_UI : UI
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Button restartButton;

    public void Awake()
    {
        this.enabled = false;
    }
    private void OnEnable()
    {
        EventManager.instance.OnPlayerDied.AddListener(ShowScreen);
        EventManager.instance.RestartGame.AddListener(OffScreen);
        restartButton.onClick.AddListener(RestartGame);
    }
    private void OnDisable()
    {
        if(EventManager.instance == null)
        {
            return;
        }
        EventManager.instance.OnPlayerDied.RemoveListener(ShowScreen);
        EventManager.instance.RestartGame.RemoveListener(OffScreen);
        restartButton.onClick.RemoveListener(RestartGame);
    }
    private void ShowScreen()
    {
        gameOverScreen.SetActive(true);
    }

    private void OffScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void Start()
    {
        OffScreen();
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        EventManager.instance.RestartGame.Invoke();
    }

}

public class UI : MonoBehaviour
{
}
