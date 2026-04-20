using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.LoadSceneAsync(1);
    }


    public Action<float> OnHealthChanged;
    public Action<float> OnPlayerAmmoChanged;

    public UnityEvent<float> onHealth;

    public UnityEvent OnPlayerDied;

    public UnityEvent<float> OnGamePause;
    public UnityEvent<float> OnGameUnPause;

    public UnityEvent RestartGame;


}
