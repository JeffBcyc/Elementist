using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayHealth : MonoBehaviour
{
    private string currentHealth, maxHealth;

    [SerializeField] private HealthBar healthBar;


    private void Update()
    {
        currentHealth = healthBar.GetComponent<Slider>().value.ToString();
        maxHealth = healthBar.GetComponent<Slider>().maxValue.ToString();
        gameObject.GetComponent<Text>().text = currentHealth + "/" + maxHealth;
    }
}