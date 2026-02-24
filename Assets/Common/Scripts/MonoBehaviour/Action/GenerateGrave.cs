using System;
using UnityEngine;

public class GenerateGrave : MonoBehaviour
{
    public GameObject Grave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(Vector2 pos)
    {
        Instantiate(
            Grave,
            pos,
            Quaternion.identity
        );
    }
}
