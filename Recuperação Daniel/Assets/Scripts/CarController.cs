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

    // Variáveis para armazenar entradas
    private float moveInput;
    private float turnInput;

    // Variáveis para sincronização
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    // Inicializa o componente Rigidbody.

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (photonView.IsMine)
        {
            // Opcional: Configure a câmera para seguir este carro
        }
        else
        {
            // Desativa o controle local para carros que não são do jogador
            enabled = false;
        }
    }

    // Captura a entrada do jogador.

    void Update()
    {
        if (photonView.IsMine)
        {
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }
        else
        {
            // Atualiza a posição e rotação do carro de acordo com a rede
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    // Movimenta e rotaciona o carro com base na entrada do jogador.
    
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Calcula o movimento e a rotação
            Vector3 movement = transform.forward * moveInput * speed * Time.fixedDeltaTime;
            float rotation = turnInput * turnSpeed * Time.fixedDeltaTime;

            // Aplica a física ao Rigidbody
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));

            // Sincroniza a posição e rotação com os outros jogadores
            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }
    
    // Método RPC para sincronizar a posição do carro com outros jogadores.
    [PunRPC]
    void SyncPosition(Vector3 position, Quaternion rotation)
    {
        networkPosition = position;
        networkRotation = rotation;
    }

    // Implementação da aceleração do carro.
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
