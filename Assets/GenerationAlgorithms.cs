using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GenerationAlgorithms
{
    public int mNeededToGrowth;
    public int mGenerationAmountPerTick;
    public CellManager cellManager;
    public Organ mOrgan;

    public GenerationAlgorithms() { }

    public void GenerationPerTick()
    {
        if (mOrgan.numberOfUnits > 0)
        {

            cellManager.cells += mOrgan.cellsPerTick / (mOrgan.numberOfUnits / 10);
            mOrgan.cellsPerClick = mOrgan.cellsPerTick * (mOrgan.numberOfUnits) * (1+ cellManager.people);
            mOrgan.pricePerUnit = mOrgan.pricePerUnit * (mOrgan.numberOfUnits + 1);
        }
    }
}
