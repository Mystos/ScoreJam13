using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenericHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int m_currentHealth = 0;

    public int CurrentHealth { get => m_currentHealth; }

    [System.Serializable]
    public class OnHitEvent : UnityEvent<string, int> { }
    [System.Serializable]
    public class OnHealEvent : UnityEvent<string, int> { }
    [System.Serializable]
    public class OnDeathEvent : UnityEvent<string> { }

    [Header("Events")]
    public OnHitEvent onHit;
    public OnHealEvent onHeal;
    public OnDeathEvent onDeath;

    public Slider healthBar;

    void Start()
    {
        m_currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public bool IsAlive()
    {
        return m_currentHealth > 0;
    }

    public void Heal(int value)
    {
        if (!IsAlive()) return;

        m_currentHealth += value;
        if (m_currentHealth > maxHealth)
            m_currentHealth = maxHealth;
        onHeal.Invoke(gameObject.tag, value);
    }

    public void Hit(int value)
    {
        if (!IsAlive()) return;

        m_currentHealth -= value;
        healthBar.value = m_currentHealth;
        if (m_currentHealth <= 0)
        {
            m_currentHealth = 0;
            onDeath.Invoke(gameObject.tag);
        }
        else
            onHit.Invoke(gameObject.tag, value);
    }

    public void FullHeal()
    {
        m_currentHealth = maxHealth;
    }

    /// WARNING: For debug purpose only
    public int GetCurrentHealth()
    {
        return m_currentHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HeatSource")
        {
            FullHeal();
        }
    }

    public void DestroyGO()
    {
        Destroy(this.gameObject);
        WaveSystem wave = FindObjectOfType<WaveSystem>();
        if (wave != null)
        {
            wave.nbrOfEnemy--;
        }
    }
}
