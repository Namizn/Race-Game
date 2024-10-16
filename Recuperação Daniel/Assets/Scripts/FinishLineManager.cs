using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FinishLineManager : MonoBehaviourPun, IFinishable
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("OnFinishLineCrossed", RpcTarget.All, other.GetComponent<PhotonView>().Owner.NickName);
        }
    }

    [PunRPC]
    public void OnFinishLineCrossed(string playerName)
    {
        Debug.Log($"{playerName} cruzou a linha de chegada!");
        // Implementação adicional quando um jogador termina a corrida
    }
}
