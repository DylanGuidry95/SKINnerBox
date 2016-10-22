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
        skin.owner = this;
        kidnies.owner = this;
        livers.owner = this;
        bones.owner = this;
        lungs.owner = this;
        eyes.owner = this;
        hearts.owner = this;
        brains.owner = this;
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
            type.pricePerUnit = type.pricePerUnit + (type.numberOfUnits * 2);
            type.cellsPerTick = type.cellsPerClick * (type.numberOfUnits / 10);
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

    public float time = 0;
    void Update()
    {
        if(time > 1)
        {
            skin.algorithm.GenerationPerTick(skin);
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
    public int cellsPerTick;
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
        BuyOrgan.GetComponentInChildren<Text>().text = "<b>Buy: </b>" + pricePerUnit;
        if (numberOfUnits == 1)
            GenCell.interactable = true; 
        Information.text = numberOfUnits + "<b> X </b>" + cellsPerClick + "<b> || </b>" + cellsPerTick + " (¢ps)";
    }

    public void GenerateCells()
    {
        owner.cells += cellsPerClick * (numberOfUnits * (1 + owner.people));
    }
}