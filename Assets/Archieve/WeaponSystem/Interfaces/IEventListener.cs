using UnityEngine;

public interface IEventListener <TEvent>
{
    void OnEvent(TEvent eventArgs);
}
