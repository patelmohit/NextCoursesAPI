using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCourses.Models
{
    /// <summary>
    /// This class represents the metadata which is attached to each response of the UW Api. Not currently used in this application.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// The number of requests send to the endpoint
        /// </summary>
        public int requests;
        /// <summary>
        /// The timestamp of the response
        /// </summary>
        public int timestamp;
        /// <summary>
        /// The HTTP status of the response
        /// </summary>
        public int status;
        /// <summary>
        /// A message associated with the status
        /// </summary>
        public string message;
        /// <summary>
        /// The method id of the response
        /// </summary>
        public int method_id;
    }

}