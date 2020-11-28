using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579043:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "공격 적중 시 상대의 내성이 내성, 면역이면 주사위 값만큼 추가 피해";
            }
        }
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            //base.OnSucceedAttack(behavior);
            if(behavior.card.target.Book.GetResistHP(behavior.Detail) == AtkResist.Resist|| behavior.card.target.Book.GetResistHP(behavior.Detail) == AtkResist.Immune)
            {
                behavior.card.target.TakeDamage(behavior.DiceResultValue, this.owner, true);
                behavior.card.target.TakeBreakDamage(behavior.DiceResultValue, this.owner, AtkResist.Normal);
            }
        }
    }
}
