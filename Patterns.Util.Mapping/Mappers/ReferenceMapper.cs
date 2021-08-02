using System;
using System.Collections.Generic;
using System.Linq;

using static Patterns.Util.Mapping.Mappers.EnumerableMapper;

namespace Patterns.Util.Mapping.Mappers {
    public static class ReferenceMapper {
        public static bool MapReference<TSourceItem, TValue, TTarget>(IEnumerable<TSourceItem> SourceValue, Func<TSourceItem, TValue> ReferenceAccessor,
            IEnumerable<TValue> TargetValue, TTarget Target, Action<TTarget, IEnumerable<TValue>> TargetSetter)
            => MapEnumerable(SourceValue?.Select(ReferenceAccessor), TargetValue, Target, TargetSetter);

        public static bool MapFromReference<TValue, TTargetItem, TTarget>(IEnumerable<TValue> SourceValue, IEnumerable<TTargetItem> TargetValue,
            Func<TTargetItem, TValue> ReferenceAccessor, TTarget Target, Action<TTarget, IEnumerable<TTargetItem>> TargetSetter,
            IDictionary<TValue, TTargetItem> TargetLookup) {
            // if null source, clear target
            if (SourceValue == null) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                } else {
                    return false;
                }
            }

            // if target is null or empty, lookup all values
            if (TargetValue == null || !TargetValue.Any()) {
                var newValue = SourceValue.Where(id => TargetLookup.ContainsKey(id)).Select(id => TargetLookup[id]);
                TargetSetter(Target, newValue);
                return true;
            }

            // otherwise, merge lists
            var itemsToRemove = TargetValue.Where(t => !SourceValue.Contains(ReferenceAccessor(t)));
            var targetIds = TargetValue.Select(ReferenceAccessor).ToList();
            var itemsToAdd = SourceValue.Where(id => !targetIds.Contains(id));

            if (itemsToRemove.Any() || itemsToAdd.Any()) {
                var newValue = TargetValue.ToList();

                if (itemsToRemove.Any()) {
                    foreach (var i in itemsToRemove) {
                        newValue.Remove(i);
                    }
                }

                if (itemsToAdd.Any()) {
                    var newItems = itemsToAdd.Where(id => TargetLookup.ContainsKey(id)).Select(id => TargetLookup[id]);
                    newValue.AddRange(newItems);
                }

                TargetSetter(Target, newValue);
                return true;
            }

            return false;
        }
    }
}
