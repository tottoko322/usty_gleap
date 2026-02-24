using UnityEngine;

// [CreateAssetMenu(fileName = "SelfStatusAction", menuName = "Scriptable Objects/SelfStatusAction")]
public abstract class SelfStatusAction : ScriptableObject
{
    public abstract void Execute(GameObject user);
}
