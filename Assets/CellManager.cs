using UnityEngine;
using System.Collections;

public class CellManager : MonoBehaviour
{
    public int cells;
    public int people;

    public Organ skin;
    public Organ kidnies;
    public Organ livers;
    public Organ bones;
    public Organ lungs;
    public Organ eyes;
    public Organ hearts;
    public Organ brains;

    public void BuyOrgan(Organ type)
    {
        if(cells >= type.pricePerUnit)
        {
            cells -= type.pricePerUnit;
            type.numberOfUnits++;
        }
    }
}

public class Organ
{
    public int numberOfUnits;
    public int pricePerUnit;
    public int cellsPerTick;
}