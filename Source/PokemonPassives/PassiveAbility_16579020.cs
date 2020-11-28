using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579020:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작 시 힘 - 허약, 인내 - 무장해제, 보호 - 취약, 신속 - 속박으로 바뀌어서 부여됨";
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
            try
            {
                List<KeywordBuf> buflist = new List<KeywordBuf>();
                List<int> bufstack = new List<int>();
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Strength))
                {
                    buflist.Add(KeywordBuf.Weak);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Strength).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Strength));
                }
                if(this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Weak))
                {
                    buflist.Add(KeywordBuf.Strength);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Weak).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Weak));
                }
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Endurance))
                {
                    buflist.Add(KeywordBuf.Disarm);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Endurance).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Endurance));
                }
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Disarm))
                {
                    buflist.Add(KeywordBuf.Endurance);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Disarm).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Disarm));
                }
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Protection))
                {
                    buflist.Add(KeywordBuf.Vulnerable);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Protection).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Protection));
                }
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Vulnerable))
                {
                    buflist.Add(KeywordBuf.Protection);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Vulnerable).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Vulnerable));
                }
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Quickness))
                {
                    buflist.Add(KeywordBuf.Binding);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Quickness).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Quickness));
                }
                if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Binding))
                {
                    buflist.Add(KeywordBuf.Quickness);
                    bufstack.Add(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Binding).stack);
                    this.owner.bufListDetail.GetReadyBufList().Remove(this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Binding));
                }


                for (int i = 0; i < buflist.Count; i++)
                {
                    this.owner.bufListDetail.AddKeywordBufByEtc(buflist[i], bufstack[i]);
                }
            }
            catch(Exception ex)
            {
                File.WriteAllText(Application.dataPath + "/BaseMods/PokemonPassiveMOD/error.txt", ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
