using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579008:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대에서 플러스 보유자 숫자만큼 자신의 주사위 피해량 증가";
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
        public override void OnRoundStart()
        {
            pluscnt = 0;//매 막 시작시 플러스 보유자 수 초기화
            List<BattleUnitModel> alivelist = BattleObjectManager.instance.GetAliveList();
            foreach(BattleUnitModel target in alivelist)
            {
                if (target == this.owner)
                {
                    continue;
                }
                if (target != null && target.passiveDetail.HasPassive<PassiveAbility_16579009>())
                {
                    pluscnt++;
                }
            }
        }
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = pluscnt
            });
        }
        private int pluscnt;
    }
}
