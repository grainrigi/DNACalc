using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DNACalc {
    public class NoDupSamples : Samples {
        public NoDupSamples(bool[][] bs) : base(bs) {
        }
        public override BigInteger Calc(int pick) {
            Combinator c = new Combinator(data.Length, pick);
            BigInteger result = 0;
            ushort ctr = 0;
            int ctr2 = 0;

            pubResult = 0;

            do {
                ulong acc = 0;

                // 補完するか計算
                for (int i = 0; i < pick; i++) {
                    acc |= data[c.combination[i]];
                    if(acc == accFull) {
                        if(i == pick - 1) {
                            // 本当に下位の組み合わせを含んでいないか確認
                            for (int n = 0; n < pick - 1; n++) {
                                acc = 0;
                                for (int r = 0; r < pick; r++) {
                                    if(n == r) continue;
                                    acc |= data[c.combination[r]];
                                    if(acc == accFull) goto detected;
                                }
                            }
                            result++;
                        detected:;
                        } else {
                            var skip = c.SkipThis(i + 1);
                            pubResult += skip;
                            ctr2++;
                        }
                        break;
                    }
                }
                if(++ctr == 0) {
                    lock(this) {
                        pubResult += 65536 - ctr2;
                        ctr2 = 0;
                        if(cancel) return 0;
                    }
                }
            } while(c.Next());

            return result;
        }
    }
}
