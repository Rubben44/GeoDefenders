using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    public TowerLocation CurrentTowerLocation { get; set; }
    public abstract void ReloadAmmo();

    public enum TowerLocation
    {
        Up,
        Down,
        Left,
        Right
    }

}
