using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TowerUtilityManager : MonoBehaviour
{
    public static TowerUtilityManager Instance { get; private set; }

    [SerializeField] private MathEquationsDatabase equationsDatabase;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button upgradeButton;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }


    public void SetTowerUtilityPanel(Tower towerToSet, TowerSO upgradeTowerSO)
    {
        reloadButton.onClick.RemoveAllListeners();
        reloadButton.onClick.AddListener(() => RequestReload(towerToSet));

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => UpgradeTower(towerToSet, upgradeTowerSO));
    }

    private void RequestReload(Tower towerToRequestReload)
    {
        MathEquation equation = GetRandomMathEquation();
        UIManager.Instance.OpenMathEquationPanel(equation, towerToRequestReload);
    }

    private void UpgradeTower(Tower tower, TowerSO upgradeTowerSO)
    {
        GameObject lastTower = tower.gameObject;
        Debug.Log("Clicked on upgrade!");

        if (upgradeTowerSO.TowerPrice <= EconomyManager.Instance.CurrentCoins)
        {
            BuildingManager.Instance.UpgradeTower(upgradeTowerSO, tower.transform, tower.CurrentTowerLocation, lastTower);
            Debug.Log("Building the tower" + upgradeTowerSO);
        }
    }

    private MathEquation GetRandomMathEquation()
    {
        if (equationsDatabase.equations.Count == 0)
        {
            Debug.LogError("No equations in the database!");
            return null;
        }

        int index = Random.Range(0, equationsDatabase.equations.Count);
        return equationsDatabase.equations[index];
    }
}
