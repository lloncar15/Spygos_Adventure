using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public CharacterStats player;

    public void SetMaxHealth()
    {
        slider.maxValue = player.getMaxHealth();
        slider.value = GameController.Instance.health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth()
    {
        slider.value = GameController.Instance.health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
