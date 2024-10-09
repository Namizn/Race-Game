using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviourPunCallbacks, ITrackManager
{
    public PlayerCar[] cars;

    void Update()
    {
        SyncCarPositions();
        CheckFinish();
    }

    // Sincroniza a posição dos carros
    public void SyncCarPositions()
    {
        foreach (var car in cars)
        {
            if (car.photonView.IsMine)
            {
                // Sincroniza apenas os carros locais
                car.photonView.RPC("SyncPosition", RpcTarget.Others, car.transform.position);
            }
        }
    }

    // Verifica se algum carro cruzou a linha de chegada
    public void CheckFinish()
    {
        foreach (var car in cars)
        {
            if (car.CheckFinishLine())
            {
                // Anuncia o vencedor
                photonView.RPC("DeclareWinner", RpcTarget.All, car.photonView.Owner.NickName);
            }
        }
    }

    // Método que será chamado para anunciar o vencedor
    [PunRPC]
    void DeclareWinner(string winnerName)
    {
        Debug.Log("O vencedor é: " + winnerName);
    }
}
