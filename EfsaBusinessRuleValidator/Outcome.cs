using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfsaBusinessRuleValidator
{
    /// <summary>
    /// Outcome
    /// </summary>
    public class Outcome
    {
        /// <summary>
        /// Passed
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Lastupdate
        /// </summary>
        public string Lastupdate { get; set; }

        /// <summary>
        /// Values
        /// </summary>
        public List<Tuple<string, string>> Values { get; set; } = new List<Tuple<string, string>>();

    }
}
