using System.Collections;
using Q3Movement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterEvents))]
public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;
    public float GetMaxHealth() => maxHealth;

    [SerializeField] private float currentHealth;
    public float GetCurrentHealth() => currentHealth;

    [SerializeField] private Image damageOverlay;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float overlayAlphaOnDamage = 0.5f;

    private Coroutine fadeRoutine;
    private CharacterEvents eventsRef;
    private bool isDead;

    void Awake()
    {
        eventsRef = GetComponent<CharacterEvents>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        isDead = currentHealth <= 0f;
    }

    public void ChangeHealth(float amount)
    {
        if (isDead && amount < 0f) return;

        float prev = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);

        Debug.Log($"{gameObject.name} health changed by {amount}. Current health: {currentHealth}");

        // dano: efeito visual + emitir evento
        if (amount < 0f)
        {
            StartDamageOverlay();
            eventsRef.EmitHurt(Mathf.Abs(amount));
        }

        // morte (entra somente uma vez)
        if (!isDead && currentHealth <= 0f)
        {
            isDead = true;
            currentHealth = 0f;
            DeathSetup();
            eventsRef.EmitDie();
        }
    }

    #region Visual Effects (overlay)
    void StartDamageOverlay()
    {
        if (damageOverlay == null) return;

        // Reinicia o overlay pra uma cor vermelha com alpha configurado
        Color baseColor = damageOverlay.color;
        damageOverlay.color = new Color(0.7f, 0f, 0f, overlayAlphaOnDamage);

        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeOverlay());
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
    #endregion

    void DeathSetup()
    {
        Debug.Log($"{gameObject.name} has died.");

        var col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        var cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // desliga scripts relevantes de controle
        var q3 = GetComponent<Q3PlayerController>();
        if (q3 != null) q3.enabled = false;

        this.enabled = false;

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}