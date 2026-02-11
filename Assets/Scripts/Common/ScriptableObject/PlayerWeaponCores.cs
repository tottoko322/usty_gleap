using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerWeaponCores", menuName = "Data/PlayerWeaponCores")]
public class PlayerWeaponCores : ScriptableObject
{
    public List<GameObject> weaponCores = new();
}
