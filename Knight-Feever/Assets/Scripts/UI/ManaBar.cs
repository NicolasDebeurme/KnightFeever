using System;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public Text manaText;

    public void SetMaxMana(int mana)
    {
        slider.maxValue= mana;
        slider.value = mana;
        manaText.text = mana.ToString()+"/"+mana.ToString();
    }
    public void SetMana(int mana)
    {
        slider.value = mana;
        manaText.text = mana.ToString() + "/" + slider.maxValue.ToString();
    }
}
