using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RageBar_UI : MonoBehaviour
{
    public Slider rageBar;


    public void Awake()
    {
    }
    public void OnEnable()
    {
        EventManager.instance.OnUpdateRage.AddListener(UpdateRage);
    }

    public void OnDisable()
    {
        EventManager.instance.OnUpdateRage.RemoveListener(UpdateRage);
    }

    public void UpdateRage(float Exp)
    {

        rageBar.value = Exp;


    }
}
