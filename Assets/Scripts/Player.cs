using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    private float _horizontal;
    private float _speed = 6f;
    private float _jumpPower = 10f;
    private bool _canDash = true;
    private bool _isDashing = false;
    private float _dashPower = 8f;
    private float _dashingTime = 1f; // Þimdilik
    private float _dashingCooldown = 1f; // Þimdilik
    private bool _isFacingRight = true;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

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
                // Animasyon burada baþlar
            }
            else
            {
                // Animasyon burada durur
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
}
