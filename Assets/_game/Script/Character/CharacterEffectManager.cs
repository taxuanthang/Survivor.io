using UnityEngine;

public class CharacterEffectManager : MonoBehaviour
{
    [SerializeField] protected float bloodDuration =0.3f;

    protected CharacterManager character;

    public void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public void OnEnable()
    {
        character.OnHit.AddListener(SplitBlood);
    }

    public void OnDisable()
    {
        character.OnHit.RemoveListener(SplitBlood);
    }

    public virtual void SplitBlood(int dmg)
    {
        GameObject newBlood = PoolManager.instance.Get(PoolType.BloodVFX); // Get a blood from the pool
        newBlood.transform.position = character.transform.position;
        DestroyTimer timer = newBlood.gameObject.GetComponent<DestroyTimer>();
        timer.poolType = PoolType.BloodVFX;
        timer.timeToDestroy = bloodDuration;
    }


}
