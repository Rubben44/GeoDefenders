using System.Collections;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinsAmount_TXT;
    public int CurrentCoins => currentCoins;
    private int currentCoins;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentCoins = int.MaxValue;
        UpdateUI();
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UpdateUI();
    }

    public void RemoveCoins(int coinsToRemove)
    {
        currentCoins -= coinsToRemove;
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinsAmount_TXT.text = currentCoins.ToString();
    }

}
