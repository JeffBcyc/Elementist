using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayHealth : MonoBehaviour
{

    [SerializeField] HealthBar healthBar;
    string currentHealth, maxHealth;


    private void Update()
    {
        currentHealth = healthBar.GetComponent<Slider>().value.ToString();
        maxHealth = healthBar.GetComponent<Slider>().maxValue.ToString();
        gameObject.GetComponent<Text>().text = currentHealth + "/" + maxHealth;
    }

}
