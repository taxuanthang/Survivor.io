using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attack/Base")]
public class Attack : ScriptableObject
{
    public virtual async Task PerformAttack(Transform origin, Transform target)
    {
    }
}
