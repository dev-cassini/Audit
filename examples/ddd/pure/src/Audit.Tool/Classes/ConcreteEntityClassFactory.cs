using System.Collections;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Audit.Tool.Classes;

using static SyntaxFactory;

public class ConcreteEntityClassFactory
{
    public CompilationUnitSyntax Create()
    {
        var privateAuditRecordProperty = PrivateReadonlyListOfAuditRecords();
        var publicAuditRecordProperty = PublicEnumerableOfAuditRecordsAutoProperty();
        
        var compilationUnitSyntax = CompilationUnit()
            .AddUsings(UsingDirective(ParseName("Audit.Domain.Abstraction.Model.Audit")));

        var @class = ClassDeclaration("Audit.Domain.Pump")
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddBaseListTypes(
                SimpleBaseType(ParseTypeName("Abstraction.Model.Pump")),
                SimpleBaseType(ParseTypeName("IAuditable<PumpAuditRecord>")))
            .AddMembers(privateAuditRecordProperty)
            .AddMembers(publicAuditRecordProperty);

        var @namespace = FileScopedNamespaceDeclaration(ParseName("Audit.Domain.Model"))
            .NormalizeWhitespace()
            .AddMembers(@class);

        return compilationUnitSyntax
            .AddMembers(@namespace);
    }

    private FieldDeclarationSyntax PrivateReadonlyListOfAuditRecords()
    {
        var auditRecordProperty = VariableDeclaration(ParseTypeName("List<PumpAuditRecord>"))
            .AddVariables(VariableDeclarator("_auditRecords")
                .WithInitializer(EqualsValueClause(ImplicitObjectCreationExpression())));

        return FieldDeclaration(auditRecordProperty)
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));
    }
    
    private PropertyDeclarationSyntax PublicEnumerableOfAuditRecordsAutoProperty()
    {
        var arrowExpressionClause = ArrowExpressionClause(InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression, 
                IdentifierName("_auditRecords"),
                IdentifierName("Where")),
            ArgumentList(SeparatedList(new List<ArgumentSyntax>
            {
                Argument(
                    SimpleLambdaExpression(
                        Parameter(Identifier("x")),
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("x"),
                                    IdentifierName("Metadata")),
                                IdentifierName("Any")))))
            }))));
        
        return PropertyDeclaration(
                GenericName(Identifier(nameof(IEnumerable)),
                    TypeArgumentList(Token(SyntaxKind.LessThanToken),
                        new SeparatedSyntaxList<TypeSyntax> { ParseTypeName("PumpAuditRecord") }, //TODO: figure out why this is blank
                        Token(SyntaxKind.GreaterThanToken))),
                Identifier("AuditRecords"))
            .WithExpressionBody(arrowExpressionClause)
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            .AddModifiers(Token(SyntaxKind.PublicKeyword));
    }
}