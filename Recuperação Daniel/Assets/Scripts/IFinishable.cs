using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFinishable
{
    // Método chamado quando a linha de chegada é cruzada.
    void OnFinishLineCrossed(string playerName);
}
