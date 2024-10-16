using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Biblioteca para RPC
public class CarController : MonoBehaviourPun, ICar
{
    // Velocidade de movimento do carro
    public float speed = 10f;
    // Velocidade de rotação do carro
    public float turnSpeed = 100f;

    // Referência ao Rigidbody do carro
    private Rigidbody rb;


    // Inicializa o componente Rigidbody.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Captura a entrada do jogador e movimenta o carro se for o jogador local.
    void Update()
    {
    // Verifica se este objeto pertence ao jogador local
        if (photonView.IsMine)
        {
        // Captura entrada de movimento e rotação
            float move = Input.GetAxis("Vertical") * speed;
            float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // Calcula o movimento e a rotação
            Vector3 movement = transform.forward * move * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, turn * Time.deltaTime, 0);

        // Move e rotaciona o carro
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);

        // Sincroniza a posição com os outros jogadores
            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }

    // Método RPC para sincronizar a posição do carro com outros jogadores.

    [PunRPC]
    void SyncPosition(Vector3 position, Quaternion rotation)
    {
        rb.position = position;
        rb.rotation = rotation;

    }
// Implementação da aceleração do carro
    public void Accelerate(float amount)
    {
    // Implementação da aceleração (pode ser expandida)
    }

    // Implementação da direção do carro.
    public void Drive(float amount)
    {
    // Implementação da direção (pode ser expandida)
    }

    // Atualiza a posição do carro.
    public void UpdatePosition()
    {
    // Implementação da atualização de posição (pode ser expandida)
    }
}
