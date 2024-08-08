using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe class ButtonManagementSystem : SystemMainThreadFilter<ButtonManagementSystem.Filter>, ISignalOnTriggerEnter2D, ISignalOnTriggerExit2D
    {
        public struct Filter 
        {
            public EntityRef Entity;
            public ButtonInfo* ButtonInfo;
        }


        public override void Update(Frame f, ref Filter filter)
        {
            if (filter.ButtonInfo->HasTimer && filter.ButtonInfo->ButtonTimer.IsRunning && filter.ButtonInfo->ButtonTimer.Expired(f))
            { 
                filter.ButtonInfo->ButtonState = ButtonState.Iddle;
            }

        }

        // resets button to iddle state
        public void OnTriggerExit2D(Frame f, ExitInfo2D info)
        {

            var entityA = info.Entity;
            var entityB = info.Other;

            
            if (f.Unsafe.TryGetPointer<ButtonInfo>(entityB, out var buttonInfo))
            {

                buttonInfo->NumberPlayersOnButton -= 1;

                // button standart behaviour (shared by all button types)

                //does nothing when player exits if it uses Timer do deactivate
                if (buttonInfo->HasTimer)
                    return;

                
                if (buttonInfo->ButtonState == ButtonState.Clicking )
                {
                    if (!buttonInfo->NeedEveryoneOffButtonToIddle)
                    {
                        buttonInfo->ButtonState = ButtonState.Iddle;
                    }
                    else if (buttonInfo->NumberPlayersOnButton == 0)
                        buttonInfo->ButtonState = ButtonState.Iddle;
             
                }

                // switch case that switches to different function depending on type of button (in order to handle multiple types of buttons with different behaviours)
                switch (buttonInfo->Type)
                {
                    case ButtonTypes.SpeedUpButton:
                        OnPlayerExitTriggerSpeedUpButton(f, entityA, buttonInfo);
                        break;
                    case ButtonTypes.SpawnFoodButton:
                        OnPlayerExitTriggerSpawnFood(f);
                        break;
                }
            }
        }

        // there wont be any timer to reset the button to a pushable state (idle), it will instead happen whenever the player exits the collider for the button
        public void OnTriggerEnter2D(Frame f, TriggerInfo2D info)
        {

            var entityA = info.Entity;
            var entityB = info.Other;

            
            if (f.Unsafe.TryGetPointer<ButtonInfo>(entityB, out var buttonInfo))
            {

                buttonInfo->NumberPlayersOnButton += 1;


                // button standart behaviour (shared by all button types)

                if (buttonInfo->ButtonState == ButtonState.Clicking)
                    return;

                buttonInfo->ButtonState = ButtonState.Clicking;

                buttonInfo->ToggleActive = !buttonInfo->ToggleActive;

                // creates timer if button has Timer functionality
                if (buttonInfo->HasTimer)
                    buttonInfo->ButtonTimer = FrameTimer.CreateFromSeconds(f, buttonInfo->TotalTimerTime);


                // switch case that switches to different function depending on type of button (in order to handle multiple types of buttons with different behaviours)
                switch (buttonInfo->Type)
                {
                    case ButtonTypes.SpeedUpButton:
                        OnPlayerEnterTriggerSpeedUpButton(f, entityA, buttonInfo);
                        break;
                    case ButtonTypes.SpawnFoodButton:
                        OnPlayerEnterTriggerSpawnFood(f, entityB);
                        break;
                }
            }
        }

        private void OnPlayerEnterTriggerSpeedUpButton(Frame f, EntityRef playerEntity, ButtonInfo* buttonInfo)
        {   
            if (f.Unsafe.TryGetPointer<TopDownKCC>(playerEntity, out var TPKCC))
            {
                
            }
        }

        private void OnPlayerExitTriggerSpeedUpButton(Frame f, EntityRef playerEntity, ButtonInfo* buttonInfo)
        {
            if (f.Unsafe.TryGetPointer<TopDownKCC>(playerEntity, out var TPKCC))
            {
                
            }
        }

        private void OnPlayerEnterTriggerSpawnFood(Frame f, EntityRef buttonRef)
        {
            var prototype = f.FindAsset<EntityPrototype>("FoodEntityPrototype");

            // Define the spawn position and rotation
            FPVector2 spawnPosition = new FPVector2(0, 0);
            FPQuaternion spawnRotation = FPQuaternion.Identity;

            // Create the entity from the prototype
            var createdEntity = f.Create(prototype);

            if (f.Unsafe.TryGetPointer<Transform2D>(createdEntity, out var transform) && f.Unsafe.TryGetPointer<Transform2D>(buttonRef, out var buttonTransform))
            {
                transform->Position = buttonTransform->Position + new FPVector2(5, 0);
            }
        }

        private void OnPlayerExitTriggerSpawnFood(Frame f) { }


    }
}
