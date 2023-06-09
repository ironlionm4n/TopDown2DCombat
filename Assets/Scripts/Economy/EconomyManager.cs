using MISC;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    private TMP_Text _goldText;
    private int _currentGold;

    public void UpdateCurrentGold()
    {
        _currentGold += 1;

        if (_goldText == null) _goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();

        _goldText.text = _currentGold.ToString("D3");
    }
}