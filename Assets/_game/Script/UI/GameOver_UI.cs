using UnityEngine;
using UnityEngine.UI;

public class GameOver_UI : UI
{
    [SerializeField] private Button restartButton;

    private void OnEnable()
    {
        EventManager.instance.OnPlayerDied.AddListener(ShowScreen);
        restartButton.onClick.AddListener(RestartGame);
    }
    private void OnDisable()
    {
        EventManager.instance.OnPlayerDied.RemoveListener(ShowScreen);
        restartButton.onClick.RemoveListener(RestartGame);
    }
    private void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        EventManager.instance.RestartGame.Invoke();
    }

}

public class UI : MonoBehaviour
{
}
