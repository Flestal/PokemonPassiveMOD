using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579018:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "매 막마다 자신에게 불리한 버프가 걸려 있을 시 신속 1 부여";
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
            //speed = 0;
            if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.positiveType == BufPositiveType.Negative))
            {
                this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 1);
                //speed = 3;
            }
        }
        //public override buf
        /*public override int GetSpeedDiceAdder(int speedDiceResult)
        {
            return speed;
        }*/
        //private int speed;
    }
}
