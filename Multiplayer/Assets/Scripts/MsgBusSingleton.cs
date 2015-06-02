using UnityEngine;
using System.Collections;

public static class Messenger
{
    private static MessageBus ThisBus;

    public static MessageBus Bus
    {
        get
        {
            if( ThisBus == null )
                ThisBus = new MessageBus();

            return ThisBus;
        }
    }
}
