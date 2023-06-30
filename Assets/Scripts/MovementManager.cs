using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Queue<IEnumerator> queue = new();

    private void Awake()
    {
        Global.moveManager = this;
    }

    public void Move(IEnumerator order)
    {
        queue.Enqueue(order);
    }

    public void Execute()
    {
        for (int i = 0; i < 10; i++)
        {
            int queueSize = queue.Count;
            for (int j = 0; j < queueSize; j++)
            {
                IEnumerator temp = queue.Dequeue();
                temp.MoveNext();
                queue.Enqueue(temp);
            }
            Physics2D.SyncTransforms();
        }
        queue.Clear();
    }

    private void LateUpdate()
    {
        if (queue.Count > 0)
        {
            Execute();
        }
    }
}
