using Quantum;
using UnityEngine;

public unsafe class MaterialRendererUpdater : MonoBehaviour
{
    private EntityView entityView;
    private MeshRenderer meshRenderer;

    public Material[] materials; // Array of materials to choose from based on ID
    public ButtonState currentButtonState = ButtonState.Iddle; 

    void Awake()
    {
        entityView = GetComponent<EntityView>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        
        
        var entityRef = entityView.EntityRef;
        var frame = QuantumRunner.Default.Game.Frames.Predicted;

        if (frame == null || entityRef == null)
            return;

        if (frame.Exists(entityRef))
        {
            var buttonInfo = frame.Unsafe.GetPointer<ButtonInfo>(entityRef);


            if (buttonInfo != null)
            {
                ButtonState buttonState = buttonInfo->ButtonState;

                
                // so it doesnt keep updating the material to the same material

                /*if (currentButtonState == buttonState)
                    return;
                else
                    currentButtonState = buttonState;*/

                


                switch (buttonState)
                {
                    case ButtonState.Iddle:
                        meshRenderer.sharedMaterial = materials[0];
                        break;
                    case ButtonState.Clicking:
                        meshRenderer.sharedMaterial = materials[1];
                        break;
                }

            }
        }
    }
}