namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
    /// <summary>
    /// Encapsulates the field information
    /// </summary>
    public class FieldInfo
    {
        private static readonly string _signatureDescription = "DESCRIPTION \"";
        private static readonly int _signatureDescriptionLen = _signatureDescription.Length;
        private string _fieldDescription = "";

        public string FieldDescription { get => _fieldDescription; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldDescription"></param>
        /// <exception cref="ArgumentException">if fieldName is null.</exception>
        public FieldInfo(string fieldDescription)
        {
            _fieldDescription = fieldDescription ?? "";
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
