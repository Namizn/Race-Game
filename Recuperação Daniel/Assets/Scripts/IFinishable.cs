using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFinishable
{
    // M�todo chamado quando a linha de chegada � cruzada.
    void OnFinishLineCrossed();
}
