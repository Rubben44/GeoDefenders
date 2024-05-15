using System.Collections;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private TextMeshProUGUI coinsAmount_TXT;
    public int CurrentCoins => currentCoins;
    private int currentCoins;
    private int currentLives;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentCoins = 2000;
        currentLives = 3;
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

    public void LoseHealth(int amountToLose)
    {
        currentLives -= amountToLose;
        CheckHealth();
    }

    private void CheckHealth()
    {
        switch (currentLives)
        {
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;

            case 2:
                hearts[0].SetActive(false);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;

            case 1:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(true);
                break;

            case 0:
                Dead();
                break;
        }

        currentLives = Mathf.Clamp(currentLives, 0, 3);
    }
        
    private void Dead()
    {
        UIManager.Instance.ShowDeadScreen();
    }

    private void UpdateUI()
    {
        coinsAmount_TXT.text = currentCoins.ToString();
    }

}
