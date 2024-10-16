using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Gerencia a detecção de carros cruzando a linha de chegada.
public class FinishLineManager : MonoBehaviourPun, IFinishable
{

    // Detecta colisões com o trigger da linha de chegada.
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Chama o RPC para notificar todos os jogadores
            photonView.RPC("OnFinishLineCrossed", RpcTarget.All, other.GetComponent<PhotonView>().Owner.NickName);
        }
    }

    // Método RPC chamado quando um carro cruza a linha de chegada.
    [PunRPC]
    public void OnFinishLineCrossed(string playerName)
    {
        Debug.Log($"{playerName} cruzou a linha de chegada!");
        // Implementação adicional quando um jogador termina a corrida
    }
}
