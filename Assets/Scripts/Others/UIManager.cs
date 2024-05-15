using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("MainPanels")]
    [SerializeField] private Button mainButton;
    [SerializeField] private GameObject infoPanel, utilityPanel;
    [SerializeField] private GameObject leftArrow, upArrow;
    [SerializeField] private GameObject infoSign, towerSign;

    [Header("EnemyInfo")]
    [SerializeField] private GameObject physicalArrmor, magicalArrmor;
    [SerializeField] private GameObject currentEnemyInfoPanel;
    [SerializeField] private TextMeshProUGUI currentEnemyHP;
    [SerializeField] private TextMeshProUGUI currentEnemyArrmorStaus;
    [SerializeField] private TextMeshProUGUI currentEnemyMoveSpeed;
    [SerializeField] private TextMeshProUGUI currentEnemyDamage;

    [Header("TowerInfo")]
    [SerializeField] private GameObject physicalDamage, magicalDamage;
    [SerializeField] private GameObject currentTowerInfoPanel;
    [SerializeField] private TextMeshProUGUI currentTowerDamage;
    [SerializeField] private TextMeshProUGUI currentTowerDamageType;
    [SerializeField] private TextMeshProUGUI currentTowerAttackSpeed;
    [SerializeField] private TextMeshProUGUI currentTowerAmmoAmount;

    [SerializeField] private GameObject towerUtilityPanel;
    [SerializeField] private GameObject currentTowerUtilityInfo;
    [SerializeField] private TextMeshProUGUI upgradeCost;

    [Header("UtilityPanel - Building")]
    [SerializeField] private GameObject buildingButtonsPanel;
    [SerializeField] private BuyBuildingButton[] buyBuildingButtons;

    [Header("UtilityPanel - Math")]
    [SerializeField] private GameObject mathUtilityPanel;
    [SerializeField] private GameObject mathEquationPanel;
    [SerializeField] private GameObject mathAnswersPanel;
    [SerializeField] private List<Button> answerButtons;
    [SerializeField] private TextMeshProUGUI equationText;
    [SerializeField] private RectTransform mathEQPanelRectT;

    private MathEquation currentEquation;
    private Tower currentTower;

    private bool utilityPanelIsTransitioning = false;
    private bool infoPanelIsTransitioning = false;
    private bool isEnemyInfoOpen = false;
    private bool isTowerInfoOpen = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        infoPanel.SetActive(false);
        utilityPanel.SetActive(false);
        infoSign.SetActive(false);
        towerSign.SetActive(false);
        towerUtilityPanel.SetActive(false);
        currentTowerInfoPanel.SetActive(false);
        currentEnemyInfoPanel.SetActive(false);
        buildingButtonsPanel.SetActive(false);
        mathAnswersPanel.SetActive(false);
        mathEquationPanel.SetActive(false);
        mathUtilityPanel.SetActive(false);
        mathUtilityPanel.transform.localScale = new(0, 1, 1);
        infoPanel.transform.localScale = new(1, 0, 1);
        utilityPanel.transform.localScale = new(0, 1, 1);
        towerUtilityPanel.transform.localScale = new(0, 1, 1);

        mathEQPanelRectT.sizeDelta = new(mathEQPanelRectT.sizeDelta.x, 0f);
    }

    #region ---- Building ---- 
    public void OpenUtilityPanel(GameObject olderLocation, Transform towerPlace, Tower.TowerLocation towerLocation)
    {
        utilityPanel.SetActive(true);
        leftArrow.SetActive(false);
        utilityPanel.transform.DOScaleX(1f, 2f).OnComplete(() =>
        {
            buildingButtonsPanel.SetActive(true);
        });

        foreach(BuyBuildingButton buildingButton in buyBuildingButtons)
        {
            buildingButton.PlaceToBuildTower = towerPlace;
            buildingButton.CurrentTowerLocation = towerLocation;
            buildingButton.OldBuild = olderLocation;
        }

    }

    public void CloseUtilityPanel()
    {
        buildingButtonsPanel.SetActive(false);

        utilityPanel.transform.DOScaleX(0f, 2f).OnComplete(() => {
            utilityPanel.SetActive(false);
            leftArrow.SetActive(true);
        });
    }
    
    public void ShowBuildingInfoPanel(GameObject panelToShow)
    {
        panelToShow.SetActive(true);
    }

    public void HideBuildingInfoPanel(GameObject panelToHide)
    {
        panelToHide.SetActive(false);
    }

    #endregion

    #region ---- ReloadUI ----
    public void OpenMathEquationPanel(MathEquation equation, Tower tower)
    {
        currentEquation = equation;
        currentTower = tower;
        equationText.text = "";
        mathUtilityPanel.SetActive(true);

        mathUtilityPanel.transform.DOScaleX(1f, 2f).OnComplete(() =>
        {
            mathAnswersPanel.SetActive(true);
            mathEquationPanel.SetActive(true);
            mathEQPanelRectT.DOSizeDelta(new(mathEQPanelRectT.sizeDelta.x, 120f), 2f).OnComplete(() =>
            {
                equationText.text = equation.Equation;
            });
        });

        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (i < equation.PossibleAnswers.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = equation.PossibleAnswers[i];
                int index = i;
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(equation.PossibleAnswers[index]));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnAnswerSelected(string selectedAnswer)
    {
        MathManager.Instance.CheckAnswer(currentEquation, selectedAnswer, currentTower);
        equationText.text = "";
        mathEQPanelRectT.DOSizeDelta(new(mathEQPanelRectT.sizeDelta.x, 0f), 2f).OnComplete(() =>
        {
            mathAnswersPanel.SetActive(false);
            mathUtilityPanel.transform.DOScaleX(0f, 2f).OnComplete(() =>
            {
                HideMathUI();
            });

        });
        
    }

    public void HideMathUI()
    {
        mathEquationPanel.SetActive(false);
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }

    #endregion

    #region ---- InfoPanel ----
    private string GetMoveSpeed(float moveSpeed)
    {
        if (moveSpeed <= 5) return "SLOW";
        if (moveSpeed < 10 && moveSpeed > 5) return "MED";
        if (moveSpeed > 10) return "FAST";

        return "UNDEFINED";
    }
    private string GetArrmorStatus(EnemySO enemyData)
    {
        physicalArrmor.SetActive(true);
        magicalArrmor.SetActive(false);

        if (enemyData.PhysicalResistance == EnemySO.EnemyPhysicalResistance.None && enemyData.MagicResistance == EnemySO.EnemyMagicResistance.None) return "NONE";

        if (enemyData.PhysicalResistance != EnemySO.EnemyPhysicalResistance.None)
        {
            physicalArrmor.SetActive(true);
            magicalArrmor.SetActive(false);

            switch (enemyData.PhysicalResistance)
            {
                case EnemySO.EnemyPhysicalResistance.Low: return "LOW";
                case EnemySO.EnemyPhysicalResistance.Medium: return "MED";
                case EnemySO.EnemyPhysicalResistance.High: return "HIGH";
            }
        }

        if (enemyData.MagicResistance != EnemySO.EnemyMagicResistance.None)
        {
            physicalArrmor.SetActive(false);
            magicalArrmor.SetActive(true);

            switch (enemyData.MagicResistance)
            {
                case EnemySO.EnemyMagicResistance.Low: return "LOW";
                case EnemySO.EnemyMagicResistance.Medium: return "MED";
                case EnemySO.EnemyMagicResistance.High: return "HIGH";
            }
        }

        return "UNKNOWN";
    }
    private string GetDamageType(TowerSO towerData)
    {
        if (towerData.TowerDamageType == TowerSO.DamageType.Physical)
        {
            physicalDamage.SetActive(true);
            magicalDamage.SetActive(false);

            return "PHYSICAL";
        }

        if (towerData.TowerDamageType == TowerSO.DamageType.Magic)
        {
            physicalDamage.SetActive(false);
            magicalDamage.SetActive(true);

            return "MAGICAL";
        }

        return "UNKNOWN";
    }
    private string GetAttackSpeed(float attackSpeed)
    {
        if (attackSpeed < 1) return "SLOW";
        if (attackSpeed >= 1 && attackSpeed < 1.5) return "AVG";
        if (attackSpeed >= 1.5) return "FAST";

        return "UNDEFINED";
    }   

    public void ShowEnemyInfo(EnemySO enemyData, float currentEnemyHealthPoints)
    {
        if (isTowerInfoOpen)
        {
            HideTowerInfo();
        }

        isEnemyInfoOpen = true;
        isTowerInfoOpen = false;

        infoPanel.SetActive(true);
        currentTowerInfoPanel.SetActive(false);
        currentEnemyInfoPanel.SetActive(true);

        infoSign.SetActive(true);
        towerSign.SetActive(false);

        upArrow.SetActive(false);

        infoPanel.transform.DOScaleY(1f, 1f);

        currentEnemyHP.text = currentEnemyHealthPoints.ToString();
        currentEnemyDamage.text = enemyData.EnemyDamage.ToString();
        currentEnemyMoveSpeed.text = GetMoveSpeed(enemyData.EnemyMoveSpeed);
        currentEnemyArrmorStaus.text = GetArrmorStatus(enemyData);
    }

    public void ShowTowerInfo(TowerSO towerData, float currentTowerAmmo)
    {
        if (isEnemyInfoOpen)
        {
            HideEnemyInfo();
        }


        isTowerInfoOpen = true;
        isEnemyInfoOpen = false;

        infoPanel.SetActive(true);
        currentEnemyInfoPanel.SetActive(false);
        currentTowerInfoPanel.SetActive(true);

        infoSign.SetActive(false);
        towerSign.SetActive(true);

        upArrow.SetActive(false);

        infoPanel.transform.DOScaleY(1f, 1f);

        currentTowerDamage.text = towerData.AttackDamage.ToString();
        currentTowerAmmoAmount.text = currentTowerAmmo.ToString();
        currentTowerDamageType.text = GetDamageType(towerData);
        currentTowerAttackSpeed.text = GetAttackSpeed(towerData.AttackSpeed);
    }

    public void ShowTowerUtilityInfo(TowerSO towerUpgradeSO)
    {
        towerUtilityPanel.SetActive(true);
        currentTowerInfoPanel.SetActive(true);

        leftArrow.SetActive(false);

        towerUtilityPanel.transform.DOScaleX(1f, 1f);

        upgradeCost.text = towerUpgradeSO.TowerPrice.ToString();
    }

    public void HideTowerUtilityInfo()
    {
        currentTowerInfoPanel.SetActive(false);
        towerUtilityPanel.transform.DOScaleX(0f, 1f).OnComplete(() =>
        {
            towerUtilityPanel.SetActive(false);
        });
    }

    public void HideEnemyInfo()
    {
        if (!isTowerInfoOpen)
        {
            infoPanel.transform.DOScaleY(0f, 1f).OnComplete(() =>
            {
                infoPanel.SetActive(false);
                upArrow.SetActive(true);
            });
        }

        currentEnemyInfoPanel.SetActive(false);
        isEnemyInfoOpen = false;
        infoSign.SetActive(false);
    }

    public void HideTowerInfo()
    {
        if (!isEnemyInfoOpen)
        {
            infoPanel.transform.DOScaleY(0f, 1f).OnComplete(() =>
            {
                infoPanel.SetActive(false);
                upArrow.SetActive(true);
            });
        }

        currentTowerInfoPanel.SetActive(false);
        isTowerInfoOpen = false;
        towerSign.SetActive(false);
    }


    #endregion
}
