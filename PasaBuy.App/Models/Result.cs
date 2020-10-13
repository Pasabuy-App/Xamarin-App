namespace PasaBuy.App.Models
{
    /// <summary>
    /// Rest Standard result object.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Status of the request.
        /// </summary>
        public string status = string.Empty;

        public bool Status
        {
            get
            {
                return status == "success" ? true : false;
            }
        }

        /// <summary>
        /// Expect only a string data.
        /// </summary>
        public string data = string.Empty;

        /// <summary>
        /// Null if status is success
        /// </summary>
        public string message = string.Empty;
    }
}
