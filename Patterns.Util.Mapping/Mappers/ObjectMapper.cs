using System;

namespace Patterns.Util.Mapping.Mappers {
    public static class ObjectMapper {
        public static bool MapObject<TSourceValue, TTargetValue, TTarget>(TSourceValue SourceValue, TTargetValue TargetValue,
            Func<TTargetValue> TargetConstructor, TTarget Target, Action<TTarget, TTargetValue> TargetSetter,
            Func<TSourceValue, TTargetValue, bool> Mappings) {
            if (SourceValue == null) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                }
            } else {
                var result = false;

                if (TargetValue == null) {
                    TargetValue = TargetConstructor();
                    TargetSetter(Target, TargetValue);
                    result = true;
                }

                result |= Mappings(SourceValue, TargetValue);

                return result;
            }

            return false;
        }
    }
}
