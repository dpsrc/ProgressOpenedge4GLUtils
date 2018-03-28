using System.Text;
    
namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
    /// <summary>
    /// Encapsulates the Progress 4GL field information
    /// </summary>
    public class FieldInfo
    {
        private static readonly string _signatureDescription = "DESCRIPTION \"";
        private static readonly int _signatureDescriptionLen = _signatureDescription.Length;
        private readonly string _fieldDescription;

        public string FieldDescription { get => _fieldDescription; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fbieldDescription"></param>
        /// <exception cref="ArgumentException">if fieldName is null.</exception>
        public FieldInfo(StringBuilder sbFieldDescription)
        {
            _fieldDescription = Utils.AdjustNewLine(sbFieldDescription).ToString() ?? "";
        }

        /// <summary>
        /// Returns so called short description:
        /// the text from the
        /// DESCRIPTION "This is returned by this property"
        /// line.
        /// </summary>
        public string ShortFieldDescription
        {
            get
            {
                if (FieldDescription == null)
                    return "";

                string[] lines = FieldDescription.Split(new char[] { '\n' });
                foreach (string line in lines)
                {
                    string lineModified = line.Trim();
                    if (lineModified.StartsWith(_signatureDescription))
                        return lineModified.Split(new char[] { '\"' })[1];
                }

                return "";
            }
        }
    }
}
