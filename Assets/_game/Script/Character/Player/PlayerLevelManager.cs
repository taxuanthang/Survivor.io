using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelManager : MonoBehaviour
{
    public int nextLevelExp = 100;
    public int currentExp = 0;
    public int currentLevel = 0;
    public int lastLevelChecked = 0;
    public float catchingExpOrbRadius = 1f;
    public bool isDebugRadius = true;
    public float expOrbFlyTime = 1f;
    public List<PlayerLevel> playerLevels = new List<PlayerLevel>();

    public Queue<int> levelQueue = new Queue<int>();

    public PlayerManager player;

    public UnityEvent OnNewLevelReach;

    public void Awake()
    {
        currentLevel = 0;
        nextLevelExp = playerLevels[currentLevel + 1].EXPToReachThisLevel;
        player = GetComponent<PlayerManager>();
    }

    public void Start()
    {
        EventManager.instance.OnPlayerCompleteSelectingCard.AddListener(OnPlayerCompleteSelectCard);
        EventManager.instance.OnFinishEnemyRoom.AddListener(CatchAllEXPOrb);
    }
    public void Update()
    {
        CatchEXPPoint();

    }

    public void LevelUp()
    {
        currentLevel++;
        currentExp = 0;
        nextLevelExp = playerLevels[currentLevel+1].EXPToReachThisLevel;
        //cho chọn thẻ?
    }

    public void IncreaseEXP(int exp)
    {
        print(exp);
        if (currentLevel >= playerLevels.Count - 1)
        {
            Debug.Log("Max level reached.");
            return;
        }
        currentExp += exp;
        if (currentExp >= nextLevelExp)
        {
            LevelUp();
        }
    }

    public void CatchEXPPoint()
    {
        Collider2D[] expOrbs = Physics2D.OverlapCircleAll(this.transform.position, catchingExpOrbRadius, UtilitiesManager.instance.expPointLayer);

        foreach (var expOrb in expOrbs)
        {
            if (expOrb.TryGetComponent<ExpPoint>(out ExpPoint expPoint))
            {

                expPoint.SetTarget(player, expOrbFlyTime);
                expPoint.OnExpPointCollected.AddListener(IncreaseEXP);

            }
        }
    }



    private void OnDrawGizmos()
    {
        if(!isDebugRadius)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, catchingExpOrbRadius);
    }

    public void CheckCurrentPlayerLevelToCallUpgradeCard()
    {
        int levelGap = currentLevel - lastLevelChecked;
        if (levelGap >0)
        {
            EventManager.instance.PauseGame?.Invoke();
            EventManager.instance.OnPlayerReachNewLevel.Invoke();
            lastLevelChecked++;
        }
        else
        {
            EventManager.instance.UnPauseGame?.Invoke();
        }

    }

    public void OnPlayerCompleteSelectCard(Upgrade lastSelectedCard)
    {
        CheckCurrentPlayerLevelToCallUpgradeCard();
    }


    public async void CatchAllEXPOrb()
    {
        float temp = catchingExpOrbRadius;
        catchingExpOrbRadius = float.MaxValue;
        await Awaitable.WaitForSecondsAsync(expOrbFlyTime+1f);
        catchingExpOrbRadius = temp;
        CheckCurrentPlayerLevelToCallUpgradeCard();

    }
}

[System.Serializable]
public class PlayerLevel
{
    public int level;
    public int EXPToReachThisLevel;
}
