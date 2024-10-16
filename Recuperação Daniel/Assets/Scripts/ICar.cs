using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    void Accelerate(float amount);
    void Drive(float amount);
    void UpdatePosition();
}
