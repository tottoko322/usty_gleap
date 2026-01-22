using System.Collections.Generic;
using UnityEngine;

public class HitBoxHolder : MonoBehaviour
{
    // Inspector でセットできるようにする場合
    [SerializeField] private List<GameObject> hitBoxList;

    void Start()
    {
        // リストが null の場合は初期化
        hitBoxList ??= new List<GameObject>();
    }

    public List<GameObject> GetHitBoxList(){
        return hitBoxList;
    }

    public void AddHitBox(GameObject hitBox)
    {
        hitBoxList.Add(hitBox);
    }
}
