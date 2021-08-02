using Microsoft.CodeAnalysis;

namespace Patterns.Util.SourceGenerators.Extensions {
    internal static class TypeSymbolExtensions {
        public static string GetTypeFullName(this INamedTypeSymbol TypeSymbol) => TypeSymbol.ConstructedFrom.ToString();
    }
}
