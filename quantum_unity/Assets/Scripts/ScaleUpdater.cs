using Quantum;
using UnityEngine;

public unsafe class ScaleUpdater : MonoBehaviour
{
    private EntityView entityView;
    public float playerMass = 1;

    void Awake()
    {
        entityView = GetComponent<EntityView>();
    }

    void Update()
    {
        var entityRef = entityView.EntityRef;
        var frame = QuantumRunner.Default.Game.Frames.Predicted;

        if (frame == null || entityRef == null)
            return;

        if (frame.Exists(entityRef))
        {
            var massData = frame.Unsafe.GetPointer<MassData>(entityRef);
            float newScaleFactor = (float)(massData->ammountMass) / 1000f;
            if (massData != null)
            {
                Debug.Log(newScaleFactor);
                transform.localScale = new Vector3(newScaleFactor, newScaleFactor, newScaleFactor);
                playerMass = newScaleFactor;
            }
        }
    }
}