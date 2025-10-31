using UnityEngine;
using System.Collections.Generic;

public class DamagingArea : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 10f;
    [SerializeField] private float tickInterval = 1f; // tempo entre danos

    private Dictionary<Health, float> damageTimers = new();

    void OnTriggerStay(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null) return;

        if (!damageTimers.ContainsKey(health))
            damageTimers[health] = 0f;

        damageTimers[health] += Time.deltaTime;

        if (damageTimers[health] >= tickInterval)
        {
            health.ChangeHealth(-damagePerSecond);
            damageTimers[health] = 0f;
            Debug.Log($"{other.name} took {damagePerSecond} damage from {name}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
            damageTimers.Remove(health);
    }
}
