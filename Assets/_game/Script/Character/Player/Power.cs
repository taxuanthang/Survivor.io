using System;
using UnityEngine;

[System.Serializable]
public class Power : ScriptableObject
{
    public virtual void UseAbility()
    {
        // Implement dodge ability logic here
        Console.WriteLine("Dodge ability used!");
    }

    public virtual void UseAbility(Transform transform, PlayerManager player)
    {

    }
}



