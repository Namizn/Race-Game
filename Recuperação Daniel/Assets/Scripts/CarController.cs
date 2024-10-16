using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Biblioteca para RPC

public class CarController : MonoBehaviourPun, ICar
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float move = Input.GetAxis("Vertical") * speed;
            float turn = Input.GetAxis("Horizontal") * turnSpeed;

            Vector3 movement = transform.forward * move * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, turn * Time.deltaTime, 0);

            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);

            photonView.RPC("SyncPosition", RpcTarget.Others, rb.position, rb.rotation);
        }
    }

    [PunRPC]
    void SyncPosition(Vector3 position, Quaternion rotation)
    {
        rb.position = position;
        rb.rotation = rotation;
    }

    public void Accelerate(float amount)
    {
        // Implementação da aceleração
    }

    public void Drive(float amount)
    {
        // Implementação da direção
    }

    public void UpdatePosition()
    {
        // Implementação da atualização de posição
    }
}
