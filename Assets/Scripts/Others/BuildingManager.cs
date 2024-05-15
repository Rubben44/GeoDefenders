using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void BuildTower(TowerSO towerToBuild, Transform buildLocation, Tower.TowerLocation towerLocation, GameObject oldBuild)
    {
        if (towerToBuild.TowerPrice <= EconomyManager.Instance.CurrentCoins)
        {
            switch (towerLocation)
            {
                case Tower.TowerLocation.Up:
                    BuildUp(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Up);
                    break;

                case Tower.TowerLocation.Down:
                    BuildDown(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Down);
                    break;

                case Tower.TowerLocation.Left:
                    BuildOnLeftPart(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Left);
                    break;

                case Tower.TowerLocation.Right:
                    BuildOnRightPart(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Right);
                    break;

                default:
                    Debug.Log("What position are you looking at??");
                    break;
            }

        }
    
    }

    public void UpgradeTower(TowerSO towerToBuild, Transform buildLocation, Tower.TowerLocation towerLocation, GameObject oldBuild)
    {
        switch (towerLocation)
        {
            case Tower.TowerLocation.Up:
                BuildUp(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Up);
                break;

            case Tower.TowerLocation.Down:
                BuildDown(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Down);
                break;

            case Tower.TowerLocation.Left:
                BuildOnLeftPart(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Left);
                break;

            case Tower.TowerLocation.Right:
                BuildOnRightPart(towerToBuild, buildLocation, oldBuild, Tower.TowerLocation.Right);
                break;

            default: Debug.Log("What position are you looking at??");
                break;
        }
    }

    private void BuildDown(TowerSO towerToBuild, Transform buildLocation, GameObject oldPlace, Tower.TowerLocation towerLocation)
    {
        if (towerToBuild.GetTowerType == TowerSO.TowerType.Temple)
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.Euler(new Vector3(0f, 270f, 0f)));
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }
        else
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }

        Destroy(oldPlace);
        EconomyManager.Instance.RemoveCoins(towerToBuild.TowerPrice);
    }

    private void BuildUp(TowerSO towerToBuild, Transform buildLocation, GameObject oldPlace, Tower.TowerLocation towerLocation)
    {

        if (towerToBuild.GetTowerType == TowerSO.TowerType.Temple)
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }
        else
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.identity);
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }

        Destroy(oldPlace);
        EconomyManager.Instance.RemoveCoins(towerToBuild.TowerPrice);
    }

    private void BuildOnLeftPart(TowerSO towerToBuild, Transform buildLocation, GameObject oldPlace, Tower.TowerLocation towerLocation)
    {
        if (towerToBuild.GetTowerType == TowerSO.TowerType.Temple)
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }
        else
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }

        Destroy(oldPlace);
        EconomyManager.Instance.RemoveCoins(towerToBuild.TowerPrice);
    }

    private void BuildOnRightPart(TowerSO towerToBuild, Transform buildLocation, GameObject oldPlace, Tower.TowerLocation towerLocation)
    {
        if (towerToBuild.GetTowerType == TowerSO.TowerType.Temple)
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.identity);
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }
        else
        {
            GameObject spawnedTower = Instantiate(towerToBuild.TowerPrefab, buildLocation.position, Quaternion.Euler(new Vector3(0f, 270f, 0f)));
            spawnedTower.GetComponent<Tower>().CurrentTowerLocation = towerLocation;
        }

        Destroy(oldPlace);
        EconomyManager.Instance.RemoveCoins(towerToBuild.TowerPrice);
    }
        
}
