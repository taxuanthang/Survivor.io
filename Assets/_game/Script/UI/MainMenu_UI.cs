using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] Button _startGameButton;

    public void Awake()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    public void OnStartGameButtonClicked()
    {
        EventManager.instance.StartGame.Invoke();
    }
}
