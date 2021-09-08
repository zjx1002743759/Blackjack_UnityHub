namespace Blackjack_UnityHub.Modle
{
    using System.Collections.Generic;

    public class UnityVersion
    {
        /// <summary>
        /// Gets or sets unity.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets webPath.
        /// </summary>
        public string WebPath { get; set; }

        /// <summary>
        /// Gets or sets veriosnCodes.
        /// </summary>
        public List<UnityVersion> VeriosnCodes { get; set; }
    }
}
