using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Patterns.Util.SourceGenerators.Extensions {
    internal static class AttributeExtensions {
        public static IEnumerable<AttributeSyntax> GetAttributeNodes(this IEnumerable<SyntaxNode> Nodes)
            => Nodes.Where(n => n.IsKind(SyntaxKind.Attribute)).OfType<AttributeSyntax>();

        public static IEnumerable<AttributeSyntax> GetAttributeNodes(this IEnumerable<SyntaxNode> Nodes, string AttributeName) {
            var names = GetAttributeNames(AttributeName);

            var attributes = Nodes.GetAttributeNodes().Where(n => {
                var name = n.Name.ToString();
                return name == names.AttributeName || name == names.AttributeNameFull;
            });

            return attributes;
        }

        public static AttributeSyntax GetAttribute(this PropertyDeclarationSyntax Property, string AttributeName) {
            var names = GetAttributeNames(AttributeName);

            return Property.AttributeLists
                .SelectMany(a => a.Attributes)
                .Where(n => {
                    var name = n.Name.ToString();
                    return name == names.AttributeName || name == names.AttributeNameFull;
                })
                .FirstOrDefault();
        }

        public static string GetStringValue(this AttributeArgumentSyntax Argument)
            => (Argument.Expression as LiteralExpressionSyntax).Token.Value as string;

        private static (string AttributeName, string AttributeNameFull) GetAttributeNames(string AttributeName) {
            // nodes are literal to how attribute is used, so compare against AttributeName and AttributeNameAttribute to capture both usages
            string attributeNameFull;
            if (AttributeName.EndsWith(nameof(Attribute))) {
                attributeNameFull = AttributeName;
                AttributeName = AttributeName.Substring(AttributeName.Length - 9);
            } else {
                attributeNameFull = AttributeName + nameof(Attribute);
            }

            return (AttributeName, attributeNameFull);
        }
    }
}
