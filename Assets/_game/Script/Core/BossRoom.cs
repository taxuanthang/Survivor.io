using System.Collections.Generic;

public class BossRoom : Room
{
    public List<BossManager> thisRoomBosses;


    public override void Update()
    {
        base.Update();
    }

    public void ActivateBoss(PlayerManager player)
    {
        // spawn boss
        foreach (var boss in thisRoomBosses)
        {
            boss.Activate(player);
        }    
    }

    public override void OnPlayerCrossDoor(PlayerManager player)
    {
        if (triggered == false)
        {
            triggered = true;
            ActivateBoss(player);
        }
        else
        {

        }
    }
}