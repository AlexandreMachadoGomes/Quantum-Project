using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, ISignalOnPlayerDataSet
    {
        public struct Filter
        {
            public EntityRef Entity;
            public TopDownKCC* TopDownKCC;
            public Transform2D* Transform;
            public PlayerLink* PlayerLink;
        }

        public override void Update(Frame f, ref Filter filter)
        {

            var input = f.GetPlayerInput(filter.PlayerLink->Player);

            var inputVector = new FPVector2((FP)input->DirectionX / 10, (FP)input->DirectionY / 10);

            //that anticheat is kinda stupid

            filter.TopDownKCC->Move(f, filter.Entity, inputVector);



            if (input->Dash.WasPressed)
            {
                if (!filter.PlayerLink->IsDashing)
                {
                    filter.PlayerLink->IsDashing = true;
                    filter.TopDownKCC->MaxSpeed *= 5;
                    filter.TopDownKCC->Acceleration = filter.TopDownKCC->MaxSpeed;
                }

            }
            

            if (filter.PlayerLink->IsDashing)
            {
                if (filter.PlayerLink->totalDashEnergy > 0)
                    filter.PlayerLink->totalDashEnergy -= 1;
                else
                {
                    filter.PlayerLink->IsDashing = false;
                    filter.TopDownKCC->MaxSpeed /= 5;
                }
            }
            else if (filter.PlayerLink->totalDashEnergy < filter.PlayerLink->maxDashEnergy)
            {
                filter.PlayerLink->totalDashEnergy += 1;
            }
            

        }

        public void OnPlayerDataSet(Frame f, PlayerRef player)
        {

            var data = f.GetPlayerData(player);

            var prototypeEntity = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
            var createdEntity = f.Create(prototypeEntity);

            if (f.Unsafe.TryGetPointer<PlayerLink>(createdEntity, out var playerLink))
            {
                playerLink->Player = player;
                playerLink->maxDashEnergy = 50;
                playerLink->totalDashEnergy = playerLink->maxDashEnergy;
            }

            if (f.Unsafe.TryGetPointer<Transform2D>(createdEntity, out var transform))
            {
                transform->Position = GetSpawnPosition(player);
            }

            /// inits the TopDownKCC 
            if (f.Unsafe.TryGetPointer<TopDownKCC>(createdEntity, out var TPKCC))
            {
                var settings = f.FindAsset<TopDownKCCSettings>(TPKCC->Settings.Id);
                settings.Init(ref *TPKCC);
            }

            FPVector2 GetSpawnPosition(int playerNumber)
            {
                return new FPVector2(-4 + (playerNumber * 2), 0);
            }
        }
    }
}
