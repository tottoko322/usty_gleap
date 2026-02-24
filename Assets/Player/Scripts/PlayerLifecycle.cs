using UnityEngine;

public class PlayerLifecycle : MonoBehaviour
{
    void Start()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.RegisterPlayer(transform);
    }

    void OnDestroy()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.UnregisterPlayer(transform);
    }
}