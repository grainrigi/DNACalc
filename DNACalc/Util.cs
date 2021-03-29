﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DNACalc {
    public class Util {
        // bool配列をlongに変換
        public static ulong bs2ulong(bool[] bs, int x) {
            ulong result = 0;
            for (int i = 0; i < x; i++) {
                if(i >= bs.Length) break;
                result |= (ulong)(bs[i] ? 1 : 0) << (x - i - 1);
            }
            return result;
        }

        public static bool[][] csv2bs(string csv) {
            var ls = System.Text.RegularExpressions.Regex.Split(csv, "\r\n|\r|\n").Where(v => v.Length > 0);
            return ls.Select(l => l.Split(',').Select(v => v.Length > 0).ToArray()).ToArray();
        }

        public static BigInteger nCr(int n, int r) {
            return combinations[n, r];
        }

        public static BigInteger nCrRaw(int n, int r) {
            BigInteger result = 1;
            if(r > (n - r)) r = n - r;
            for (int i = 0; i < r; i++) {
                result *= n - i;
            }
            for (int i = 0; i < r; i++) {
                result /= (i + 1);
            }
            return result;
        }

        public static void CacheCombinations(int _maxN) {
            maxN = _maxN;
            int maxR = maxN;
            combinations = new BigInteger[maxN + 1, maxR + 1];
            for (int n = 1; n <= maxN; n++) {
                for (int r = 0; r <= maxR && r <= n; r++) {
                    combinations[n, r] = nCrRaw(n, r);
                }
            }
        }

        private static BigInteger[,] combinations;
        private static int maxN;
    }
}
