using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossAttackData bossAttackData;
    private PlayerHealth player;
    private bool _isPlayerIn;
    public ElementType elementType;

    public virtual void _start()
    {
        player = FindObjectOfType<PlayerHealth>();
        _isPlayerIn = false;
    }

    public virtual void _update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerIn = false;
        }
    }

    private void Start()
    {
        _start();
    }

    private void Update()
    {
        _update();
    }

    public virtual void TryHit()
    {
        if (_isPlayerIn)
        {
            player.TakeDamage(bossAttackData.damage);
        }

        FinishAttack();
        Destroy(gameObject);
    }

    public virtual void FinishAttack()
    {

        FindObjectOfType<RemnantGenerator>().GenerateRemnant(transform.position, bossAttackData.radius / 2, 1, elementType);

        var generateAttacks = GetComponents<GenerateAttack>();

        foreach(var generator in generateAttacks)
        {
            //print("Generate Attack: " + generator);
            generator.Generate();
        }
    }
}
