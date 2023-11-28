using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = 1f;
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Debug.Log("YOU DIED!");
            currentHealth = 0f;
            healthText.text = "DEAD";
            healthSlider.value = 0f;
        }

        healthSlider.value = (currentHealth / maxHealth);
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
}
