using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Transform CurrentPlayer { get; private set; }
    public event Action<Transform> OnPlayerChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterPlayer(Transform player)
    {
        if (player == null) return;

        CurrentPlayer = player;
        OnPlayerChanged?.Invoke(player);
    }

    public void UnregisterPlayer(Transform player)
    {
        if (CurrentPlayer != player) return;

        CurrentPlayer = null;
        OnPlayerChanged?.Invoke(null);
    }
}