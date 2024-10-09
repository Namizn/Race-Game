using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviourPunCallbacks, ICar
{
    public float speed = 10f;
    public Transform finishLine;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Só permite que o jogador controle o próprio carro
        if (photonView.IsMine)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Move(horizontal, vertical);
        }
    }

    // Move o carro de acordo com o input do jogador
    public void Move(float horizontalInput, float verticalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * speed;

        // Sincroniza a posição para outros jogadores
        photonView.RPC("SyncPosition", RpcTarget.Others, transform.position);
    }

    // Checa se o carro cruzou a linha de chegada
    public bool CheckFinishLine()
    {
        if (Vector2.Distance(transform.position, finishLine.position) < 1f)
        {
            return true;
        }
        return false;
    }

    // Sincroniza a posição do carro com os outros jogadores
    [PunRPC]
    void SyncPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
