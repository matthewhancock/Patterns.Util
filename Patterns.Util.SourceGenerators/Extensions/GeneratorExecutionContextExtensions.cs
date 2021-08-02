using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Patterns.Util.SourceGenerators.Extensions {
    public static class GeneratorExecutionContextExtensions {
        public static IEnumerable<SyntaxNode> GetSyntaxNodes(this GeneratorExecutionContext Context)
            => Context.Compilation.SyntaxTrees.SelectMany(t => t.GetRoot().DescendantNodes());

        public static IEnumerable<SemanticModel> GetSemanticModels(this GeneratorExecutionContext Context)
            => Context.Compilation.SyntaxTrees.Select(tree => Context.Compilation.GetSemanticModel(tree));
    }
}
