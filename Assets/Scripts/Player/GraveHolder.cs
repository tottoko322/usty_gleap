using System.Collections.Generic;
using UnityEngine;

public class GraveHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> graves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // リストが null の場合は初期化
        graves ??= new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> GetGraves(){
        return graves;
    }

    public void AddGrave(GameObject grave)
    {
        graves.Add(grave);
    }
}
