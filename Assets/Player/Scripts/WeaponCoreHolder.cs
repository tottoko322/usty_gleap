using System.Collections.Generic;
using UnityEngine;

public class WeaponCoreHolder : MonoBehaviour
{
    // Inspector でセットできるようにする場合
    private List<GameObject> weaponCoreList;

    void Start()
    {
        // リストが null の場合は初期化
        weaponCoreList = PlayerManager.Instance.GetPlayerWeaponCoresData();
    }

    public List<GameObject> GetWeaponCoreList(){
        return weaponCoreList;
    }

    public void AddWeaponCore(GameObject weaponCore)
    {
        weaponCoreList.Add(weaponCore);
    }

    public void DeleteAllWeaponCores()
    {
        weaponCoreList.Clear();
    }
}
