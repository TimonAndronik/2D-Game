using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_coin : MonoBehaviour
{
    public int value = 1; // ʳ������ ���� ��� �����, �� ��������� ��� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.AddCoins(value); // �������� �������� �����
            }

            Destroy(gameObject); // ������� ������ ���� �����
        }
    }
}
