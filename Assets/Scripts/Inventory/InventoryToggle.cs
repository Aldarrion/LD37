using UnityEngine;
using System.Collections;
using Fungus;

public class InventoryToggle : MonoBehaviour
{

    public Flowchart Chart;

    public void Pulse()
    {
        Chart.SendFungusMessage("ShakeToggle");
    }

}
