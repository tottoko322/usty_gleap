using UnityEngine;

// [CreateAssetMenu(fileName = "TargetStatusAction", menuName = "Scriptable Objects/TargetStatusAction")]
public abstract class TargetStatusAction : ScriptableObject
{
    public abstract void Execute(GameObject user,GameObject target);
}
