using System.Collections;
using Q3Movement;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float GetMaxHealth() => maxHealth;
    [SerializeField] private float currentHealth;
    public float GetCurrentHealth() => currentHealth;

    [SerializeField] private AudioClip[] damageSound;
    [SerializeField] private AudioClip[] deathSound;
    
    [SerializeField] private Image damageOverlay;
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine fadeRoutine;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        if (currentHealth <= 0 && amount < 0)
            return; // Já está morto, não aplica mais dano
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"{gameObject.name} health changed by {amount}. Current health: {currentHealth}");

        if (amount < 0)
            DamageEffect();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DeathEffect();
            DeathSetup();
        }
    }

    void DeathSetup()
    {
        Debug.Log($"{gameObject.name} has died.");

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<Health>().enabled = false;
        gameObject.GetComponent<Q3PlayerController>().enabled = false;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
    }

    void DeathEffect()
    {
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound[Random.Range(0, deathSound.Length - 1)], transform.position);
    }

    void DamageEffect()
    {
        if (damageSound != null)
            AudioSource.PlayClipAtPoint(damageSound[Random.Range(0, damageSound.Length - 1)], transform.position);

        if (damageOverlay != null)
        {
            // Reinicia o overlay pra 50% opaco vermelho
            damageOverlay.color = new Color(0.7f, 0, 0, 0.5f);

            // Se já tiver uma animação de fade rodando, para ela
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);

            fadeRoutine = StartCoroutine(FadeOverlay());
        }
    }

    IEnumerator FadeOverlay()
    {
        float elapsed = 0f;
        Color startColor = damageOverlay.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            damageOverlay.color = Color.Lerp(startColor, endColor, elapsed / fadeDuration);
            yield return null;
        }

        damageOverlay.color = endColor;
        fadeRoutine = null;
    }
}
