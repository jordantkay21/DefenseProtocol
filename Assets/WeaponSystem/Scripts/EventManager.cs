using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    /// <summary>
    /// A dictionary that maps each event type to a list of listeners (methods) that respond to that event. 
    /// The key is the event type, and the value is a list of delegates that are invoked when the event is published.
    /// </summary>
    private Dictionary<Type, List<Action<object>>> eventListeners = new Dictionary<Type, List<Action<object>>>();

    /// <summary>
    /// Allows a listener to subscribe to an event
    /// </summary>
    /// <typeparam name="TEvent">The event to be subscribed to</typeparam>
    /// <param name="listener">the delegate to be invoked when event is triggered</param>
    public void Subscribe<TEvent>(Action<TEvent> listener)
    {
        // gets the Type object corresponding to the generic type parameter TEvent
        var eventType = typeof(TEvent);

        //Checks if there are already listeners to the specified event type 
        if (!eventListeners.ContainsKey(eventType))
            //If there are no listeners registered for this event type, it creates a new list to hold them
            eventListeners[eventType] = new List<Action<object>>();

        //Adds provided listener to the list of listeners for this event Type
        //Wraps it in a lambda expression that casts the event data back to the appropriate type before invoking the listener
        eventListeners[eventType].Add(subscriber => listener((TEvent)subscriber));
        
        ///Lambda Expression Breakdown
        ///subscriber = a parameter of type 'object' because the dictionary is storing Action<object> delegates
        ///(TEvent)subscriber = the object is cast back to the event type TEvent
        ///         This is necessary because the listener expects a parameter of type TEvent
        ///listener((TEvent)subscriber) = After casting e back to TEvent, the listener is invoked with the casted data back

    }

    /// <summary>
    /// Allows a caller to unsubscribe from an event
    /// </summary>
    /// <typeparam name="TEvent">Event to be unsubscribed from</typeparam>
    /// <param name="listener">delegate requesting to unsubscribe</param>
    public void Unsubscribe<TEvent>(Action<TEvent> listener)
    {
        // gets the Type object corresponding to the generic type parameter TEvent
        var eventType = typeof(TEvent);

        //Checks if there are any registered listeners for the specified event type
        if (eventListeners.ContainsKey(eventType))
            //removes the listener from the list
            eventListeners[eventType].RemoveAll(subscriber => subscriber.Equals(listener));

        ///Lambda Expression Breakdown
        ///subscriber = a parameter of type object
        ///subscriber.Equals(listeners) = checks if the current delegate (subscriber) is equal to the listener delegate that was passed into the method
        ///if the condition evaluates to true, RemoveAll will remove the delegate from the list
    }


    /// <summary>
    /// Allows a caller to publish an event, notifying all registered listeners by passing them the event data
    /// </summary>
    /// <typeparam name="TEvent">Event type to be invoked</typeparam>
    /// <param name="eventArgs">Necessary event data</param>
    public void Publish<TEvent>(TEvent eventArgs)
    {
        // gets the Type object corresponding to the generic type parameter TEvent
        var eventType = typeof(TEvent);

        //Checks if there are any listeners registered for the event
        if (eventListeners.ContainsKey(eventType))
        {
            //If yes; iterate over each listener and invokes it passing the event data to each one
            foreach (var listener in eventListeners[eventType])
                listener(eventArgs);
        }
    }

}
