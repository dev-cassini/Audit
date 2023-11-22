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
    
    private PropertyDeclarationSyntax PublicEnumerableOfAuditRecordsAutoProperty()
    {
        return PropertyDeclaration(
            GenericName(Identifier("IEnumerable"),
                TypeArgumentList(Token(SyntaxKind.LessThanToken),
                    new SeparatedSyntaxList<TypeSyntax> { IdentifierName(Identifier("PumpAuditRecord")) },
                    Token(SyntaxKind.GreaterThanToken))),
            Identifier("AuditRecords"))
            .AddModifiers(Token(SyntaxKind.PublicKeyword));
    }
}