using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    public Button bodyButton;

    public long cells;
    public long people;

    public long totalCellPerTick;

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

        skin.canBuy = true;
    }

    bool canBuildPerson
    {
        get
        {
            return skin.numberOfUnits >= 10 && kidnies.numberOfUnits >= 10 && bones.numberOfUnits >= 10 &&
            livers.numberOfUnits >= 10 && lungs.numberOfUnits >= 10 && eyes.numberOfUnits >= 10 &&
            hearts.numberOfUnits >= 10 && brains.numberOfUnits >= 10 && cells >= 1000000;
        }
    }

    public void BuildPerson()
    {
        if(canBuildPerson)
        {
            UIStacking.CreateHuman.Invoke();
            cells -= 1000000;
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
            type.pricePerUnit = type.pricePerUnit + (int)(type.pricePerUnit * 0.35f);
            type.cellsPerTick = type.cellsPerClick * (int)(type.numberOfUnits / 3.0f);

            if (!type.isGenerating)
                StartCoroutine(type.Generate());
        }
    }

    public void BuySkin()
    {
        BuyOrgan(skin);

        if (skin.numberOfUnits >= 10)
            kidnies.canBuy = true;
        else
            kidnies.canBuy = false;
    }
    public void BuyKidney()
    {
        BuyOrgan(kidnies);

        if (kidnies.numberOfUnits >= 10)
            livers.canBuy = true;
        else
            livers.canBuy = false;
    }
    public void BuyLiver()
    {
        BuyOrgan(livers);

        if (livers.numberOfUnits >= 10)
            bones.canBuy = true;
        else
            bones.canBuy = false;
    }
    public void BuyBone()
    {
        BuyOrgan(bones);

        if (bones.numberOfUnits >= 10)
            lungs.canBuy = true;
        else
            lungs.canBuy = false;
    }
    public void BuyLung()
    {
        BuyOrgan(lungs);

        if (lungs.numberOfUnits >= 10)
            eyes.canBuy = true;
        else
            eyes.canBuy = false;
    }
    public void BuyEye()
    {
        BuyOrgan(eyes);

        if (eyes.numberOfUnits >= 10)
            hearts.canBuy = true;
        else
            hearts.canBuy = false;
    }
    public void BuyHeart()
    {
        BuyOrgan(hearts);

        if (hearts.numberOfUnits >= 10)
            brains.canBuy = true;
        else
            brains.canBuy = false;
    }
    public void BuyBrain()
    {
        BuyOrgan(brains);
    }

    void UpdateTotalTick()
    {
        totalCellPerTick = 0;
        totalCellPerTick += skin.numberOfUnits >= 5 ? skin.cellsPerTick : 0;
        totalCellPerTick += kidnies.numberOfUnits >= 5 ? kidnies.cellsPerTick : 0;
        totalCellPerTick += livers.numberOfUnits >= 5 ? livers.cellsPerTick : 0;
        totalCellPerTick += bones.numberOfUnits >= 5 ? bones.cellsPerTick : 0;
        totalCellPerTick += lungs.numberOfUnits >= 5 ? lungs.cellsPerTick : 0;
        totalCellPerTick += eyes.numberOfUnits >= 5 ? eyes.cellsPerTick : 0;
        totalCellPerTick += hearts.numberOfUnits >= 5 ? hearts.cellsPerTick : 0;
        totalCellPerTick += brains.numberOfUnits >= 5 ? brains.cellsPerTick : 0;
    }

    public float time = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            cells += 999999999;
        //if(time > 1)
        //{
        //    skin.algorithm.GenerationPerTick(skin);
        //    kidnies.algorithm.GenerationPerTick(kidnies);
        //    livers.algorithm.GenerationPerTick(livers);
        //    bones.algorithm.GenerationPerTick(bones);
        //    lungs.algorithm.GenerationPerTick(lungs);
        //    eyes.algorithm.GenerationPerTick(eyes);
        //    hearts.algorithm.GenerationPerTick(hearts);
        //    brains.algorithm.GenerationPerTick(brains);

        //    time = 0;
        //}
        //time += Time.deltaTime;

        skin.UpdateInformation();
        kidnies.UpdateInformation();
        livers.UpdateInformation();
        bones.UpdateInformation();
        lungs.UpdateInformation();
        eyes.UpdateInformation();
        hearts.UpdateInformation();
        brains.UpdateInformation();

        bodyButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.10f + (cells / 1000000.0f));

        if (bodyButton.GetComponent<Image>().color.a >= 1 && canBuildPerson)
            bodyButton.interactable = true;
        else
            bodyButton.interactable = false;

        UpdateTotalTick();

        Income.text = cells + "¢" + "\n" + totalCellPerTick + " ¢ps";
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
    public long numberOfUnits;
    public long pricePerUnit;
    [HideInInspector] public long cellsPerTick;
    public long cellsPerClick;
    [HideInInspector] public CellManager owner;
    [HideInInspector] public GenerationAlgorithms algorithm;
    public Text Information;
    public Button BuyOrgan;
    public Button GenCell;

    [HideInInspector] public bool canBuy;

    [HideInInspector] public bool isGenerating;

    public IEnumerator Generate()
    {
        isGenerating = true;
        while (numberOfUnits > 0)
        {
            algorithm.GenerationPerTick(this);
            yield return new WaitForSeconds(1.0f);
        }
        isGenerating = false;
    }

    public void UpdateInformation()
    {
        if (owner.cells < pricePerUnit || !canBuy)
            BuyOrgan.interactable = false;
        else
            BuyOrgan.interactable = true;

        if (numberOfUnits < 1)
            GenCell.interactable = false;
        else
            GenCell.interactable = true;

        BuyOrgan.GetComponentInChildren<Text>().text = "<b>Buy: </b>" + pricePerUnit + "¢";

        if (numberOfUnits == 1)
            GenCell.interactable = true;

        long perTic = 0;

        if (numberOfUnits >= 5)
            perTic = cellsPerTick;

        Information.text = numberOfUnits + "<b> X </b>" + cellsPerClick + "¢<b> || </b>" + perTic + " ¢ps";
    }

    public void GenerateCells()
    {
        owner.cells += cellsPerClick * (numberOfUnits * (1 + owner.people));
    }
}