using System;
using System.Collections.Generic;

namespace GameComp.Utilities {
    [Serializable]
    public struct StringIntPair {
        public string key;
        public int val;
    }

    public static class StaticUtilities {
        public static int? GetValOfStringIntPair(List<StringIntPair> x, string key) {
            int? retval = null;

            for (var i = 0; i < x.Count; i++) {
                if (x[i].key == key) { retval = x[i].val; break; }
            }

            return retval;
        }
    }
}