using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579000 : PassiveAbilityBase 
    {
        public override string debugDesc
        {
            get
            {
                return "자신에게 전투 시작 시 신속 1 부여, 매 막마다 신속 효과 1씩 증가, 최대 신속 6까지 가능";
            }
        }
        public override void OnWaveStart()
        {
            count = 1;
            foreach(BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
            {
                this.owner.passiveDetail.DestroyPassive(this);
            }
        }
        public override void OnRoundStart()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, count, base.owner);
            if (count < 6)
            {
                count++;
            }
        }
        private int count;
    }
}
