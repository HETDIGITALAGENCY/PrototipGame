using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private float _attackRate = 2f;
    private int _attackDamage = 2;
    private float _attackRange = 0.5f;
    private bool _isAttack = false;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _attackPoint;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _isAttack = true;
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _isAttack = false;
            StopCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        while (_isAttack == true)
        {
            _player.TakeDamage(_attackDamage);
            yield return new WaitForSeconds(_attackRate);
        }
    }

}
