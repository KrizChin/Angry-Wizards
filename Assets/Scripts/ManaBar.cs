using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image fill;

    public void SetMana(float percent)
    {
        fill.fillAmount = Mathf.Clamp01(percent);
    }
}
