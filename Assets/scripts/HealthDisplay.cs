using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health healthComponent;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBarFill;

    void Update()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        if (healthComponent != null && healthText != null && healthBarFill != null)
        {
            float currentHealth = healthComponent.GetCurrentHealth();
            float maxHealth = healthComponent.GetMaxHealth();

            healthText.text = $"{currentHealth:G} / {maxHealth:G}";
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("HealthDisplay: One or more references are not set.");
        }
    }
}
