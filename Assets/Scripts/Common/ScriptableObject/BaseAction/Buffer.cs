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
        if (other.TryGetComponent<IHasStatusManager>(out var hasStatus))
        {
            StatusManager otherStatusManager = hasStatus.Status;
            otherStatusManager.AddBuff(copiedBuff);
        }
    }
}
