using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

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

    void Start()
    {
        skin = new Organ(this, 1, 2);
    }

    void BuildPerson()
    {
        if(skin.numberOfUnits >= 1 && kidnies.numberOfUnits >= 1 && bones.numberOfUnits >= 1 && livers.numberOfUnits >= 1 && lungs.numberOfUnits >= 1 && eyes.numberOfUnits >= 1 && hearts.numberOfUnits >= 1 && brains.numberOfUnits >= 1)
        {
            skin.numberOfUnits--;
            kidnies.numberOfUnits--;
            livers.numberOfUnits--;
            bones.numberOfUnits--;
            lungs.numberOfUnits--;
            eyes.numberOfUnits--;
            hearts.numberOfUnits--;
            brains.numberOfUnits--;
            people++;
        }
    }

    public void BuyOrgan(Organ type)
    {
        if(cells >= type.pricePerUnit)
        {
            cells -= type.pricePerUnit;
            type.numberOfUnits++;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}

[Serializable]
public class Organ
{
    public int numberOfUnits;
    public int pricePerUnit;
    public int cellsPerTick;
    public int cellsPerClick;
    CellManager owner;
    public GenerationAlgorithms algorithm;
    public Text Information;
    public Button BuyOrgan;
    

    public Organ(CellManager cm, int price, int cpt)
    {
        owner = cm;
        pricePerUnit = price;
        cellsPerTick = cpt;
        algorithm = new GenerationAlgorithms();
        algorithm.cellManager = owner;
        algorithm.mOrgan = this;
        numberOfUnits = 1;
    }

    public void UpdateInformation()
    {
        BuyOrgan.GetComponentInChildren<Text>().text = "<b>Buy: </b>" + pricePerUnit;
        Information.text = "<b>Click: </b>" + cellsPerClick + "<b> || </b>" + "<b>Idle: </b>" + cellsPerTick;
    }
}