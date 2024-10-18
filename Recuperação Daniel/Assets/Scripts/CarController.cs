using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Biblioteca para RPC
using UnityEngine;
using Photon.Pun;


/// Controla o movimento e a sincronização do carro do jogador em 2D.
public class CarController : MonoBehaviourPun, ICar
{
    // Velocidade de movimento do carro
    public float speed = 10f;
    // Velocidade de rotação do carro
    public float turnSpeed = 100f;

    // Referência ao Rigidbody do carro
    private Rigidbody2D rb;

    // Variáveis para armazenar entradas
    private float moveInput;
    private float turnInput;

    // Variáveis para sincronização
    private Vector2 networkPosition;
    private float networkRotation;

    bool controllerOn = true;

    [PunRPC]
    private void Initialize()
    {
        if (!photonView.IsMine)
        {
            Color color = Color.white;
            color.a = 0.5f;
            GetComponent<SpriteRenderer>().color = color;
            rb.isKinematic = true;
            controllerOn = false;
        }
    }

    // Inicializa o componente Rigidbody2D
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (photonView.IsMine)
        {
            // Configure a câmera para seguir este carro, se necessário
        }
        else
        {
            // Desativa o controle local para carros que não são do jogador
            enabled = false;
        }
    }

    // Captura a entrada do jogador
    void Update()
    {
        if (photonView.IsMine)
        {
            moveInput = Input.GetAxis("Vertical"); // Input de aceleração para frente/trás
            turnInput = Input.GetAxis("Horizontal"); // Input para virar à esquerda/direita
        }
        else
        {
            // Atualiza a posição e rotação do carro de acordo com a rede
            transform.position = Vector2.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, networkRotation), Time.deltaTime * 10);
        }
    }

    // Movimenta e rotaciona o carro com base na entrada do jogador
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Aplica a rotação primeiro (carro se orienta antes de se mover)
            float rotationAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation - rotationAmount);

            // Movimenta o carro na direção em que está apontando (eixo up é a frente)
            Vector2 movement = transform.up * moveInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement); // Atualiza a posição com MovePosition

            // Sincroniza a posição e rotação com os outros jogadores
            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }

    // Método RPC para sincronizar a posição do carro com outros jogadores
    [PunRPC]
    void SyncPosition(Vector2 position, float rotation)
    {
        networkPosition = position;
        networkRotation = rotation; // Armazena o valor da rotação (em graus)
    }

    // Implementação da aceleração do carro
    public void Accelerate(float amount)
    {
        // Implementação adicional de aceleração, se necessário
    }

    // Implementação da direção do carro
    public void Drive(float amount)
    {
        // Implementação adicional de direção, se necessário
    }

    // Atualiza a posição do carro (método adicional)
    public void UpdatePosition()
    {
        // Implementação adicional para atualização de posição
    }

}