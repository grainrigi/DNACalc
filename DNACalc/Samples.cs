using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DNACalc {
    public class Samples {
        private ulong[] data;
        private int length;
        private ulong accFull;
        private CombTree tree = new CombTree();

        public BigInteger pubResult;
        public bool cancel = false;

        public int Width { get => length; }
        public int Height { get => data.Length; }

        public Samples(bool[][] vs) {
            // サイズを決定
            var x = vs.Select(v => v.Length).Max();
            var y = vs.Length;

            if(x > 64) throw new Exception("64項目以上のデータは使えません。");
            if(y > 255) throw new Exception("256サンプル以上のデータは使えません。");

            // 変換
            data = vs.Select(v => Util.bs2ulong(v, x)).ToArray();
            length = x;

            for (int i = 0; i < length; i++) {
                accFull |= (ulong)1 << i;
            }
        }

        public BigInteger Calc(int pick) {
            Combinator c = new Combinator(data.Length, pick);
            BigInteger result = 0;
            ushort ctr = 0;

            do {
                ulong acc = 0;

                // 補完するか計算
                for (int i = 0; i < pick; i++) {
                    acc |= data[c.combination[i]];
                    if(acc == accFull) {
                        if(i == pick - 1) {
                            result++;
                        } else {
                            var skip = c.SkipThis(i + 1);
                            result += skip;
                        }
                        break;
                    }
                }
                if(++ctr == 0) {
                    lock(this) {
                        pubResult = result;
                        if(cancel) return 0;
                    }
                }
            } while(c.Next());

            return result;
        }

        public void OptimzeOrder() {
            int[] scores = new int[Height];
            Combinator c = new Combinator(Height, 3);

            do {
                ulong acc = 0;

                // 補完するか計算
                for (int i = 0; i < 3; i++) {
                    acc |= data[c.combination[i]];
                    if(acc == accFull) {
                        break;
                    }
                }

                int score = 0;
                for (int b = 0; b < Width; b++) {
                    if((acc & ((ulong)1 << b)) != 0) score++;
                }
                for (int i = 0; i < 3; i++) {
                    scores[c.combination[i]] += score;
                }
            } while(c.Next());

            var sorter = new int[Height][];
            for (int i = 0; i < Height; i++) {
                sorter[i] = new int[2] { i, scores[i] };
            }

            int idx = 0;
            ulong[] ndata = new ulong[Height];
            foreach(var s in sorter.OrderByDescending(v => v[1])) {
                ndata[idx] = data[s[0]];
                idx++;
            }

            data = ndata;
        }
    }
}
