using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using Logic.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var data = new GameEventData(gameObject.name, other.gameObject.GetComponent<PlayerController>());
        ServiceLocator.Find<EventManager>().Propagate(new ObjectTriggerEnterEvent(), data);
    }

    private void OnTriggerExit(Collider other)
    {
        var data = new GameEventData(gameObject.name, other.gameObject.GetComponent<PlayerController>());
        ServiceLocator.Find<EventManager>().Propagate(new ObjectTriggerExitEvent(), data);
    }
}
