using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    // Método para acelerar o carro.
    void Accelerate(float amount);

    // Método para virar o carro.
    void Drive(float amount);

    // Atualiza a posição do carro.
    void UpdatePosition();
}
