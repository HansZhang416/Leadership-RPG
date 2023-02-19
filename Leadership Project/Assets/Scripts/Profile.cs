using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "CharacterProfile")]
public class Profile : ScriptableObject
{
    [Header("Skintone")]
    public Sprite skintone;

    [Header("Clothing")]
    public Sprite shirt;
    public Sprite pants;
    public Sprite shoes;

    [Header("Face")]
    public Sprite hair;
    public Sprite hats;

    [Header("Equipment")]
    public Sprite weapon;
    public Sprite shield;
}