using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Transform CurrentPlayer { get; private set; }
    public PlayerGraves playerGravesData;
    public PlayerWeaponCores playerWeaponCoresData;
    public event Action<Transform> OnPlayerChanged;

    void Awake()
    {
        ClearData();
        Debug.Log($"[PM] Awake {name} SO={playerGravesData}", this);
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

    public List<GameObject> GetPlayerGravesData()
    {
        return playerGravesData.graves;
    }

    public List<GameObject> GetPlayerWeaponCoresData()
    {
        return playerWeaponCoresData.weaponCores;
    }

    public void ClearData()
    {
        playerGravesData.graves.Clear();
        playerWeaponCoresData.weaponCores.Clear();
    }
}