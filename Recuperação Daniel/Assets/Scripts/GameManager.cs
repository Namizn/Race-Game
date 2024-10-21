using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{

    public Camera mainCamera;
    Vector2 screenBounds;
    int score;
    public Vector2 ScreenBounds { get => screenBounds; }
    public int Score { get => score; set => score = value; }

    const string playerPrefabPath = "Prefabs/Player";
    int playersInGame = 0;

    private void Start()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
    }

    private void CreatePlayer()
    {
        CarController car = NetworkManager.instance.Instantiate(playerPrefabPath, GetRandomSpawnPosition(), Quaternion.identity).GetComponent<CarController>();
        car.photonView.RPC("Initialize", RpcTarget.All);

        if (car.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("Este carro pertence ao jogador local.");

            // Atribui a câmera para seguir o carro do jogador
            CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(car.transform); // Define o carro como o alvo da câmera
                Debug.Log("Câmera configurada para seguir o carro.");
            }
            else
            {
                Debug.LogError("Componente CameraFollow não encontrado.");
            }
        }

    }

    [PunRPC]
    private void AddPlayer()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    public Vector3 GetRandomSpawnPosition()
    {
        // Defina aqui sua lógica para obter uma posição aleatória
        float x = Random.Range(0.6f, 1.6f);
        float y = Random.Range(5.46f, 4.5f);
        return new Vector3(x, y, 0);
    }
}