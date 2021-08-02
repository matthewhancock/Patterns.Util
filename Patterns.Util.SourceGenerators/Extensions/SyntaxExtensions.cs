using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Patterns.Util.SourceGenerators.Extensions {
    public static class SyntaxExtensions {
        public static INamedTypeSymbol GetTypeSymbol(this TypeSyntax TypeSyntax, IEnumerable<SemanticModel> Models) =>
            Models.Select(m => m.GetSymbolInfo(TypeSyntax).Symbol as INamedTypeSymbol).FirstOrDefault();
        public static INamedTypeSymbol GetTypeSymbol(this ClassDeclarationSyntax ClassSyntax, IEnumerable<SemanticModel> Models) =>
            Models.Select(m => m.GetDeclaredSymbol(ClassSyntax)).FirstOrDefault();
    }
}
