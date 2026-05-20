using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar_UI : MonoBehaviour
{
    public Slider expBar;

    public TextMeshProUGUI levelText;

    public void Awake()
    {
    }
    public void OnEnable()
    {
        EventManager.instance.OnGetEXP.AddListener(UpdateEXP);
    }

    public void OnDisable()
    {
        EventManager.instance.OnGetEXP.RemoveListener(UpdateEXP);
    }

    public void UpdateEXP(float Exp,int currentLevel)
    {

        expBar.value = Exp;
        levelText.text = currentLevel.ToString();


    }
}
