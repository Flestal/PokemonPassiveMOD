using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579025:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "적을 처치할 시 그 무대 동안 처치한 적의 수만큼 힘을 부여";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            killcount = 0;
        }
        public override void OnRoundStart()
        {
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, killcount);
        }
        public override void OnKill(BattleUnitModel target)
        {
            killcount++;
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
        }
        private int killcount;
    }
}
