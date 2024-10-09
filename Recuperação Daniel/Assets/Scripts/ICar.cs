using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    // Método para movimentar o carro
    void Move(float horizontalInput, float verticalInput);

    // Método que detecta quando o carro cruza a linha de chegada
    bool CheckFinishLine();
}
