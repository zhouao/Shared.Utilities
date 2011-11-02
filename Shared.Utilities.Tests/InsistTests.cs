﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;

namespace Shared.Utilities.Tests {
	[TestFixture]
	public class InsistTests {

		public const string ARGUMENT_NAME = "MyArg";

		public const string MESSAGE = "*#MyMessage@$";

		#region Test Enums

		private enum TestEnum
		{
			First = 1,
			Second = 2,
			Last = 99
		}

		#endregion

		#region IsNotNull

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void IsNotNull_1_Null_Value_Throws_Exception() {

			Insist.IsNotNull(null);

		}

		[Test]
		public void IsNotNull_1_Non_Null_Value_Does_Not_Throw_Exception() {

			Insist.IsNotNull(new Object());

		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void IsNotNull_2_Null_Value_Throws_Exception() {

			Insist.IsNotNull(null, ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNull_2_Non_Null_Value_Does_Not_Throw_Exception() {

			Insist.IsNotNull(new Object(), ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNull_2_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsNotNull(null, ARGUMENT_NAME);

			} catch(ArgumentNullException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsNotNull_2_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsNotNull(null, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentNullException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsNotNullOrEmpty

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase("")]
		[TestCase(null)]
		public void IsNotNullOrEmpty_1_Null_Or_Empty_Value_Throws_Exception(string argValue) {

			Insist.IsNotNullOrEmpty(argValue);

		}

		[Test]
		public void IsNotNullOrEmpty_1_Non_Null_Or_Empty_Value_Does_Not_Throw_Exception() {

			Insist.IsNotNullOrEmpty("This is not null");

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase("")]
		[TestCase(null)]
		public void IsNotNullOrEmpty_2_Null_Or_Empty_Value_Throws_Exception(string argValue) {

			Insist.IsNotNullOrEmpty(argValue, ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNullOrEmpty_2_Non_Null_Or_Empty_Value_Does_Not_Throw_Exception() {

			Insist.IsNotNullOrEmpty("This is not null.", ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNullOrEmpty_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsNotNullOrEmpty(null, ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsNotNullOrEmpty_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsNotNullOrEmpty(null, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsWithinBounds

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(1, 5, 10)]
		[TestCase(10, 1, 5)]
		public void IsWithinBounds_Value_Out_Of_Bounds_Throws_Exception(int value, int min, int max) {

			Insist.IsWithinBounds(value, min, max, ARGUMENT_NAME);

		}

		[Test]
		public void IsWithinBounds_Value_Witin_Bounds_Does_Not_Throw_Exception() {

			Insist.IsWithinBounds(5, 1, 10, ARGUMENT_NAME);

		}

		[Test]
		public void IsWithinBounds_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsWithinBounds(1, 5, 10, ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsWithinBounds_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsWithinBounds(1, 5, 10, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsAtLeast

		[Test]
		[ExpectedException(typeof(ArgumentException))]		
		public void IsAtLeast_Value_Out_Of_Bounds_Throws_Exception() {

			Insist.IsAtLeast(1, 5, ARGUMENT_NAME);

		}

		[Test]
		public void IsAtLeast_Value_Within_Bounds_Does_Not_Throw_Exception() {

			Insist.IsAtLeast(10, 5, ARGUMENT_NAME);

		}

		[Test]
		public void IsAtLeast_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsAtLeast(1, 5, ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsAtLeast_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsAtLeast(1, 5, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsAtMost

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAtMost_Value_Out_Of_Bounds_Throws_Exception()
		{
			Insist.IsAtMost(10, 5, ARGUMENT_NAME);
		}

		[Test]
		public void IsAtMost_Value_Within_Bounds_Does_Not_Throw_Exception()
		{
			Insist.IsAtMost(1, 5, ARGUMENT_NAME);
		}

		[Test]
		public void IsAtMost_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				Insist.IsAtMost(10, 5, ARGUMENT_NAME);
			}
			catch (ArgumentException e)
			{
				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);
			}
		}

		[Test]
		public void IsAtMost_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				Insist.IsAtMost(10, 5, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region IsAssignableFrom

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAssignableFrom_Unassignable_Type_Throws_Exception() {

			Insist.IsAssignableFrom<IDisposable>(typeof(Object), ARGUMENT_NAME);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAssignableFrom_Null_Type_Throws_Exception() {

			Insist.IsAssignableFrom<IDisposable>(null, ARGUMENT_NAME);

		}

		[Test]
		public void IsAssignableFrom_Assignable_Type_Does_Not_Throw_Exception() {

			Insist.IsAssignableFrom<IDisposable>(typeof(IDataReader), ARGUMENT_NAME);

		}

		[Test]
		public void IsAssignableFrom_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsAssignableFrom<IDisposable>(typeof(Object), ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsAssignableFrom_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsAssignableFrom<IDisposable>(typeof(Object), ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region Equality

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Equality_Non_Matching_Values_Throws_Exception() {

			Insist.Equality("ABC123", "DEF456", ARGUMENT_NAME);

		}

		[Test]
		[TestCase("ABC123")]
		[TestCase(12345)]
		[TestCase(12345D)]
		[TestCase(12345L)]		
		public void Equality_Matching_Values_Does_Not_Throw_Exception(Object expected) {

			Insist.Equality(expected, expected, ARGUMENT_NAME);

		}

		[Test]
		public void Equality_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.Equality("ABC123", "DEF456", ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void Equality_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.Equality("ABC123", "DEF456", ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region AllItemsAreNotNull Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void AllItemsAreNotNull_Null_Collection_Throws_Exception()
		{
			IList<string> list = null;
			Insist.AllItemsAreNotNull(list);
		}

		[Test]
		public void AllItemsAreNotNull_Empty_Collection_Does_Not_Throw_Exception()
		{
			IList<string> list = new List<string>();
			Insist.AllItemsAreNotNull(list);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void AllItemsAreNotNull_Collection_Contains_Null_Throws_Exception()
		{
			IList<string> list = new List<string>() { "a", "b", null, "c" };
			Insist.AllItemsAreNotNull(list);
		}

		[Test]
		public void AllItemsAreNotNull_Collection_Does_Not_Contain_Null_Does_Not_Throw_Exception()
		{
			IList<string> list = new List<string>() { "a", "b", "c" };
			Insist.AllItemsAreNotNull(list);
		}

		[Test]
		public void AllItemsAreNotNull_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", null, "c" };
				Insist.AllItemsAreNotNull(list, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void AllItemsAreNotNull_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", null, "c" };
				Insist.AllItemsAreNotNull(list, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region AllItemsSatisfyCondition Tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void AllItemsSatisfyCondition_Null_Collection_Throws_Exception()
		{
			IList<int> list = null;
			Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; });
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void AllItemsSatisfyCondition_Null_Predicate_Throws_Exception()
		{
			IList<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			Insist.AllItemsSatisfyCondition(list, null);
		}

		[Test]
		public void AllItemsSatisfyCondition_Empty_Collection_Does_Not_Throw_Exception()
		{
			IList<int> list = new List<int>();
			Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; });
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void AllItemsSatisfyCondition_Collection_Contains_Invalid_Value_Throws_Exception()
		{
			IList<int> list = new List<int>() { 1, 2, 3, 0, 4, 5 };
			Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; });
		}

		[Test]
		public void AllItemsSatisfyCondition_Collection_Does_Not_Contain_Invalid_Value_Does_Not_Throw_Exception()
		{
			IList<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; });
		}

		[Test]
		public void AllItemsSatisfyCondition_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<int> list = new List<int>() { 1, 2, 3, 0, 4, 5 };
				Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void AllItemsSatisfyCondition_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<int> list = new List<int>() { 1, 2, 3, 0, 4, 5 };
				Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region ContainsAtLeast Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void ContainsAtLeast_Null_Collection_Throws_Exception()
		{
			List<string> list = null;
			Insist.ContainsAtLeast(list, 1);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtLeast_Number_Of_Items_Less_Than_Zero_Throws_Exception()
		{
			List<string> list = new List<string>();
			Insist.ContainsAtLeast(list, -1);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtLeast_Number_Of_Items_Less_Than_Required_Throws_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
			Insist.ContainsAtLeast(list, 10);
		}

		[Test]
		public void ContainsAtLeast_Number_Of_Items_Equal_To_Required_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
			Insist.ContainsAtLeast(list, 3);
		}

		[Test]
		public void ContainsAtLeast_Number_Of_Items_More_Than_Required_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
			Insist.ContainsAtLeast(list, 1);
		}

		[Test]
		public void ContainsAtLeast_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtLeast(list, 10, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void ContainsAtLeast_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtLeast(list, 10, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region ContainsAtLeast Tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void ContainsAtMost_Null_Collection_Throws_Exception()
		{
			List<string> list = null;
			Insist.ContainsAtMost(list, 1);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtMost_Number_Of_Items_Less_Than_Zero_Throws_Exception()
		{
			List<string> list = new List<string>();
			Insist.ContainsAtMost(list, -1);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtMost_Number_Of_Items_More_Than_Max_Throws_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
			Insist.ContainsAtMost(list, 2);
		}

		[Test]
		public void ContainsAtMost_Number_Of_Items_Equal_To_Max_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
			Insist.ContainsAtMost(list, 3);
		}

		[Test]
		public void ContainsAtMost_Number_Of_Items_Less_Than_Max_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
			Insist.ContainsAtMost(list, 10);
		}

		[Test]
		public void ContainsAtMost_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtMost(list, 1, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}
		[Test]
		public void ContainsAtMost_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtMost(list, 1, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}


		#endregion

		#region IsDefined Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void IsDefined_Value_Is_Not_defined()
		{
			TestEnum e = (TestEnum)0;
			Insist.IsDefined<TestEnum>(e);
		}

		[Test]
		public void IsDefined_Value_Is_defined()
		{
			TestEnum e = (TestEnum)99;
			Insist.IsDefined<TestEnum>(e);
		}

		[Test]
		public void IsDefined_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				TestEnum e = (TestEnum)0;
				Insist.IsDefined<TestEnum>(e, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void IsDefined_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				TestEnum e = (TestEnum)0;
				Insist.IsDefined<TestEnum>(e, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion
	}
}
