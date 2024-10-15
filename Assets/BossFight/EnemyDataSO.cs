using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Enemy Data", order = 0)]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxPower;
    [SerializeField] private int _maxRage;
    [SerializeField] private int _maxSpeed;
    [SerializeField] private EnumEnemyState _defaultState;

    public string Name => _name;
    public EnumEnemyState DefaultState => _defaultState;
    public int MaxHealth => _maxHealth;
    public int MaxPower => _maxPower;
    public int MaxRage => _maxRage;
    public int MaxSpeed => _maxSpeed;
}
