using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class FoodSystem : SystemMainThreadFilter<FoodSystem.Filter>, ISignalOnTriggerEnter2D
    {

        public struct Filter
        {
            public EntityRef Entity;
            public Transform2D* Transform;
            public FoodData* FoodData;
        }


        public void OnTriggerEnter2D(Frame f, TriggerInfo2D info)
        {
            var entityA = info.Entity;
            var entityB = info.Other;


            if (f.Unsafe.TryGetPointer<MassData>(entityB, out var foodMassInfo) && f.Unsafe.TryGetPointer<MassData>(entityA, out var playerMassInfo))
            {
                playerMassInfo->ammountMass += foodMassInfo->ammountMass;
                f.Destroy(entityB);
            }
        }

        public override void Update(Frame f, ref Filter filter)
        {
            throw new NotImplementedException();
        }
    }
}
