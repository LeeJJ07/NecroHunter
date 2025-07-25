using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviorTree tree;
    private void Start()
    {
        tree = tree.Clone();
        tree.Bind(/*GetComponent<AiAgent>()*/);
    }

    private void Update()
    {
        tree.Update();
    }
}
