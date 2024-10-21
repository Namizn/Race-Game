using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Gerencia a detec��o de carros cruzando a linha de chegada.
public class FinishLineManager : MonoBehaviourPun
{

    // M�todo chamado quando um objeto entra no Collider da linha de chegada
    private void OnTriggerEnter(Collider other)
    {
        // Tenta obter a interface IFinishable do objeto que cruzou a linha de chegada
        IFinishable finishable = other.GetComponent<IFinishable>();

        if (finishable != null)
        {
            // Se o objeto implementa IFinishable, chama o m�todo OnFinishLineCrossed
            finishable.OnFinishLineCrossed();
        }

        if (other.CompareTag("Player"))
        {
            PhotonView playerPhotonView = other.GetComponent<PhotonView>();

            // Verifique se o carro que cruzou a linha de chegada pertence ao jogador local
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                Debug.Log("Voc� cruzou a linha de chegada!");

                // Chame o c�digo para registrar a vit�ria ou completar a corrida
                PlayerFinishedRace();
            }
        }
    }

    void PlayerFinishedRace()
    {
        Debug.Log("Corrida finalizada!");
        // Aqui voc� pode implementar a l�gica de final de corrida, como:
        // - Mostrar a tela de vit�ria
        // - Notificar outros jogadores
        // - Enviar um RPC para comunicar que a corrida acabou
    }
}
