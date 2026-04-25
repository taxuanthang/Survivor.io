using UnityEngine;

public class CharacterHealthManager : MonoBehaviour
{
    // bây giờ có thể copy hết logic trên xuống
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead = false;

    CharacterManager character;

    public void Awake()
    {
        currentHealth = maxHealth;
        character = GetComponent<CharacterManager>();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            character.OnDie?.Invoke();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        // run death animation, disable player controls, etc.


    }


}

