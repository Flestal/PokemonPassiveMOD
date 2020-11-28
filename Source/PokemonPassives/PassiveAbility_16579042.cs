using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579042:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "체력이 50% 이하가 되면 모든 주사위 값이 1로 고정됨";
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (this.owner.hp<=(this.owner.MaxHp*0.5f))
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = -9999
                });
            }
        }
    }
}
