using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Util.Mapping.Mappers {
    public static class EnumerableMapper {

        public static bool MapEnumerable<TSourceItem, TTargetItem, TKey, TTarget>(IEnumerable<TSourceItem> SourceValue, IEnumerable<TTargetItem> TargetValue,
            Func<TSourceItem, TKey> SourceKeyAccessor, Func<TTargetItem, TKey> TargetKeyAccessor, Func<TTargetItem> TargetItemConstructor,
            TTarget Target, Action<TTarget, IEnumerable<TTargetItem>> TargetSetter, Func<TSourceItem, TTargetItem, bool> MapItem) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                } else {
                    return false;
                }
                //}
            }

            var modified = false;

            var newTarget = new List<TTargetItem>();

            foreach (var i in SourceValue) {
                var sourceKey = SourceKeyAccessor(i);

                // check if item exists in target
                TTargetItem targetItem;
                if (TargetValue != null) {
                    targetItem = TargetValue.FirstOrDefault(t => TargetKeyAccessor(t).Equals(sourceKey));

                    // create item if it doesn't exist
                    if (targetItem == null) {
                        targetItem = TargetItemConstructor();
                        modified = true;
                    }
                } else {
                    targetItem = TargetItemConstructor();
                    modified = true;
                }

                modified |= MapItem(i, targetItem);
                newTarget.Add(targetItem);
            }

            // if no changes detected, ensure no items removed from existing Target
            modified |= !EnumerableEquals(TargetValue, newTarget);

            if (modified) {
                TargetSetter(Target, newTarget);
            }

            return modified;
        }

        public static bool MapEnumerable<TValue, TTarget>(IEnumerable<TValue> SourceValue, IEnumerable<TValue> TargetValue,
            TTarget Target, Action<TTarget, IEnumerable<TValue>> TargetSetter) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                TargetSetter(Target, SourceValue);
                return true;
            } else if (!EnumerableEquals(SourceValue, TargetValue)) {
                // otherwise, set value
                TargetSetter(Target, SourceValue);
                return true;
            }

            return false;
        }

        /// <remarks>
        /// Equality comparison (Intersect) will work for ValueTypes but not reference types if new ones created.
        /// However, this is private and in this controlled environment so it shouldn't be an issue as long as objects are reused
        /// </remarks>
        private static bool EnumerableEquals<TValue>(IEnumerable<TValue> EnumerableA, IEnumerable<TValue> EnumerableB) {
            if (EnumerableA == null && EnumerableB == null) {
                return true;
            } else if (EnumerableA == null || EnumerableB == null) {
                return false;
            }

            var lengthA = EnumerableA.Count();
            var lengthB = EnumerableB.Count();

            // if length the same, and values the same then exit
            if (lengthA == lengthB && EnumerableA.Intersect(EnumerableB).Count() == lengthA) {
                return true;
            }

            return false;
        }
    }
}
