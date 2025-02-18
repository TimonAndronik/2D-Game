using System.Collections;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private float _speed; // Швидкість ворога
    [SerializeField] private Animator _animator; // Аніматор
    private Rigidbody2D _rigidbody;
    private Enemy_follow _playerAwarenessController;
    private Vector2 _targetDirection;

    private int _currentState; // Стан анімації

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<Enemy_follow>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        UpdateAnimation();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = _targetDirection * _speed;
        }
    }

    private void UpdateAnimation()
    {
        if (_targetDirection != Vector2.zero)
        {
            // Встановлюємо напрямок для Blend Tree
            _animator.SetFloat("Horizontal", _targetDirection.x);

            // Перевіряємо, чи ми вже у стані руху
            SetAnimationState(1); // Стан "рух"
        }
        else
        {
            // Якщо ворог не рухається, переходимо в стан "idle"
            SetAnimationState(0); // Стан "стоячий"
        }
    }

    private void SetAnimationState(int state)
    {
        if (_currentState == state) return; 
        _currentState = state;
        _animator.SetInteger("State", _currentState);
    }
}
