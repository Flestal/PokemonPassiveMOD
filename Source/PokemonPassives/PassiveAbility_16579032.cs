using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579032:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "자신에게 부여된 부식 수치3당 1만큼 주사위 위력 증가, 부식 수치만큼 피해량 증가";
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (this.owner.bufListDetail.GetActivatedBufList().Exists((BattleUnitBuf x) => x.bufType==KeywordBuf.Decay))
            {
                int stack = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay).stack;
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = Convert.ToInt32(stack/3),
                    dmg = stack
                });
            }
        }
    }
}
