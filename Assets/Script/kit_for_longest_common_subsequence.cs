using System;
using UnityEngine;
using System.Collections.Generic;

namespace CommonKit.Extend
{
    public static class StringCommon
    {
        public static int LongestCommonSubsequence(string lhs, string rhs)
        {
            int[,] TempList = new int[lhs.Length + 1, rhs.Length + 1];

            /* Following steps build L[m+1][n+1] 
            in bottom up fashion. Note
            that L[i][j] contains length of 
            LCS of X[0..i-1] and Y[0..j-1] */

            for (int i = 0; i <= lhs.Length; i++)
            {
                for (int j = 0; j <= rhs.Length; j++)
                {
                    if (i == 0 || j == 0)
                        TempList[i, j] = 0;
                    else if (lhs[i - 1] == rhs[j - 1])
                        TempList[i, j] = TempList[i - 1, j - 1] + 1;
                    else
                        TempList[i, j] = Math.Max(TempList[i - 1, j], TempList[i, j - 1]);
                }
            }

            return TempList[lhs.Length, rhs.Length];
        }
    }
}