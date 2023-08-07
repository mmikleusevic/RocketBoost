using System;
using System.ComponentModel;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public const string Friendly = "Friendly";
    public const string Finish = "Finish";
    public const string Fuel = "Fuel";

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case Friendly:
                Debug.Log("You bumped into a friendly unit.");
                break;
            case Finish:
                Debug.Log("You bumped into finish.");
                break;
            case Fuel:
                Debug.Log("You bumped into fuel.");
                break;
            default:
                Debug.Log("You blew up");
                break;
        }
    }
}
