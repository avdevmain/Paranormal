using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraits : MonoBehaviour
{

    public EnemyType enemyType;
    public bool hasPeePee;
    public string enemyName;
    public bool isShy;
    public int deathLength;
    
    public enum EnemyType
    {
        none,
        Spirit,
        Wraith,
        Phantom,
        Poltergeist,
        Banshee,
        Jinn,
        Mare,
        Revenant,
        Shade,
        Demon,
        Yurei,
        Oni
    }
}
