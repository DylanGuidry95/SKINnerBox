using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class HumanCreateEvents : UnityEvent
{

}

public class UIStacking : MonoBehaviour
{
    public Transform SpawnAnchor1;
    public Transform SpawnAnchor2;
    public GameObject mHuman;

    void Start()
    {
        CreateHuman = new HumanCreateEvents();
        CreateHuman.AddListener(SpawnHuman);
    }

    public static HumanCreateEvents CreateHuman;

    public void SpawnHuman()
    {
        GameObject human = Instantiate(mHuman);
        human.transform.SetParent(this.transform);
        human.transform.localPosition =
            new Vector3(Random.Range(SpawnAnchor1.localPosition.x, SpawnAnchor2.localPosition.x), SpawnAnchor1.transform.localPosition.y, 0);
    }
}
