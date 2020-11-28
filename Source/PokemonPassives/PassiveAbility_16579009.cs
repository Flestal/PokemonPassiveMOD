using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579009:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대에서 마이너스 보유자 숫자만큼 자신의 주사위 위력 증가";
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
            minuscnt = 0;
            List<BattleUnitModel> alivelist = BattleObjectManager.instance.GetAliveList();
            foreach (BattleUnitModel target in alivelist)
            {
                if (target == this.owner)
                {
                    continue;
                }
                if (target != null && target.passiveDetail.HasPassive<PassiveAbility_16579008>())
                {
                    minuscnt++;
                }
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = minuscnt
            });
        }
        private int minuscnt;
    }
}
