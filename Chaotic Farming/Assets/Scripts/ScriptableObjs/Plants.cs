using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/Plant", order = 1)]
public class Plants : ScriptableObject
{
    public string plantName;
    public Sprite seeds;
    public Sprite harvest;
}
