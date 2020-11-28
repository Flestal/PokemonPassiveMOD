using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579047:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "상대의 가장 약한 속성으로 적중 시 주사위 값만큼 추가 피해";
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
            BattleUnitModel target = behavior.card.target;
            BehaviourDetail def = BehaviourDetail.Slash;
            if (target.Book.GetResistHP(def) > target.Book.GetResistHP(BehaviourDetail.Penetrate))
            {
                def = BehaviourDetail.Penetrate;
            }
            if (target.Book.GetResistHP(def) > target.Book.GetResistHP(BehaviourDetail.Hit))
            {
                def = BehaviourDetail.Hit;
            }

            if (behavior.Detail == def)
            {
                target.TakeDamage(behavior.DiceResultValue, this.owner, true);
                target.TakeBreakDamage(behavior.DiceResultValue, this.owner, target.Book.GetResistBP(behavior.Detail));
            }
        }
    }
}
