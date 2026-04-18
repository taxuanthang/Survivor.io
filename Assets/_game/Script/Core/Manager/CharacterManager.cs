using UnityEngine;

public class CharacterManager: MonoBehaviour
{
    // class cha chung cho cả player và enemy, có thể để các hàm chung ở đây
    // như là HandleDamage, HandleDeath, v.v.

    // vì ko có playerHealthManager nên sẽ làm thêm CharacterHealthManager, sau đó playerHealthManager sẽ kế thừa từ CharacterHealthManager, và enemyHealthManager cx vậy
    public LayerMask _layerMask;
    [Header("Character")]
    [SerializeField] CharacterHealthManager characterHealthManager;

    public virtual void Awake()
    {
        if (characterHealthManager == null) characterHealthManager = GetComponent<CharacterHealthManager>();
    }

    public void HandleDamage(int damage)
    {
        Debug.Log($"{gameObject.name} takes {damage} damage.");
        characterHealthManager.TakeDamage(damage);
    }

    public bool IsDead()
    {
        return characterHealthManager.isDead;
    }

    public virtual void SetUp()
    {

    }
}

