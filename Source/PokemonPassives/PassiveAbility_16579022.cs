using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579022:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작 시 모든 적에게 허약 1 부여";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            List<BattleUnitModel> enemylist = BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy);
            foreach (BattleUnitModel enemy in enemylist)
            {
                enemy.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 1, null);
            }
        }
    }
}
