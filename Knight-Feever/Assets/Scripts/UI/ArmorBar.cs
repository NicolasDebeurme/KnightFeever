using System;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{
    public Slider slider;
    public Text ArmorText;

    public void SetMaxArmor(int armor)
    {
        slider.maxValue= armor;
        slider.value = armor;
        ArmorText.text = armor.ToString()+"/"+armor.ToString();
    }
    public void SetArmor(int armor)
    {
        slider.value = armor;
        ArmorText.text = armor.ToString() + "/" + slider.maxValue.ToString();
    }
}
