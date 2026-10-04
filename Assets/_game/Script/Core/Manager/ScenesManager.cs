using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [Tooltip("1: Main scene; 2: Test scene")]
    [SerializeField] int firstSceneIndex = 1;


    

    public void Awake()
    {
        EventManager.instance.StartGame.AddListener(StartGame);

    }
    public async void StartGame()
    {
        await SceneManager.LoadSceneAsync(firstSceneIndex);
        EventManager.instance.OnEnterNewLevel.Invoke();
    }
}
