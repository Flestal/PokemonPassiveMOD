using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579012:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "매 막마다 자신에게 힘, 허약, 인내, 무장해제, 보호, 취약, 신속, 속박 중 3가지가 1~3 부여";
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
            run = 0;
            randBuff = getRandom(3,0,Randy.Count);
            for(int i = 0; i <= 3; i++)
            {
                base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(Randy[randBuff[i]], UnityEngine.Random.Range(1,4),base.owner);
            }
        }
        private List<KeywordBuf> Randy = new List<KeywordBuf>(new KeywordBuf[] {KeywordBuf.Strength,KeywordBuf.Weak,KeywordBuf.Endurance,KeywordBuf.Disarm,KeywordBuf.Protection,KeywordBuf.Vulnerable,KeywordBuf.Quickness,KeywordBuf.Binding });
        private int[] randBuff;
        private int temp;
        private int run;
        public int[] getRandom(int length,int min,int max)
        {
            int[] randArr = new int[length];
            bool isSame;
            for(int i = 0; i < length; i++)
            {
                while (true)
                {
                    randArr[i] = UnityEngine.Random.Range(min, max);
                    isSame = false;
                    for(int j = 0; j < i; j++)
                    {
                        if (randArr[j] == randArr[i])
                        {
                            isSame = true;
                            break;
                        }
                    }
                    if (!isSame)
                    {
                        break;
                    }
                }
            }
            return randArr;
        }
    }
}
