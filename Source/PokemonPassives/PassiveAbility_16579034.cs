using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579034:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "모든 공격 주사위에 \'[적중] 자신과 상대에게 부식 1 부여\'가 추가됨";
            }
        }
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 1, this.owner);
            behavior.card.target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 1, this.owner);
        }
    }
}
