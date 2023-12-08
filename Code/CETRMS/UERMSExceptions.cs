using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CETRMS
{
    [Serializable]

    public class CETRMSExceptions:Exception
    {
        public CETRMSExceptions() { }
        ///<summary>
        /// CETRMSExceptions are custom defined exceptions. Which can be utilized to return specific message.
        /// </summary>
        /// <param name="message">
        /// Message to be passed.
        /// </param>
        public CETRMSExceptions(string message) : base(String.Format("CETRMSException: {0}", message)) { }
    }
}