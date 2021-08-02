using System;

namespace Patterns.Util.Mapping.Mappers {
    public static class ValueMapper {
        public static bool MapValue<TValue, TTarget>(TValue SourceValue, TValue TargetValue, TTarget Target, Action<TTarget, TValue> TargetSetter) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
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
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    TargetSetter(Target, SourceValue);
                    return true;
                }
            }
            //}

            return false;
        }
    }
}
