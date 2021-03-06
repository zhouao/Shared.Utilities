﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	public static class DateTimeExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(DateTimeExtensions));

		#endregion

        #region Private static constants

        private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0);

        #endregion

        #region Public static methods

        /// <summary>
		/// Given a DateTime it will move the time component for that day
		/// to 1 second before the end of the day.
		/// </summary>
		public static DateTime MoveToEndOfDay(this DateTime dt)
		{
			_logger.DebugMethodCalled(dt);

			return dt.Date.AddDays(1).Subtract(TimeSpan.FromSeconds(1));
		}

		/// <summary>
		/// Given a DateTime it will move the set the time component for that day
		/// to 00:00:00, i.e, the very start of the day.
		/// </summary>
		public static DateTime MoveToStartOfDay(this DateTime dt)
		{
			_logger.DebugMethodCalled(dt);

			return dt.Date;
		}

        /// <summary>
        /// Gets a long date time string that is safe to be embedded directly 
        /// in file names.
        /// </summary>
        public static string ToFileSystemSafeString(this DateTime dt)
        {
            _logger.DebugMethodCalled(dt);

            return dt.ToString("dd-MM-yyyy_HH-mm-ss");
        }

        /// <summary>
        /// Gets the current time as a unix time stamp (i.e. number of seconds elapsed
        /// since 1/1/1970 00:00:00).
        /// </summary>
        /// <returns>
        /// The number of seconds elapsed since the unix epoch.
        /// </returns>
        public static long ToUnixTime(this DateTime dt)
        {
            return (long)(dt - UNIX_EPOCH).TotalSeconds;
        }

		#endregion
	}
}
