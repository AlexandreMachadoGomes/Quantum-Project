using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quantum;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe class MassSystem : SystemMainThreadFilter<MassSystem.Filter>, ISignalOnTriggerEnter2D
    {

        public struct Filter
        {
            public EntityRef Entity;
            public Transform2D* Transform;
            public MassData* MassData;
        }


        public void OnTriggerEnter2D(Frame f, TriggerInfo2D info)
        {
            var entityA = info.Entity;
            var entityB = info.Other;


            if (f.Unsafe.TryGetPointer<MassData>(entityB, out var foodMassInfo) && f.Unsafe.TryGetPointer<MassData>(entityA, out var playerMassInfo)
                                                                                && f.Unsafe.TryGetPointer<PhysicsCollider2D>(entityA, out var playerPhysics2D))
            {
                playerPhysics2D->Shape = PhysicsS
                playerMassInfo->ammountMass += foodMassInfo->ammountMass;
                f.Destroy(entityB);
            }
        }

        public override void Update(Frame f, ref Filter filter)
        {
            
        }


    }
}
