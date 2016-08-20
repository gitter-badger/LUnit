﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LCore.Interfaces;

namespace LCore.Extensions
    {
    /// <summary>
    /// Provides extension methods to IComparable objects.
    /// </summary>
    [ExtensionProvider]
    public static class ComparableExt
        {
        #region Extensions +

        #region IsEqualTo

        /// <summary>
        /// Use this fluent extension method to compare IComparable equality.
        /// </summary>
        public static bool IsEqualTo([CanBeNull] this IComparable In, [CanBeNull] IComparable Obj)
            {
            if (In == null && Obj != null)
                return false;
            if (In != null && Obj == null)
                return false;
            if (In == null)
                return true;

            return In.CompareTo(Obj) == 0;
            }

        #endregion

        #region IsNotEqualTo

        /// <summary>
        /// Use this fluent extension method to compare IComparable inequality.
        /// </summary>
        public static bool IsNotEqualTo([CanBeNull] this IComparable In, [CanBeNull] IComparable Obj)
            {
            if (In == null && Obj != null)
                return true;
            if (In != null && Obj == null)
                return true;
            if (In == null)
                return false;

            return In.CompareTo(Obj) != 0;
            }

        #endregion

        #region IsLessThan

        /// <summary>
        /// Use this fluent extension method to compare one IComparable to another.
        /// </summary>
        public static bool IsLessThan([CanBeNull] this IComparable In, [CanBeNull] IComparable Obj)
            {
            if (In == null && Obj != null)
                return true;
            if (In != null && Obj == null)
                return false;
            // ReSharper disable once UseNullPropagation
            if (In == null)
                return false;

            return In.CompareTo(Obj) < 0;
            }

        #endregion

        #region IsLessThanOrEqual

        /// <summary>
        /// Use this fluent extension method to compare one IComparable to another.
        /// </summary>
        public static bool IsLessThanOrEqual([CanBeNull] this IComparable In, [CanBeNull] IComparable Obj)
            {
            if (In == null && Obj != null)
                return true;
            if (In != null && Obj == null)
                return false;
            if (In == null)
                return true;

            return In.CompareTo(Obj) <= 0;
            }

        #endregion

        #region IsGreaterThan

        /// <summary>
        /// Use this fluent extension method to compare one IComparable to another.
        /// </summary>
        public static bool IsGreaterThan([CanBeNull] this IComparable In, [CanBeNull] IComparable Obj)
            {
            if (In == null && Obj != null)
                return false;
            if (In != null && Obj == null)
                return true;
            // ReSharper disable once UseNullPropagation
            if (In == null)
                return false;

            return In.CompareTo(Obj) > 0;
            }

        #endregion

        #region IsGreaterThanOrEqual

        /// <summary>
        /// Use this fluent extension method to compare one IComparable to another.
        /// </summary>
        public static bool IsGreaterThanOrEqual([CanBeNull] this IComparable In, [CanBeNull] IComparable Obj)
            {
            if (In == null && Obj != null)
                return false;
            if (In != null && Obj == null)
                return true;
            if (In == null)
                return true;

            return In.CompareTo(Obj) >= 0;
            }

        #endregion

        #region Max

        /// <summary>
        /// Returns the largest of <paramref name="In" /> and all items in <paramref name="Others" />
        /// </summary>
        public static T Max<T>([CanBeNull] this T In, [CanBeNull] params T[] Others)
            where T : IComparable
            {
            var Out = In;

            if (Others != null)
                {
                foreach (var Item in Others)
                    {
                    if (Out == null ||
                        (Item != null && Item.CompareTo(Out) > 0))
                        Out = Item;
                    }
                }
            return Out;
            }

        /// <summary>
        /// Returns the Item which causes <paramref name="Comparer"/> to return the greatest IComparable value.
        /// </summary>
        public static T Max<T>([CanBeNull] this IEnumerable<T> In, [CanBeNull] Func<T, IComparable> Comparer)
            {
            var Out = default(T);
            IComparable CompareValue = null;

            if (Comparer == null)
                return Out;

            if (In != null)
                {
                foreach (var Item in In)
                    {
                    var CompareValue2 = Comparer(Item);

                    if (CompareValue == null ||
                        (Item != null && CompareValue2.CompareTo(CompareValue) > 0))
                        {
                        CompareValue = CompareValue2;
                        Out = Item;
                        }
                    }
                }
            return Out;
            }

        #endregion

        #region Min

        /// <summary>
        /// Returns the smallest of <paramref name="In" /> and all items in <paramref name="Others" />
        /// </summary>
        public static T Min<T>([CanBeNull] this T In, [CanBeNull] params T[] Others)
            where T : IComparable
            {
            var Out = In;

            if (Others != null)
                {
                foreach (var Item in Others)
                    {
                    if (Out == null ||
                        (Item != null && Item.CompareTo(Out) < 0))
                        Out = Item;
                    }
                }
            return Out;
            }

        /// <summary>
        /// Returns the Item which causes <paramref name="Comparer"/> to return the smallest IComparable value.
        /// </summary>
        [CanBeNull]
        public static T Min<T>([CanBeNull] this IEnumerable<T> In, [CanBeNull] Func<T, IComparable> Comparer)
            {
            var Out = default(T);
            IComparable CompareValue = null;

            if (Comparer == null)
                return Out;

            if (In != null)
                {
                foreach (var Item in In)
                    {
                    var CompareValue2 = Comparer(Item);

                    if (CompareValue == null ||
                        (Item != null && CompareValue2.CompareTo(CompareValue) < 0))
                        {
                        CompareValue = CompareValue2;
                        Out = Item;
                        }
                    }
                }
            return Out;
            }

        #endregion

        #endregion
        }
    }