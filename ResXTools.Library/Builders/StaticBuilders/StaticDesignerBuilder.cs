using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResXTools.Library.Builders.Base;
using ResXTools.Library.Builders.Interfaces;

namespace ResXTools.Library.Builders
{
    public class StaticDesignerBuilder : DesignerBuilder
    {
        public StaticDesignerBuilder(string resourceFile, string classNamespace) : base(resourceFile, classNamespace)
        {
        }

        public override ICodeBuilder Build()
        {
            throw new NotImplementedException();
        }

        protected override ConstructorDeclarationSyntax GetConstructor()
        {
            throw new NotImplementedException();
        }

        protected override FieldDeclarationSyntax[] GetFields()
        {
            throw new NotImplementedException();
        }

        protected override List<MemberDeclarationSyntax> GetResourceAccessors()
        {
            throw new NotImplementedException();
        }
    }
}