using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        
    }
    private void Update()
    {
        _textMeshPro.text = $"{PlayerStats._player.Money}$";
    }
}
