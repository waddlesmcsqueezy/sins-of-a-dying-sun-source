using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 50.0f;
    [SerializeField] private float _currentHealth;

    [SerializeField] private int _level = 1;

    [SerializeField] private GameObject _particleSystemPrefab;

    private bool _isEmitting = false;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public int Level => _level;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageThis(float units)
    {
        if (_currentHealth - units < 0)
            Destroy(gameObject);
        else
        {
            _currentHealth -= units;
            if (!_isEmitting)
                StartCoroutine(EmitParticles());
        }
    }

    private IEnumerator EmitParticles()
    {
        _isEmitting = true;

        GameObject newParticleSystem = GameObject.Instantiate(_particleSystemPrefab);
        newParticleSystem.transform.position = gameObject.transform.position;

        newParticleSystem.GetComponent<ParticleSystem>().Emit(5);

        yield return new WaitForSeconds(2.0f);

        newParticleSystem.GetComponent<ParticleSystem>().Emit(5);

        yield return new WaitForSeconds(2.0f);

        newParticleSystem.GetComponent<ParticleSystem>().Emit(5);

        yield return new WaitForSeconds(4.0f);

        Destroy(newParticleSystem);
        _isEmitting = false;

        yield return null;
    }
}
