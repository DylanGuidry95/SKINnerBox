using UnityEngine;
using System.Collections;

public static class CellManager
{
    public static int cells;
    public static int people;

    public static Organ skin;
    public static Organ kidnies;
    public static Organ livers;
    public static Organ bones;
    public static Organ lungs;
    public static Organ hearts;
    public static Organ brains;
    public static Organ eyes;
}

public class Organ
{
    public int numberOfUnits;
    public int pricePerUnit;
    public int cellsPerTick;
}