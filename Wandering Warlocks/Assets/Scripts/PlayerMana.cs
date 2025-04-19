using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 5f;

    public Slider manaSlider;

    void Start()
    {
        currentMana = maxMana;
        UpdateManaUI();
    }

    void Update()
    {
        RegenerateMana();
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            UpdateManaUI();
        }
    }

    public bool HasEnoughMana(float amount)
    {
        return currentMana >= amount;
    }

    public void UseMana(float amount)
    {
        if (HasEnoughMana(amount))
        {
            currentMana -= amount;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            UpdateManaUI();
        }
    }

    public void RegainMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        UpdateManaUI();
    }

    void UpdateManaUI()
    {
        if (manaSlider != null)
        {
            manaSlider.maxValue = maxMana;
            manaSlider.value = Mathf.Floor(currentMana);
        }
    }
}