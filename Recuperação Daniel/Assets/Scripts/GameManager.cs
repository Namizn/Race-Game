using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{

    /*
    public Camera mainCamera; // Referência à câmera principal


    void Start()
    {
        // Se a câmera não foi atribuída no Inspector, tenta buscar a Main Camera automaticamente
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Carrega a cena de jogo para todos os jogadores
            PhotonNetwork.LoadLevel("Jogo");
        }
    }

    public void SpawnPlayer()
    {
        Debug.Log("Tentando spawnar o carro...");

        GameObject playerCar = PhotonNetwork.Instantiate("CarroPrefab", new Vector3(0, 2f, 0), Quaternion.identity);

        if (playerCar != null)
        {
            Debug.Log("Carro spawnado com sucesso.");
        }
        else
        {
            Debug.LogError("Falha ao spawnar o carro.");
        }

        if (playerCar.GetComponent<PhotonView>().IsMine)
        {
            CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
            cameraFollow.SetTarget(playerCar.transform);
        }

    }
    */
}