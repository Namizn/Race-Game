using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackManager
{
    // Método para gerenciar a sincronização de posições dos carros
    void SyncCarPositions();

    // Método para verificar se algum carro cruzou a linha de chegada
    void CheckFinish();
}
