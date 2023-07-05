using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			double sum = 0;
			var length = checkpoints.Length;
			var res = new int[length];
            var preRes = new int [length];
			var pos = 1;
            MakePermutation(checkpoints, sum, res, preRes, pos);
			return res;
		}
		private static double  MakePermutation(Point[] checkpoints, double sum, int[]res, int[] preRes, int pos)
		{
			var segment = new int[pos];
			Array.Copy(preRes, segment, segment.Length);
            double preSum = PointExtensions.GetPathLength(checkpoints, segment);
            if (sum != 0 && preSum > sum)
			{
                return sum;
            }
			if (pos == res.Length && (sum > preSum || sum == 0))
			{
				sum = preSum;
				Array.Copy(preRes, res, res.Length);
				Console.WriteLine(sum);
				return sum;
			}
			for (int i = 1; i < checkpoints.Length; i++)
			{
                var index = Array.IndexOf(preRes, i, 0, pos);
                if (index == -1)
				{
					preRes[pos] = i;
                    sum = MakePermutation(checkpoints, sum, res, preRes, pos+1);
                }
			}
			return sum;
		}
	}
}