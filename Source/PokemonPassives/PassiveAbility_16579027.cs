using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579027:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "공격 받을 시 30%로 상대 주사위 1개 봉인";
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
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (atkDice != null)
            {
                int rand = r.Next(10);
                if (rand < 3)
                {
					BattleUnitBuf battleUnitBuf = atkDice.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is PassiveAbility_16579027.BattleUnitBuf_speedBreak);
					if (battleUnitBuf == null)
					{
						PassiveAbility_16579027.BattleUnitBuf_speedBreak speedBreak = new PassiveAbility_16579027.BattleUnitBuf_speedBreak();
						atkDice.card.owner.bufListDetail.AddBuf(speedBreak);
						speedBreak.Add();
						return;
					}
				}
            }
        }
        Random r = new Random(DateTime.Now.Millisecond);
		public class BattleUnitBuf_speedBreak : BattleUnitBuf
		{
			protected override string keywordId
			{
				get
				{
					return "Butterfly_Seal";
				}
			}

			public BattleUnitBuf_speedBreak()
			{
				this.stack = 0;
			}

			public override void OnRoundEnd()
			{
				base.OnRoundEnd();
				this.stack -= this.deleteThisTurn;
				if (this.stack <= 0)
				{
					this.Destroy();
				}
			}

			public override int SpeedDiceBreakedAdder()
			{
				return this.stack;
			}

			public override void OnRoundStart()
			{
				base.OnRoundStart();
				this.deleteThisTurn = this.addedThisTurn;
				this.addedThisTurn = 0;
			}

			public void Add()
			{
				this.stack++;
				this.addedThisTurn++;
				BattleCardTotalResult battleCardResultLog = this._owner.battleCardResultLog;
				if (battleCardResultLog != null)
				{
					battleCardResultLog.SetCreatureEffectSound("Creature/ButterFlyMan_Lock");
				}
				int num = this._owner.passiveDetail.SpeedDiceNumAdder() - this._owner.passiveDetail.SpeedDiceBreakAdder() - this._owner.bufListDetail.SpeedDiceBreakAdder() + this.addedThisTurn - 1;
				if (num >= 0 && num < this._owner.speedDiceResult.Count && this._owner.cardSlotDetail.cardAry[num].GetRemainingAbilityCount() > 0)
				{
					this._owner.cardSlotDetail.cardAry[num].DestroyDice(DiceMatch.AllDice, DiceUITiming.Start);
				}
			}

			private int addedThisTurn;

			private int deleteThisTurn;
		}
	}
}
