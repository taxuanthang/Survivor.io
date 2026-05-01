using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public int nextLevelExp = 100;
    public int currentExp = 0;
    public int currentLevel = 0;
    public float catchingExpOrbRadius = 1f;
    public bool isDebugRadius = true;
    public List<PlayerLevel> playerLevels = new List<PlayerLevel>();

    public PlayerManager player;
    public void CatchEXPPoint()
    {

    }

    public void Awake()
    {
        currentLevel = 0;
        nextLevelExp = playerLevels[currentLevel + 1].EXPToReachThisLevel;
        player = GetComponent<PlayerManager>();
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

    public void Update()
    {
        Collider2D[] expOrbs = Physics2D.OverlapCircleAll(this.transform.position, catchingExpOrbRadius, UtilitiesManager.instance.expPointLayer);

        foreach (var expOrb in expOrbs)
        {
            if (expOrb.TryGetComponent<ExpPoint>(out ExpPoint expPoint))
            {

                expPoint.SetTarget(player);
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
}

[System.Serializable]
public class PlayerLevel
{
    public int level;
    public int EXPToReachThisLevel;
}
