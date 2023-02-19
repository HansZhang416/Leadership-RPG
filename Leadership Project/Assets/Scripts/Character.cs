using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Storage", menuName = "CharacterStorage")]
public class Character : ScriptableObject
{
    [Header("Skintone")]
    public List<Sprite> skintones;

    [Header("Clothing")]
    public List<Sprite> shirt;
    public List<Sprite> pants;
    public List<Sprite> shoes;

    [Header("Face")]
    public List<Sprite> hair;
    public List<Sprite> hats;

    [Header("Equipment")]
    public List<Sprite> weapon;
    public List<Sprite> shield;
}