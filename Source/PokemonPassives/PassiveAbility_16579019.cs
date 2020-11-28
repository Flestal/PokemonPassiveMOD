using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579019:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작 시 속박 3 부여, 이후 매 막마다 부여하는 속박이 1씩 감소함";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            slowcnt = 3;
            this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Binding, slowcnt--);
        }
        public override void OnRoundStart()
        {
            if (slowcnt > 0)
            {
                this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Binding, slowcnt--);
            }
        }
        private int slowcnt;
    }
}
