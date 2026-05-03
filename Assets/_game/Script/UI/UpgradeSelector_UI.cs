using NaughtyAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelector_UI : MonoBehaviour
{
    public GameObject content;

    public UpgradeCard upgradeCardPrefab;

    public List<UpgradeCard> upgradeCardsList = new List<UpgradeCard>();


    public void OnPlayerReachNewLevel()
    {
        for (int i = 0; i < 3; i++)
        {
            Upgrade upgradeData = UpgradeDatabaseManager.instance.GetRandomUpgrade();
            upgradeCardsList[i].AssignUpgradeData(upgradeData);
        }
    }

    public void Start()
    {
        EventManager.instance.OnPlayerReachNewLevel.AddListener(OnPlayerReachNewLevel);

        for (int i = 0; i < 3; i++)
        {
            UpgradeCard upgradeCard = Instantiate(upgradeCardPrefab.gameObject, content.transform).GetComponent<UpgradeCard>();
            upgradeCardsList.Add(upgradeCard);
        }

        gameObject.SetActive(false);
    }

}
