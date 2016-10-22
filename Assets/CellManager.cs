using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    public int cells;
    public int people;

    public int totalCellPerTick;

    public Text Income;

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
        skin.owner = this;
        kidnies.owner = this;
        livers.owner = this;
        bones.owner = this;
        lungs.owner = this;
        eyes.owner = this;
        hearts.owner = this;
        brains.owner = this;
    }


    public void BuildPerson()
    {
        if(skin.numberOfUnits >= 10 && kidnies.numberOfUnits >= 10 && bones.numberOfUnits >= 10 && livers.numberOfUnits >= 10 && lungs.numberOfUnits >= 10 && eyes.numberOfUnits >= 10 && hearts.numberOfUnits >= 10 && brains.numberOfUnits >= 10)
        {
            skin.numberOfUnits-=10;
            kidnies.numberOfUnits-=10;
            livers.numberOfUnits-=10;
            bones.numberOfUnits-=10;
            lungs.numberOfUnits-=10;
            eyes.numberOfUnits-=10;
            hearts.numberOfUnits-=10;
            brains.numberOfUnits-=10;
            people++;
        }
    }

    public void BuyOrgan(Organ type)
    {
        if(cells >= type.pricePerUnit)
        {
            cells -= type.pricePerUnit;
            type.numberOfUnits++;
            type.pricePerUnit = type.pricePerUnit + (type.numberOfUnits * 2);
            type.cellsPerTick = type.cellsPerClick * (type.numberOfUnits / 10);

            UpdateTotalTick();
        }
    }

    public void BuySkin()
    {
        BuyOrgan(skin);
    }
    public void BuyKidney()
    {
        BuyOrgan(kidnies);
    }
    public void BuyLiver()
    {
        BuyOrgan(livers);
    }
    public void BuyBone()
    {
        BuyOrgan(bones);
    }
    public void BuyLung()
    {
        BuyOrgan(lungs);
    }
    public void BuyEye()
    {
        BuyOrgan(eyes);
    }
    public void BuyHeart()
    {
        BuyOrgan(hearts);
    }
    public void BuyBrain()
    {
        BuyOrgan(brains);
    }

    void UpdateTotalTick()
    {
        totalCellPerTick =
            skin.cellsPerTick +
            kidnies.cellsPerTick +
            livers.cellsPerTick +
            bones.cellsPerTick +
            lungs.cellsPerTick +
            eyes.cellsPerTick +
            hearts.cellsPerTick +
            brains.cellsPerTick;
    }

    public float time = 0;
    void Update()
    {
        if(time > 1)
        {
            skin.algorithm.GenerationPerTick(skin);
            kidnies.algorithm.GenerationPerTick(kidnies);
            livers.algorithm.GenerationPerTick(livers);
            bones.algorithm.GenerationPerTick(bones);
            lungs.algorithm.GenerationPerTick(lungs);
            eyes.algorithm.GenerationPerTick(eyes);
            hearts.algorithm.GenerationPerTick(hearts);
            brains.algorithm.GenerationPerTick(brains);

            time = 0;
        }
        time += Time.deltaTime;

        skin.UpdateInformation();
        kidnies.UpdateInformation();
        livers.UpdateInformation();
        bones.UpdateInformation();
        lungs.UpdateInformation();
        eyes.UpdateInformation();
        hearts.UpdateInformation();
        brains.UpdateInformation();

        Income.text = cells + "¢ @ " + totalCellPerTick + " ¢ps";
    }

    public void GenCell(string organ)
    {
        switch(organ)
        {
            case "Skin":
                skin.GenerateCells();
                break;
            case "Kidney":
                kidnies.GenerateCells();
                break;
            case "Liver":
                livers.GenerateCells();
                break;
            case "Bone":
                bones.GenerateCells();
                break;
            case "Lung":
                lungs.GenerateCells();
                break;
            case "Eye":
                eyes.GenerateCells();
                break;
            case "Heart":
                hearts.GenerateCells();
                break;
            case "Brain":
                brains.GenerateCells();
                break;
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
    [HideInInspector] public int cellsPerTick;
    public int cellsPerClick;
    public CellManager owner;
    public GenerationAlgorithms algorithm;
    public Text Information;
    public Button BuyOrgan;
    public Button GenCell;
    

    //public Organ(CellManager cm, int price, int cpt)
    //{
    //    owner = cm;
    //    pricePerUnit = price;
    //    cellsPerTick = cpt;
    //    algorithm = new GenerationAlgorithms();
    //    algorithm.cellManager = owner;
    //    numberOfUnits = 1;
    //}

    public void UpdateInformation()
    {
        if (owner.cells < pricePerUnit)
            BuyOrgan.interactable = false;
        else
            BuyOrgan.interactable = true;
        BuyOrgan.GetComponentInChildren<Text>().text = "<b>Buy: </b>" + pricePerUnit + "¢";
        if (numberOfUnits == 1)
            GenCell.interactable = true; 
        Information.text = numberOfUnits + "<b> X </b>" + cellsPerClick + "<b> || </b>" + cellsPerTick + " ¢ps";
    }

    public void GenerateCells()
    {
        owner.cells += cellsPerClick * (numberOfUnits * (1 + owner.people));
    }
}