using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579033:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "막 종료 시 부식에 의한 피해를 받지 않고 그 수치만큼 회복함";
            }
        }
        public override bool IsImmune(KeywordBuf buf)
        {
            return buf == KeywordBuf.Decay;
        }
        public override void OnRoundEnd()
        {
            int stack =0;
            if (this.owner.bufListDetail.GetActivatedBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Decay))
            {
                this.owner.RecoverHP(this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay).stack);
                SingletonBehavior<Sound.SoundEffectManager>.Instance.PlayClip("Creature/Angry_Decay", false, 1f, null);
            }
        }
    }
}
