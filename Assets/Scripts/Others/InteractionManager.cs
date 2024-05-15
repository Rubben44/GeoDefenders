using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [HideInInspector] public Enemy CurrentSelectedEnemy;

    private Camera mainCamera;
    private IInteractable currentTower;
    private void Awake()
    {
        Instance = this;
    }
 
    void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
         
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                IInteractable tower = hit.collider.GetComponent<IInteractable>();

                if (enemy)
                {
                    if (CurrentSelectedEnemy != enemy) 
                    {
                        CurrentSelectedEnemy = enemy;
                        UIManager.Instance.ShowEnemyInfo(enemy.GetEnemyInfo(), enemy.GetCurrentHP());
                        enemy.EnemyDestryed += OnEnemyDestroyed;
                    }
                }
                else if (CurrentSelectedEnemy)
                {
                    CurrentSelectedEnemy = null;
                    UIManager.Instance.HideEnemyInfo();
                }

                if (tower != null)
                {
                    currentTower = tower;
                    UIManager.Instance.ShowTowerInfo(tower.GetTowerInfo(), tower.GetCurrentAmmo());
                    UIManager.Instance.ShowTowerUtilityInfo(tower.GetUpgradeInfo());
                    TowerUtilityManager.Instance.SetTowerUtilityPanel(tower.GetCurrentTower(), tower.GetUpgradeInfo());
                }
                else
                {
                    currentTower = null;
                    UIManager.Instance.HideTowerInfo();
                    UIManager.Instance.HideTowerUtilityInfo();
                }
            }
            else
            {
                UIManager.Instance.HideEnemyInfo();
                UIManager.Instance.HideTowerInfo();
                UIManager.Instance.HideTowerUtilityInfo();
            }       
        }

        if (CurrentSelectedEnemy)
        {
            UIManager.Instance.ShowEnemyInfo(CurrentSelectedEnemy.GetEnemyInfo(), CurrentSelectedEnemy.GetCurrentHP());
        }

        if (currentTower != null)
        {
            UIManager.Instance.ShowTowerInfo(currentTower.GetTowerInfo(), currentTower.GetCurrentAmmo());
        }
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        if (CurrentSelectedEnemy == enemy)
        {
            CurrentSelectedEnemy = null;
            UIManager.Instance.HideEnemyInfo();
        }
        enemy.EnemyDestryed -= OnEnemyDestroyed;
    }
}
