using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPCHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;
    public float GetMaxHealth() => maxHealth;

    [SerializeField] private float currentHealth;
    public float GetCurrentHealth() => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }
}
