using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Audit.Tool.Classes;

using static SyntaxFactory;

public class ConcreteEntityClassFactory
{
    public CompilationUnitSyntax Create()
    {
        var auditRecordProperty = PrivateReadonlyListOfAuditRecords();
        
        var compilationUnitSyntax = CompilationUnit()
            .AddUsings(UsingDirective(ParseName("Audit.Domain.Abstraction.Model.Audit")));

        var @class = ClassDeclaration("Audit.Domain.Pump")
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddBaseListTypes(
                SimpleBaseType(ParseTypeName("Abstraction.Model.Pump")),
                SimpleBaseType(ParseTypeName("IAuditable<PumpAuditRecord>")))
            .AddMembers(auditRecordProperty);

        var @namespace = FileScopedNamespaceDeclaration(ParseName("Audit.Domain.Model")).NormalizeWhitespace()
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
    
    private FieldDeclarationSyntax PublicReadonlyListOfAuditRecords()
    {
        var auditRecordProperty = VariableDeclaration(ParseTypeName("List<PumpAuditRecord>"))
            .AddVariables(VariableDeclarator("_auditRecords")
                .WithInitializer(EqualsValueClause(ImplicitObjectCreationExpression())));

        return FieldDeclaration(auditRecordProperty)
            .AddModifiers(Token(SyntaxKind.PublicKeyword));
    }
}