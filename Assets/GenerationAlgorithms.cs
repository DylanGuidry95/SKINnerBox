using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GenerationAlgorithms
{
    public int mNeededToGrowth;
    public int mGenerationAmountPerTick;

    public GenerationAlgorithms() { }

    public void GenerationPerTick(Organ o)
    {
        if (o.numberOfUnits >= 5)
        {
            o.owner.cells += o.cellsPerClick * (int)(o.numberOfUnits / 3.0f);
        }
    }
}
