using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script wilhandle the mana ui

public class PlayerMana : MonoBehaviour
{
    [Header("Mana Setting")]
    public float maxMana = 100f;
    public float currentMana;
    public float spellCost = 20f;

    [Header("UI")]
    public TMP_Text manaText; // number display
    public ManaBar manaBar; // script reference

    void Start()
    {
        currentMana = maxMana;
        UpdateUI();
    }
    
    public bool CanCast()
    {
        return currentMana >= spellCost;
    }

    public void SpendMana()
    {
        currentMana -= spellCost;
        UpdateUI();

        if (currentMana <= 0)
        {
            currentMana = 0;
            GameManager.Instance.CheckForLoss();
        }
    }

    public void UpdateUI()
    {
        if (manaText != null)
        {
            manaText.text = $"Mana: {Mathf.Ceil(currentMana)}";
        }
        if (manaBar != null)
        {
            manaBar.SetMana(currentMana / maxMana);
        }
    }
}
