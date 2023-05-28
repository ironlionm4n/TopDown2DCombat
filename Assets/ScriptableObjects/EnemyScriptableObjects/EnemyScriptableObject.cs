using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Create New Enemy Scriptable Object")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private int startingHealth;
    public int StartingHealth => startingHealth;
}