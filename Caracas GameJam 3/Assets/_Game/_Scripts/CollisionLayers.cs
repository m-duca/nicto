using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collision Layers", menuName = "Scriptable Objects/Collision Layers")]
public class CollisionLayers : ScriptableObject
{
    public int PlayerLayer;
    public int EnemyLayer;
    public int LampLayer;
    public int ObjectiveLayer;
    public int SwitchLayer;
    public int EggLayer;
    public int DarkLayer;
    public int TriggerEnemyLayer;
    public int TriggerEndLayer;
}
