using UnityEngine;
public abstract class Secret : ScriptableObject
{
    public Sprite Icon;
    public string displayName = "SECRET";
    public string description = "Obtained by secret move";
    public abstract void AddContent();
}