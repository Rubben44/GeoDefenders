using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy / CreateEnemySO")]
public class EnemySO : ScriptableObject
{
    public GameObject EnemyPrefab => enemyPrefab;
    public float EnemyHP => enemyHP;
    public float EnemyMoveSpeed => enemyMoveSpeed;
    public int EnemyDamage => enemyDamage;
    public EnemyPhysicalResistance PhysicalResistance => physicalResistance;
    public EnemyMagicResistance MagicResistance => magicResistance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemyHP;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private int enemyDamage = 1;
    [SerializeField] private EnemyPhysicalResistance physicalResistance;
    [SerializeField] private EnemyMagicResistance magicResistance;

    public enum EnemyPhysicalResistance
    {
        None, 
        Low,
        Medium,
        High
    }
    public enum EnemyMagicResistance
    {
        None,
        Low,
        Medium, 
        High
    }


}
