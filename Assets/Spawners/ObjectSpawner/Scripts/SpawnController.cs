using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private ISpawner spawner;

    void Start()
    {
        spawner = GetComponent<ISpawner>();

        if (spawner == null)
        {
            Debug.LogError("ISpawner not found!");
        }
    }
}