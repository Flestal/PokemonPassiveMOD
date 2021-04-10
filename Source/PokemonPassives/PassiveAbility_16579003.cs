using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579003:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "매 막마다 모든 적에게 속박 1 부여, 공격 적중 시 다음 막에 속박 1 부여";
            }
        }
        public override void OnWaveStart()
        {
            /*foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }*/
        }
        public override void OnRoundStart()
        {
            List<BattleUnitModel> enemylist=BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy);
            foreach(BattleUnitModel enemy in enemylist)
            {
                enemy.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Binding, 1);
            }
        }
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Binding, 1);
        }
    }
}
