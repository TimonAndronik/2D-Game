using System.Collections;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private float _speed; // �������� ������
    [SerializeField] private Animator _animator; // �������
    private Rigidbody2D _rigidbody;
    private Enemy_follow _playerAwarenessController;
    private Vector2 _targetDirection;

    private int _currentState; // ���� �������

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
            // ������������ �������� ��� Blend Tree
            _animator.SetFloat("Horizontal", _targetDirection.x);

            // ����������, �� �� ��� � ���� ����
            SetAnimationState(1); // ���� "���"
        }
        else
        {
            // ���� ����� �� ��������, ���������� � ���� "idle"
            SetAnimationState(0); // ���� "�������"
        }
    }

    private void SetAnimationState(int state)
    {
        if (_currentState == state) return; 
        _currentState = state;
        _animator.SetInteger("State", _currentState);
    }
}
