using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579041:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "근접 책장의 위력 +1";
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.card.card.GetSpec().Ranged == CardRange.Near)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }
}
