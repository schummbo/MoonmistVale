using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool Stackable;
    public Sprite Icon;
}
