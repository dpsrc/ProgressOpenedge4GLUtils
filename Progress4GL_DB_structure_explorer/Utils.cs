using System;
using System.Text;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{

    internal static class Utils
    {
        public static StringBuilder AdjustNewLine(StringBuilder sb)
        {
            if (sb == null)
                return null;

            if (Environment.NewLine != "\n")
                sb = sb.Replace(Environment.NewLine, "\n");

            return sb;
        }
    }

}
