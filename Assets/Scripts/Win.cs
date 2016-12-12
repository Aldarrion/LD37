using UnityEngine;
using System.Collections;
using Fungus;

public class Win : MonoBehaviour
{
    public Flowchart FlowChart;

    BoxCollider2D Coll;
    public BoxCollider2D Santa;

    bool Finished;

    void Awake()
    {
        Coll = GetComponent<BoxCollider2D>();
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("Triggered");
    //    if (other.GetComponent<Character>() != null)
    //    {
    //        FlowChart.SendFungusMessage("Win");
    //    }
    //}

    void Update()
    {
        if (Coll.bounds.Intersects(Santa.bounds) && !Finished)
        {
            Finished = true;
            FlowChart.SendFungusMessage("Win");
        }
    }
}
