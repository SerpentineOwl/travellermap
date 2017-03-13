﻿using Maps.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void FixCapitalizationTest()
        {
            Assert.AreEqual("", "".FixCapitalization());
            Assert.AreEqual("Regina", "Regina".FixCapitalization());
            Assert.AreEqual("Regina", "regina".FixCapitalization());
            Assert.AreEqual("Regina", "REGINA".FixCapitalization());
            Assert.AreEqual("Varen's Planet", "VAREN'S PLANET".FixCapitalization());
            Assert.AreEqual("St. George", "ST. GEORGE".FixCapitalization());
            Assert.AreEqual("Airlyrlyu'eas", "AIRLYRLYU'EAS".FixCapitalization());
            Assert.AreEqual("494-908", "494-908".FixCapitalization());
        }

        [TestMethod]
        public void SwapTest()
        {
            object o = new object();
            object p = new object();
            Assert.AreNotEqual(o, p);

            object a = o;
            object b = p;
            Assert.AreEqual(o, a);
            Assert.AreEqual(p, b);
            Assert.AreNotEqual(a, b);

            Util.Swap(ref a, ref b);
            Assert.AreEqual(o, b);
            Assert.AreEqual(p, a);
            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        public void ClampTest()
        {
            Assert.AreEqual(5, 5.Clamp(1, 10));
            Assert.AreEqual(1, -5.Clamp(1, 10));
            Assert.AreEqual(10, 15.Clamp(1, 10));
            Assert.AreEqual(10, 5.Clamp(10, 1));

            Assert.AreEqual("c", "c".Clamp("a", "f"));
            Assert.AreEqual("a", "@".Clamp("a", "f"));
            Assert.AreEqual("f", "z".Clamp("a", "f"));
        }

        [TestMethod]
        public void SequenceTest()
        {
            CollectionAssert.AreEqual(new int[] { 0 }, Util.Sequence(0, 0).ToArray());
            CollectionAssert.AreEqual(new int[] { 0, 1 }, Util.Sequence(0, 1).ToArray());
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, Util.Sequence(0, 4).ToArray());
            CollectionAssert.AreEqual(new int[] { 4, 3, 2, 1, 0 }, Util.Sequence(4, 0).ToArray());
        }

        [TestMethod]
        public void SafeSubstringTest()
        {
            Assert.AreEqual("abc", "abc".SafeSubstring(0, 100));
            Assert.AreEqual("", "abc".SafeSubstring(100, 100));
            Assert.AreEqual("c", "abc".SafeSubstring(2, 100));
        }

        [TestMethod]
        public void TruncateTest()
        {
            Assert.AreEqual("abc", "abc".Truncate(100));
            Assert.AreEqual("", "".Truncate(100));
            Assert.AreEqual("a", "abc".Truncate(1));
            Assert.AreEqual("", "".Truncate(1));
            Assert.AreEqual("", "abc".Truncate(0));
            Assert.AreEqual("", "".Truncate(0));
        }

        [TestMethod]
        public void GlobTest()
        {
            Assert.IsTrue(new Glob("*.foo").IsMatch("abc.foo"));
            Assert.IsFalse(new Glob("*.foo").IsMatch("abc.foo.bar"));
            Assert.IsTrue(new Glob("a*b*c").IsMatch("abc"));
            Assert.IsTrue(new Glob("a*b*c").IsMatch("aXXbc"));
            Assert.IsTrue(new Glob("a*b*c").IsMatch("abXXc"));
            Assert.IsTrue(new Glob("a*b*c").IsMatch("aXXbXXXc"));
            Assert.IsFalse(new Glob("a*b*c").IsMatch("ac"));
            Assert.IsFalse(new Glob("a*b*c").IsMatch("Xabc"));
            Assert.IsFalse(new Glob("a*b*c").IsMatch("abcX"));

            Assert.IsTrue(new Glob("?.foo").IsMatch("a.foo"));
            Assert.IsFalse(new Glob("?.foo").IsMatch("ab.foo"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("abc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("aXbc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("abXc"));
            Assert.IsTrue(new Glob("a?b?c").IsMatch("aXbXc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("aXXbc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("abXXc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("aXXbXXc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("ac"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("Xabc"));
            Assert.IsFalse(new Glob("a?b?c").IsMatch("abcX"));
        }

        [TestMethod]
        public void ListHashSetTest()
        {
            OrderedHashSet<string> set = new OrderedHashSet<string>();

            Assert.AreEqual(set.Count(), 0);
            CollectionAssert.AreEqual(new string[] { }, set.ToArray());
            Assert.IsFalse(set.Contains("a"));
            Assert.IsFalse(set.Contains("b"));
            
            set.Add("b");
            Assert.AreEqual(set.Count(), 1);
            Assert.IsFalse(set.Contains("a"));
            Assert.IsTrue(set.Contains("b"));
            CollectionAssert.AreEqual(new string[] { "b" }, set.ToArray());

            set.Add("a");
            Assert.AreEqual(set.Count(), 2);
            Assert.IsTrue(set.Contains("a"));
            Assert.IsTrue(set.Contains("b"));
            CollectionAssert.AreEqual(new string[] { "b", "a" }, set.ToArray());

            set.Add("b");
            Assert.AreEqual(set.Count(), 2);
            Assert.IsTrue(set.Contains("a"));
            Assert.IsTrue(set.Contains("b"));
            CollectionAssert.AreEqual(new string[] { "b", "a" }, set.ToArray());

            Assert.AreEqual(set[0], "b");
            Assert.AreEqual(set[1], "a");

            set.Remove("b");

            CollectionAssert.AreEqual(new string[] { "a" }, set.ToArray());
            Assert.IsTrue(set.Contains("a"));
            Assert.IsFalse(set.Contains("b"));

            set.Clear();

            CollectionAssert.AreEqual(new string[] {}, set.ToArray());
            Assert.IsFalse(set.Contains("a"));
            Assert.IsFalse(set.Contains("b"));
        }
    }
}
