using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MathEquationsDatabase", menuName = "Math/MathEquationsDatabase")]
public class MathEquationsDatabase : ScriptableObject
{
    public List<MathEquation> equations;
}
