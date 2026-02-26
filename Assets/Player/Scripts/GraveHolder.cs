using System.Collections.Generic;
using UnityEngine;

public class GraveHolder : MonoBehaviour
{
    private List<GameObject> graves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        graves = PlayerManager.Instance.GetPlayerGravesData();
    }

    public List<GameObject> GetGraves(){
        return graves;
    }

    public void RemoveFirstGrave()
    {
        Debug.Log("お墓の数は" + graves.Count);
        if (graves.Count > 0)
        {
            graves.RemoveAt(0);
        }
    }

    public void AddGrave(GameObject grave)
    {
        graves.Add(grave);
    }

    public void DeleteAllGraves()
    {
        graves.Clear();
    }
}
