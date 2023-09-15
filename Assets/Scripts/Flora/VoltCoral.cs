using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltCoral : Enemy
{
    private bool _playerIsInRange;
    [SerializeField] private AudioSource _idleAudioSource;
    [SerializeField] private AudioSource _attackAudioSource;

    [SerializeField] private GameObject _attackParticlePrefab;

    private float _timeOfPreviousAttack;

    [SerializeField] private float _attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (_playerIsInRange)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0.9 ||
                player.GetComponent<PlayerControlScript>().SpotlightEnabled)
            {
                if (Time.time - _timeOfPreviousAttack > _attackCooldown)
                {
                    player.GetComponent<PlayerControlScript>().Damage(_attackValue);

                    GameObject lightningParticle = GameObject.Instantiate(_attackParticlePrefab, transform);

                    //get difference in position, look at player based on atan2
                    Vector3 diff = (player.transform.position - lightningParticle.transform.position);
                    float angle = Mathf.Atan2(diff.y, diff.x);
                    lightningParticle.transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg) -90);

                    //scale sprite based on distance to player
                    float lightningScale = DistanceToPlayer() * 0.75f;
                    lightningParticle.transform.localScale = new Vector3(lightningScale, lightningScale, 1);

                    _attackAudioSource.Play();

                    _timeOfPreviousAttack = Time.time;
                }
            }
        }
    }

    void Awake()
    {
        _timeOfPreviousAttack = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player in range of volt coral");

        if (other.gameObject.tag == "Player")
            _playerIsInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("player no longer in range of volt coral");

        if (other.gameObject.tag == "Player")
            _playerIsInRange = false;
    }
}
