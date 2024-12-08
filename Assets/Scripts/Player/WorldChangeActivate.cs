using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChangeActivate : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    public void TeleportActivate()
    {
        player.GetComponent<DimensionChange>().ChangeMap();
    }
}
