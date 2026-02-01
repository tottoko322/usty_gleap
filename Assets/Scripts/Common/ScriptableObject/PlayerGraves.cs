using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGraves", menuName = "Data/PlayerGraves")]
public class PlayerGraves : ScriptableObject
{
    public List<GameObject> graves = new();
}
