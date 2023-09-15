using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _currentHealth;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _attackValue;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public int AttackValue => _attackValue;

    public void Heal(int units)
    {
        if (_currentHealth + units > _maxHealth)
            _currentHealth = _maxHealth;
        else _currentHealth += units;
    }

    public void Damage(int units)
    {
        if (_currentHealth - units < 0)
        {
            //kill
        }
        else
        {
            _currentHealth -= units;
        }
    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
    }
}
