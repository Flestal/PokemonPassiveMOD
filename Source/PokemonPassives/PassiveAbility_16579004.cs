using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579004:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "상태이상에 걸려 있을 시 주사위 피해량 x1.5";
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
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            if (this.owner.bufListDetail.GetActivatedBufList().Exists((BattleUnitBuf x) => x.positiveType == BufPositiveType.Negative))
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    dmg = Convert.ToInt32(behavior.DiceResultValue * 0.5f)
                });
            }
        }
    }
}
