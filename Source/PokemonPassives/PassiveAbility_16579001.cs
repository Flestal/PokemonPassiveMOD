using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579001:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "자신이 행동을 했으면 다음 막 동안 행동 불가";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
        }
        public override void OnRoundEnd()
        {
            if (!(this.owner.cardHistory.GetCurrentRoundCardList(Singleton<StageController>.Instance.RoundTurn).Count <= 0))
            {
                base.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Stun, 1, null);
            }
        }
    }
}
