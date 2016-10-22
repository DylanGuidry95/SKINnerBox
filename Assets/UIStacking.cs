using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStacking : MonoBehaviour
{
    public Transform SpawnAnchor1;
    public Transform SpawnAnchor2;
    public GameObject mHuman;

    [ContextMenu("Test")]
    void SpawnHuman()
    {
        GameObject human = Instantiate(mHuman);
        human.transform.SetParent(this.transform);
        human.transform.position =
            new Vector3(Random.Range(SpawnAnchor1.position.x, SpawnAnchor2.position.x), transform.position.y, transform.position.z);
    }
}
