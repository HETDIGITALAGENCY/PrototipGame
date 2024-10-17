using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRate;
    public int maxHealth = 100;
    public int currentHealth;

    private float _horizontal;
    private float _speed = 6f;
    private float _jumpPower = 10f;
    private bool _canDash = true;
    private bool _isDashing = false;
    private float _dashPower = 8f;
    private float _dashingTime = 1f; // �imdilik
    private float _dashingCooldown = 1f; // �imdilik
    private bool _isFacingRight = true;
    private float _attackRange = 0.5f;
    private float _nextAttackTime = 0f;

    private AudioManager audioManager; //devamı gelicek E

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _attackLayer;

    private void Awake()
    {
      audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // ben ekledim devamı gelicek E
        // kanka bunun daha optimize bi yolu yok mudur. Emirhan 

    }


    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (_isDashing)
        {
            return;
        }

        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }
            if (_isDashing == true)
            {
                // Animasyon burada ba�lar
            }
            else
            {
                // Animasyon burada durur
            }

        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                _nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        Flip();
    }
    private void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }

        _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
    }
    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }
        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _rb.velocity = new Vector2(transform.localScale.x * _dashPower, 0f);
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Ai_Enemy>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
