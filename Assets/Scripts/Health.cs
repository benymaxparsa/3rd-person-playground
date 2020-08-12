using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startingHealth = 5;

    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

}
