﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities
{
	/// <summary>
	/// A class that allows a supplied peice of code to be retried a 
	/// number of times with a configurable wait inbetween each attempt.
	/// </summary>
	public static class Retry
	{
		#region public constants

        /// <summary>
        /// Keep retrying until the attempt succeeds, never stop on failure.
        /// </summary>
        public const int INFINITE_ATTEMPTS = int.MaxValue;

		/// <summary>
		/// The number of times that an operation should be retried by default (5 times)
		/// </summary>
		public const int DEFAULT_ATTEMPTS = 5;

		/// <summary>
		/// The interval to wait between tries by default (half a second)
		/// </summary>
		public static readonly TimeSpan DEFAULT_INTERVAL = TimeSpan.FromMilliseconds(500);

		#endregion

		#region Public static methods

		/// <summary>
		/// Attempt to execute the supplied function.  If the function fails then it will be
		/// retried 5 times with a half second interval inbetween attempts.
		/// Any exception thrown from the supplied function will be handled.
		/// </summary>
		/// <param name="functionToAttempt">
		/// The function to try and execute
		/// </param>
		/// <returns>
		/// An OperationResult that denotes whether or not the function succeeded.
		/// If the function succeeded then OperationResult.Succeeded is returned.
		/// If the function failed then a failure OperationResult is returned with
		/// details of the failuer and any thrown exceptions 
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if
		///		functionToAttempt is null
		/// </exception>
		public static OperationResult Attempt(Func<bool> functionToAttempt)
		{
			return Attempt(functionToAttempt, DEFAULT_ATTEMPTS, DEFAULT_INTERVAL);
		}

		/// <summary>
		/// Attempt to execute the supplied function. Any exception thrown from the
		/// supplied function will be handled.
		/// </summary>
		/// <param name="numberOfAttempts">
		/// The number of attempts to try
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween attempts
		/// </param>
		/// <param name="functionToAttempt">
		/// The function to try and execute
		/// </param>
		/// <returns>
		/// An OperationResult that denotes whether or not the function succeeded.
		/// If the function succeeded then OperationResult.Succeeded is returned.
		/// If the function failed then a failure OperationResult is returned with
		/// details of the failuer and any thrown exceptions
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown if
		///		numberOfAttempts is less than 1
		///		interval is less than 1 millisecond
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if
		///		functionToAttempt is null
		/// </exception>
		public static OperationResult Attempt(
			Func<bool> functionToAttempt,
			int numberOfAttempts,
			TimeSpan interval)
		{
			return Attempt(functionToAttempt, numberOfAttempts, interval, RetryExceptionBehaviour.HandleAndCollate);
		}

		/// <summary>
		/// Attempt to execute the supplied function.
		/// </summary>
		/// <param name="numberOfAttempts">
		/// The number of attempts to try
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween attempts
		/// </param>
		/// <param name="functionToAttempt">
		/// The function to try and execute
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not to handle exceptions that get thrown as part of
		/// executing the supplied function.
		/// </param>
		/// <returns>
		/// An OperationResult that denotes whether or not the function succeeded.
		/// If the function succeeded then OperationResult.Succeeded is returned.
		/// If the function failed then a failure OperationResult is returned with
		/// details of the failuer and any thrown exceptions (if handleExceptions was true)
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown if
		///		numberOfAttempts is less than 1
		///		interval is less than 1 millisecond
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if
		///		functionToAttempt is null
		/// </exception>
		public static OperationResult Attempt(
			Func<bool> functionToAttempt,
			int numberOfAttempts,
			TimeSpan interval,
			RetryExceptionBehaviour exceptionBehaviour)
		{
			#region Input validation

			Insist.IsAtLeast(numberOfAttempts, 1, "numberOfAttempts");
			Insist.IsAtLeast(interval, TimeSpan.FromMilliseconds(1), "interval");
			Insist.IsNotNull(functionToAttempt, "functionToAttempt");
            Insist.IsDefined<RetryExceptionBehaviour>(exceptionBehaviour, "exceptionBehaviour");

			#endregion

			int currentAttempts = 0;

			List<Exception> thrownExceptions = new List<Exception>();
            int numberOfThrownExceptions = 0;

            bool usingInfiniteAttempts = numberOfAttempts == INFINITE_ATTEMPTS;

			while (currentAttempts < numberOfAttempts)
			{
				bool success = false;

				try
				{
					success = functionToAttempt();
				}
				catch (Exception e)
				{
                    if (exceptionBehaviour == RetryExceptionBehaviour.DoNotHandle)
                    {
                        throw;
                    }
                    else if( exceptionBehaviour == RetryExceptionBehaviour.HandleAndCollate)
                    {
                        //Record the exception so that we can return information
                        //about it later on.
                        thrownExceptions.Add(e);
                        numberOfThrownExceptions++;
                    }
                    else if (exceptionBehaviour == RetryExceptionBehaviour.HandleOnly)
                    {
                        numberOfThrownExceptions++;
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("Unknown RetryExceptionBehaviour '{0}'", exceptionBehaviour));
                    }

				}

				if (success)
				{
					return OperationResult.Succeeded;
				}

                if (!usingInfiniteAttempts)
                //If we're using an infinite number of attempts then
                //we don't want to make any progress regards number of
                //attempts.  Just keep looping until the attempt succeeds.
                {
                    currentAttempts++;
                }

				Thread.Sleep(interval);
			}

            //If we get to this point then all attempts have failed.
            List<string> errorMessages = new List<string>();
            errorMessages.Add(string.Format("The function was not successfull after {0} attempts", numberOfAttempts));
            
            if (numberOfThrownExceptions > 0)
            {
                if (exceptionBehaviour == RetryExceptionBehaviour.HandleAndCollate)
                {
                    errorMessages.AddRange(thrownExceptions.Select(ex => ex.AllMessages()));
                }
                else if(exceptionBehaviour == RetryExceptionBehaviour.HandleOnly)
                {
                    errorMessages.Add(string.Format(string.Format("A total of '{0}' exceptions were thrown during the attempt", numberOfThrownExceptions)));
                }
            }

            return new OperationResult(errorMessages);
        }

		#endregion
	}
}
