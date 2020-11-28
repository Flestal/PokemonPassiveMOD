using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579035:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "자신에게 부여된 화상 수치 5당 주사위 위력 1 증가, 화상에 의한 피해를 받지 않음";
            }
        }
        public override bool IsImmune(KeywordBuf buf)
        {
            return buf == KeywordBuf.Burn||buf==KeywordBuf.BurnBreak;
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (this.owner.bufListDetail.GetActivatedBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Burn))
            {
                int stack = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Burn).stack;
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = Convert.ToInt32(stack / 5)
                });
            }
        }
    }
}
