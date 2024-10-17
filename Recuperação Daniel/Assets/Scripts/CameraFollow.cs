using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


/// Faz a câmera seguir o carro do jogador.
public class CameraFollow : MonoBehaviour
{
    // O carro que a câmera deve seguir
    private Transform target;

    // Posição relativa da câmera em relação ao carro
    [SerializeField] Vector3 offset;

    // Velocidade suave para movimentação da câmera
    public float smoothSpeed = 0.125f;

    // Ajusta a posição da câmera após o spawn do carro.
    void LateUpdate()
    {
        if (target != null)
        {
            // Posição desejada da câmera com o offset
            Vector3 desiredPosition = target.position + offset;

            // Movimento suave da câmera até a posição desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Aplica a posição suavizada à câmera
            transform.position = smoothedPosition;

            // Opcional: Faz a câmera olhar para o carro
            transform.LookAt(target);
        }
    }

    // Método público para definir o alvo (carro) que a câmera deve seguir.
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
