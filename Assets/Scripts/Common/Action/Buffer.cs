using UnityEngine;

public class Buffer : MonoBehaviour
{
    [SerializeField] private Buff buff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveBuff(GameObject other)
    {
        Buff copiedBuff = Instantiate(buff);
        StatusManager otherStatusManager = other.GetComponent<StatusManager>();
        otherStatusManager.AddBuff(copiedBuff);
    }
}
